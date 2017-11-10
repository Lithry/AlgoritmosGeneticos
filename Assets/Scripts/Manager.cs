using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    private List<Agent> agents = new List<Agent>();
    private Vector3 beginPos;
    private GeneticAlg geneticAlg;
    public GameObject prefab;
    public int NumOfAgents;
    public int NumOfGensPerAgent;
    public int numOfElites;
    public float mutationProbability;
    public float durationOfSimulation = 0.0f;
    [Range(1.0f, 5.0f)]
    public float speedOfSimulation = 1.0f;
    private float fixedDelta;

    // Use this for initialization
    void Awake() {
        geneticAlg = new GeneticAlg(numOfElites, NumOfAgents, NumOfGensPerAgent, mutationProbability);
        beginPos.Set(-5, 2, 0);

        for (int i = 0; i < NumOfAgents; i++) {
            agents.Add(Instantiate(prefab).GetComponent<Agent>());
            agents[i].SetNumOfGens(NumOfGensPerAgent);
        }
        fixedDelta = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Time.timeScale = speedOfSimulation;
        Time.fixedDeltaTime = fixedDelta / Time.timeScale;
        durationOfSimulation += Time.fixedDeltaTime;

        if (durationOfSimulation > Actions.durationOfGen){
            EndOfSimulation();
        }
    }

    private void EndOfSimulation(){
        List<Agent> olds = agents;
        
        List<Chromosome> population = new List<Chromosome>();
        for (int i = 0; i < agents.Count; i++){
            population.Add(agents[i].GetChromosome());
        }
        
        population = geneticAlg.Evolv(population);
        
        for (int i = 0; i < olds.Count; i++){
            Destroy(olds[i].gameObject);
		}

        for (int i = 0; i < agents.Count; i++){
            Agent a = Instantiate(prefab).GetComponent<Agent>();
            a = agents[i];
        }
        
        durationOfSimulation = 0;
    }
}