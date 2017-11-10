using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen {
    private int action;
    private float time;
    
    public Gen(){
        action = Random.Range(0, (int)Actions.Action.Total);
        time = Random.Range(Actions.MinTime, Actions.MaxTime);
    }

    public Gen(int action, float time){
        this.action = action;
        this.time = time;
    }

    public int GetAction(){
        return action;
    }

    public float GetTime(){
        return time;
    }
}
