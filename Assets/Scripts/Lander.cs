using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lander : MonoBehaviour {
	private Transform trans;
	private List<Gen> gens = new List<Gen>();
	private int index;
    private Vector3 plataformPos;
	private float speed;
	public float fitness;
	private float waitFor;
	private bool active;
	private float force;

	void Start () {
		trans = transform;
		index = 0;
		force = 2f;
	}

	public void SetGenList(List<Gen> newGens){
		gens = newGens;
		index = 0;
		fitness = 0;
		active = true;
	}

	public List<Gen> GetGensList(){
		return gens;
	}
	
	public void UpdateLander (float dt) {
		if (active){
			waitFor += dt;
			if (waitFor >= gens[index].GetTime()){
        	        index++;
        	        if (index >= gens.Count)
        	        {
        	            active = false;
        	            return;
        	        }
        	        waitFor = 0;
			}

			switch (gens[index].GetAction()){
        	        case 0:
        	            trans.position += (trans.right * force * dt);
        	            break;
        	        case 1:
        	            trans.position += (-trans.right * force * dt);
        	            break;
        	        case 2:
        	            trans.position += (trans.up * force * dt);
        	            break;
        	        case -1:
        	            break;
        	    }

		}
        
		trans.position += (-trans.up * (force / 2) * dt);
       
        IncrementFitness(100 / Vector3.Distance(trans.position, plataformPos));
	}

	public void SetPlataformPos(Vector3 pos){
		plataformPos = pos;
	}

	public float GetFitness(){
		return fitness;
	}

	public void IncrementFitness(float val){
		fitness += val;
	}

    void OnCollisionEnter(Collision coll){
        if (coll.transform.tag == "Plataform"){
            IncrementFitness(100000);
        }
    }

    void OnCollisionStay(Collision coll){
        if (coll.transform.tag == "Plataform"){
            IncrementFitness(1000);
        }
    }
}
