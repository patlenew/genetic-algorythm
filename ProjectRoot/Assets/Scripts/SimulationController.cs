using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationController : MonoBehaviour
{

    private static SimulationController instance;
    public static SimulationController GetInstance() { return instance; }

    [SerializeField] List<Platform> platforms = new List<Platform>();
    [SerializeField] Genome newGenome;
    [SerializeField] Transform genomeSpawnLocation;
    [SerializeField] Transform genomeParent;
    public List<Genome> currentSelectedGenome = new List<Genome>();

    public int populationSize = 10;
    public int numbCurrentGeneration;
    public int totalPopulation;
    public float averageFitness;
    public float mutationRate;
    public float timeToSelect;

    private int count = 0;
    private int lifespan = 200;

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
            ResetPopulation();
            CreatePopulation();
            count = 0;
        }
    }

    void CreatePopulation()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Genome genome = Instantiate(newGenome, genomeSpawnLocation.position, Quaternion.identity);
            genome.transform.SetParent(genomeParent);
            currentSelectedGenome.Add(genome);
        }
        SetIgnoreCollision();
    }

    void ResetPopulation()
    {
        foreach (Genome genome in currentSelectedGenome)
        {
            Destroy(genome.gameObject);
        }

        currentSelectedGenome.Clear();
    }

    void SetIgnoreCollision()
    {
        foreach (Genome genome in currentSelectedGenome)
        {
            genome.IgnoreColliders(currentSelectedGenome);
            genome.StartMoving();
        }


    }

    void CalculateFitness()
    {

    }

    //set selection

    void SelectParents()
    {

    }

    //make a new child
    void Crossover(Genome firstSelectedParent, Genome secondSelectedParent)
    {

    }

    void Mutate()
    {

    }
}
