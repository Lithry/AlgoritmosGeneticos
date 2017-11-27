using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromosome {
    private List<Gen> gens = new List<Gen>();
    private float points;
    
    public Chromosome(){
        points = 0.0f;
    }

    public Chromosome(int numOfGens){
        points = 0.0f;
        for (int i = 0; i < numOfGens; i++){
            Gen a = new Gen();
            gens.Add(a);
        }
    }

    public List<Gen> GetGenList(){
        return gens;
    }

    public void AddGen(Gen gen){
        gens.Add(gen);
    }

    public float GetPoints(){
        return points;
    }

    public void AddPoints(float p){
        points += p;
    }

    public void RestartPoints(){
        points = 0;
    }
}
