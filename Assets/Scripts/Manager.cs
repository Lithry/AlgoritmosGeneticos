using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    private List<Agent> agents = new List<Agent>();
    private Vector3 beginPos;
    private GeneticAlg geneticAlg = new GeneticAlg();
    public GameObject prefab;
    public int NumOfAgents;
    public float durationOfSimulation = 0.0f;

    // Use this for initialization
    void Awake() {
        beginPos.Set(-5, 2, 0);
        for (int i = 0; i < NumOfAgents; i++) {
            agents.Add(Instantiate(prefab).GetComponent<Agent>());
        }
    }

    // Update is called once per frame
    void Update() {
        durationOfSimulation += Time.deltaTime;

        if (durationOfSimulation > Actions.durationOfGen){
            EndOfSimulation();
        }
    }

    private void EndOfSimulation(){
        durationOfSimulation = 0;
    }
}