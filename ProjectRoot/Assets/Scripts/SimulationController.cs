using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationController : MonoBehaviour {

    private static SimulationController instance;
    public static SimulationController GetInstance() { return instance; }

    [SerializeField] List<Platform> platforms = new List<Platform>();
    public List<Genome> currentSelectedGlobule = new List<Genome>();

    public int numbGlobuleToCreate = 10;
    public int numbCurrentGeneration;
    public int totalPopulation;
    public float averageFitness;
    public float mutationRate;
    public float timeToSelect;

    void Start () {
        instance = this;
	}

	void Update () {
		
	}

    void CreatePopulation() {

    }

    void CalculateFitness() {

    }

    //set selection

    void SelectParents() {

    }

    //make a new child
    void Crossover(Genome firstSelectedParent, Genome secondSelectedParent) {

    }

    void Mutate() {

    }
}
