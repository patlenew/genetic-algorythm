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
    public List<Genome> currentGenomes = new List<Genome>();
    public List<Genome> matingGenomePool = new List<Genome>();

    public int populationSize = 10;
    public int lifespan = 300;

    private int count = 0;
    private bool isInit;

    void Start()
    {
        instance = this;
        CreatePopulation();
    }

    void Update()
    {
        count++;
        if (count >= lifespan)
        {
            isInit = false;
            //ResetPopulation();
            //CreatePopulation();
            Evaluate();
            Selection();
            SetIgnoreCollision();
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
        SetIgnoreCollision();
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

    void SetIgnoreCollision()
    {
        foreach (Genome genome in currentGenomes)
        {
            genome.IgnoreColliders(currentGenomes);
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

        matingGenomePool.Clear();
        foreach (Genome genome in currentGenomes)
        {
            float n = genome.fitness * 100;
            Debug.Log("fitness augment: " + n);
            for (int i = 0; i<n; i++)
            {
                matingGenomePool.Add(genome);
            }
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
           /* DNA parentA = GetDNAByGoodFitness();
            if (parentA == null)
            {
                return;
            }

            DNA parentB = GetDNAByGoodFitness();
            if (parentB == null)
            {
                return;
            */

            DNA parentA = matingGenomePool[Random.Range(0, matingGenomePool.Count)].dna;
            DNA parentB = matingGenomePool[Random.Range(0, matingGenomePool.Count)].dna;
            DNA child = parentA.Crossover(parentB);
            child.Mutate();

            Genome genome = Instantiate(newGenome, genomeSpawnLocation.position, Quaternion.identity);
            genome.transform.SetParent(genomeParent);
            SetGenomeTargetPlatform(genome);
            genome.SetDNA(child);
            newGenomeList[i] = genome;
        }

        ResetPopulation();
        currentGenomes = newGenomeList;
    }

    /*DNA GetDNAByGoodFitness()
    {
        SortMatingPool();
        for (int i = 0; i<matingGenomePool.Count; i++)
        {
            return matingGenomePool[i].dna;
        }
        return null;
    }*/

    void SortMatingPool()
    {
        matingGenomePool.Sort((x, y) => y.fitness.CompareTo(x.fitness));
        List<Genome> newMatingList = new List<Genome>(50);

        for (int i = 0; i<50; i++)
        {
            newMatingList.Add(matingGenomePool[i]);
            Debug.Log("Sorted fitness of pool: " + matingGenomePool[i].fitness);
        }

        matingGenomePool = newMatingList;
    }
}
