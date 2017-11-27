using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {
    private Chromosome chromosome;
    private Rigidbody2D rb;
    private float waitFor;
    private bool active;
    private int numOfActions;
    private int genIndex;
    public float force;
    private int points;

    void Awake(){
        genIndex = 0;
        active = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        waitFor += Time.fixedDeltaTime;
        
        if (active){
            if (waitFor >= chromosome.GetGenList()[genIndex].GetTime()){
                genIndex++;
                if (genIndex >= numOfActions)
                    active = false;
                waitFor = 0;
            }
            
            switch (chromosome.GetGenList()[genIndex].GetAction()){
                case 0:
                    rb.AddForce(Vector2.right * force * Time.fixedDeltaTime);
                    break;
                case 1:
                    rb.AddForce(-Vector2.right * force * Time.fixedDeltaTime);
                    break;
                case 2:
                    rb.AddForce(Vector2.up * force * Time.fixedDeltaTime);
                    break;
                case -1:
                    break;
            }
        }
    }

    public float GetPoints(){
        return chromosome.GetPoints();
    }

    public void AddPoints(float value){
        chromosome.AddPoints(value);
    }

    public void SetNumOfGens(int num){
        chromosome = new Chromosome(num);
        numOfActions = num;
        active = true;
    }

    public void SetChromosome(Chromosome chro){
        chromosome = chro;
        chromosome.RestartPoints();
        active = true;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        gameObject.SetActive(false);
    }

    public Chromosome GetChromosome(){
        return chromosome;
    }
}
