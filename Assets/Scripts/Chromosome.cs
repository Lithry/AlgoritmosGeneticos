using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromosome {
    private List<Gen> gens = new List<Gen>();
    private int index;

    public Chromosome(int numOfGens){
        for (int i = 0; i < numOfGens; i++){
            Gen a = new Gen();
            gens.Add(a);
        }
        index = 0;
    }

    public void NextGen(){
        if (index < gens.Count - 1)
            index++;
        else
            index = -1;
    }

    public int GetCurrentGenAction(){
        if (index != -1)
            return gens[index].GetAction();
        else
            return -1;
    }

    public float GetCurrentGenTime(){
        if (index != -1)
            return gens[index].GetTime();
        else
            return 1000.0f;
    }

}
