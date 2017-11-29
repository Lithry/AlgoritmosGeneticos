using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlg {
	private float totalPoints;
	private int elites;
	private int agents;
	private int gens;
	private float mutation;
    public GeneticAlg(int numOfElites, int numOfAgents, int numOfGensPerAgents, float mutationProbability){
		elites = numOfElites;
		agents = numOfAgents;
		gens = numOfGensPerAgents;
		mutation = mutationProbability;
	}

	public void Crossour(Chromosome dad, Chromosome mom, out Chromosome child1, out Chromosome child2){
		Chromosome nChro1 = new Chromosome();
		Chromosome nChro2 = new Chromosome();

		int pivot = Random.Range(0, dad.GetGenList().Count);

		for (int i = 0; i < pivot; i++)
		{
			Gen nGen1 = new Gen(dad.GetGenList()[i].GetAction(), dad.GetGenList()[i].GetTime());
			Gen nGen2 = new Gen(mom.GetGenList()[i].GetAction(), mom.GetGenList()[i].GetTime());

			nChro1.AddGen(nGen1);
			nChro2.AddGen(nGen2);
		}	

		for (int i = pivot; i < gens; i++)
		{
			Gen nGen1 = new Gen(mom.GetGenList()[i].GetAction(), mom.GetGenList()[i].GetTime());
			Gen nGen2 = new Gen(dad.GetGenList()[i].GetAction(), dad.GetGenList()[i].GetTime());
			
			nChro1.AddGen(nGen1);
			nChro2.AddGen(nGen2);
		}	
		/*for (int i = 0; i < gens; i++)
		{
			Gen nGen1 = new Gen(dad.GetGenList()[i].GetAction(), mom.GetGenList()[i].GetTime());
			Gen nGen2 = new Gen(mom.GetGenList()[i].GetAction(), dad.GetGenList()[i].GetTime());
			if (i % 2 == 0){
				nChro1.AddGen(nGen1);
				nChro2.AddGen(nGen2);
			}
			else{
				nChro1.AddGen(nGen2);
				nChro2.AddGen(nGen1);
			}
			
		}*/
		child1 = nChro1;
		child2 = nChro2;
	}

	public Chromosome Roulette(List<Chromosome> pop){
		totalPoints = 0;
		for(int i = 0; i < pop.Count; i++){
			totalPoints += pop[i].GetPoints();
		}
		float rnd = Random.Range(0, totalPoints);

		float points = 0;

		for(int i = 0; i < pop.Count; i++){
			points += pop[i].GetPoints();
			if (points >= rnd)
				return pop[i];
		}
		return null;
	}

	public List<Chromosome> Mutation(List<Chromosome> population){
		float rnd;

		for (int i = 0; i < population.Count; i++){
			List<Gen> gens = population[i].GetGenList();
			for (int j = 0; j < gens.Count; j++){
				rnd = Random.Range(0.0f, 1.0f);
				if (rnd < mutation){
					float mutTime = Random.Range(-0.2f, 0.2f);
					gens[j].MutateTime(mutTime);
				}
			}
		}

		return population;
	}

	public List<Chromosome> Evolv(List<Chromosome> oldPopulation){
		List<Chromosome> population = new List<Chromosome>();
		oldPopulation.Sort(Compare);

		for (int i = 0; i < elites; i++){
			population.Add(oldPopulation[i]);
		}

		while (population.Count < agents)
		{
			Chromosome a1 = Roulette(oldPopulation);
			Chromosome a2 = Roulette(oldPopulation);
			Chromosome c1;
			Chromosome c2;
			Crossour(a1, a2, out c1, out c2);
			population.Add(c1);
			if (population.Count < agents)
				population.Add(c2);
		}
		
		population = Mutation(population);

		return population;
	}

	private int Compare(Chromosome a1, Chromosome a2){
		if (a1.GetPoints() > a2.GetPoints())
			return -1;
		else if (a1.GetPoints() < a2.GetPoints())
			return 1;
		else
			return 0;
	}
}
