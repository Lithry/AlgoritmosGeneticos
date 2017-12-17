using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
	public GameObject agent;
	public int agentNum;
	private List<Lander> agents = new List<Lander>();
	public GameObject plataformPrefab;
	public int elitesNum;
	public int numOfGens;
	[Range(0.00f, 1.00f)]
	public float mutation;
	public int durationOfGeneration;
	[Range(1, 50)]
	public int iterations;
	private float timer;
	private GeneticAlg ga;
	private int generation;
	public Text generationText;
	public Text timerText;
	private Vector3 landerBeginPos;

	void Awake () {
		generation = 1;
		timer = 0.0f;
		
		GameObject plataform = Instantiate(plataformPrefab, new Vector3(-10, 0, -10), Quaternion.Euler(90, 0, 0));

        landerBeginPos = new Vector3(10, 0, 10);
		for (int i = 0; i < agentNum; i++){
            GameObject obj = Instantiate(agent, landerBeginPos, Quaternion.Euler(90, 0, 0));
			Lander lan = obj.GetComponent<Lander>();
			
			lan.SetPlataformPos(plataform.transform.position);
			
			List<Gen> gens = new List<Gen>();
			for (int j = 0; j < numOfGens; j++)
			{
				Gen g = new Gen();
				gens.Add(g);
			}
			lan.SetGenList(gens);

			if (i == 0)
				ga = new GeneticAlg(elitesNum, agentNum, numOfGens, mutation);
			
			agents.Add(lan);
		}
	}
	
	void FixedUpdate () {
		generationText.text = "Generation: " + generation.ToString();
		timerText.text = "Time: " + timer.ToString("F2");
		for (int i = 0; i < iterations; i++){
			for (int j = 0; j < agents.Count; j++){
				agents[j].UpdateLander(Time.fixedDeltaTime);
			}
			timer += Time.fixedDeltaTime;
		}
		if (timer > durationOfGeneration)
			Evolve();
	}

	private void Evolve(){
		float maxFitness = agents[0].GetFitness();
		List<Chromosome> chromList = new List<Chromosome>();
		for (int i = 0; i < agents.Count; i++){
			Chromosome c = new Chromosome();
			c.fitness = agents[i].GetFitness();
			if (maxFitness < c.fitness)
				maxFitness = c.fitness;
			c.weights = agents[i].GetGensList();
			chromList.Add(c);
		}
		Debug.Log("Gen " + generation.ToString() + " max Fitness: " + maxFitness.ToString("F0"));
		chromList = ga.Evolv(chromList);

		for (int i = 0; i < agents.Count; i++)
		{
			agents[i].SetGenList(chromList[i].weights);
		}

		for (int i = 0; i < agents.Count; i++)
		{
			agents[i].transform.position = landerBeginPos;
		}

		timer = 0.0f;
		generation++;
	}
}
