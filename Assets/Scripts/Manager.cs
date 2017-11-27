using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    private List<Agent> agents = new List<Agent>();
    private List<float> agentsLastDistance = new List<float>();
    private Vector3 beginPos;
    public GameObject target;
    private GeneticAlg geneticAlg;
    public GameObject prefab;
    public int NumOfAgents;
    public int NumOfGensPerAgent;
    public int numOfElites;
    public float mutationProbability;
    public float durationOfGen;
    public float timer = 0.0f;
    [Range(1.0f, 5.0f)]
    public float speedOfSimulation = 1.0f;
    public float pointsPerDistance;
    public float pointsPerFly;
    private float fixedDelta;

    // Use this for initialization
    void Awake() {
        geneticAlg = new GeneticAlg(numOfElites, NumOfAgents, NumOfGensPerAgent, mutationProbability);
        beginPos.Set(-5, 2, 0);

        for (int i = 0; i < NumOfAgents; i++) {
            agents.Add(Instantiate(prefab).GetComponent<Agent>());
            agents[i].SetNumOfGens(NumOfGensPerAgent);
            float a = 100.0f;
            agentsLastDistance.Add(a);
        }
        fixedDelta = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Time.timeScale = speedOfSimulation;
        Time.fixedDeltaTime = fixedDelta / Time.timeScale;
        timer += Time.fixedDeltaTime;

        for (int i = 0; i < agents.Count; i++)
        {
            if (agents[i].transform.position.y > target.transform.position.y && Vector3.Distance(agents[i].transform.position, target.transform.position) < agentsLastDistance[i]){
                agentsLastDistance[i] = Vector3.Distance(agents[i].transform.position, target.transform.position);
                agents[i].AddPoints(pointsPerDistance);
            }
            /*if (agents[i].gameObject.activeSelf)
                agents[i].AddPoints(pointsPerFly);*/
        }

        if (timer > durationOfGen){
            EndOfSimulation();
        }
    }

    private void EndOfSimulation(){
        ClearDistanceList();

        List<Agent> olds = agents;
        
        List<Chromosome> population = new List<Chromosome>();
        for (int i = 0; i < olds.Count; i++){
            population.Add(olds[i].GetChromosome());
        }
        
        population = geneticAlg.Evolv(population);
        
        for (int i = 0; i < olds.Count; i++){
            Destroy(olds[i].gameObject);
		}
        olds.Clear();
        agents.Clear();
        for (int i = 0; i < NumOfAgents; i++){
            Agent a = Instantiate(prefab).GetComponent<Agent>();
            a.SetChromosome(population[i]);
            agents.Add(a);
        }
        
        timer = 0;
    }

    private void ClearDistanceList(){
        for (int i = 0; i < agentsLastDistance.Count; i++)
        {
            agentsLastDistance[i] = 100.0f;
        }
    }
}