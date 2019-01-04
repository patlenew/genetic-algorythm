using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
    private static SimulationController instance;
    public static SimulationController GetInstance() { return instance; }

    [SerializeField] Platform targetPlatform;
    [SerializeField] Genome newGenome;
    [SerializeField] Transform genomeSpawnLocation;
    [SerializeField] Transform genomeParent;

    public int populationSize = 150;
    public int lifespan = 300;

    private List<Genome> currentGenomes = new List<Genome>();
    private List<Genome> matingGenomePool = new List<Genome>();
    private int count = 0;
    private bool isInit;

    void Start()
    {
        instance = this;
        CreatePopulation();
    }

    void FixedUpdate()
    {
        count++;
        if (count >= lifespan)
        {
            isInit = false;
            Evaluate();
            Selection();
            InitPopulation();
            count = 0;
        }
    }

    void CreatePopulation()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Genome genome = Instantiate(newGenome, genomeSpawnLocation.position, Quaternion.identity);
            genome.transform.SetParent(genomeParent);
            SetGenomeTargetPlatform(genome);
            currentGenomes.Add(genome);
        }
        InitPopulation();
    }

    void SetGenomeTargetPlatform(Genome genome)
    {
        genome.SetTargetPlatform(targetPlatform);
    }

    void ResetPopulation()
    {
        foreach (Genome genome in currentGenomes)
        {
            Destroy(genome.gameObject);
        }

        currentGenomes.Clear();
    }

    void InitPopulation()
    {
        foreach (Genome genome in currentGenomes)
        {
            //genome.IgnoreColliders(currentGenomes);
            genome.StartMoving();
        }

        isInit = true;
    }

    void Evaluate()
    {
        float maxFitness = 0f;
        foreach (Genome genome in currentGenomes)
        {
            genome.CalculateFitness();

            if (genome.fitness > maxFitness)
            {
                maxFitness = genome.fitness;
            }
        }

        foreach (Genome genome in currentGenomes)
        {
            genome.fitness /= maxFitness;
        }

        Debug.Log("Max fitness: " + maxFitness);
    }

    //set selection
    void Selection()
    {
        SortMatingPool();
        List<Genome> newGenomeList = new List<Genome>(new Genome[currentGenomes.Count]);
        for(int i = 0; i<currentGenomes.Count; i++)
        {
            DNA parentA = null;
            DNA parentB = null;
            DNA child = null;

            if (i < 3)
            {
                child = matingGenomePool[i].dna;
            }
            else
            {
               parentA = matingGenomePool[Random.Range(0, matingGenomePool.Count)].dna;
               parentB = matingGenomePool[Random.Range(0, matingGenomePool.Count)].dna;
               child = parentA.Crossover(parentB);
               child.Mutate();
            }

            Genome genome = Instantiate(newGenome, genomeSpawnLocation.position, Quaternion.identity);
            genome.transform.SetParent(genomeParent);
            SetGenomeTargetPlatform(genome);
            genome.SetDNA(child);
            newGenomeList[i] = genome;
        }

        ResetPopulation();
        currentGenomes = newGenomeList;
    }

    void SortMatingPool()
    {
        currentGenomes.Sort((x, y) => y.fitness.CompareTo(x.fitness));
        List<Genome> newMatingList = new List<Genome>();

        float pourcentage = currentGenomes.Count / 100 * 20;
        for (int i = 0; i<(int)pourcentage; i++)
        {
            newMatingList.Add(currentGenomes[i]);
        }

        matingGenomePool = newMatingList;
    }
}
