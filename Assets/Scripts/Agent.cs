using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {
    private Chromosome chromosome;
    private Rigidbody2D rb;
    private float waitFor;
    public int numOfActions;
    public float force;

    void Awake(){
        chromosome = new Chromosome(numOfActions);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        waitFor += Time.deltaTime;
        if (waitFor >= chromosome.GetCurrentGenTime()){
            chromosome.NextGen();
            waitFor = 0;
        }

        switch (chromosome.GetCurrentGenAction()){
            case 0:
                rb.AddForce(Vector2.right * force * Time.deltaTime);
                break;
            case 1:
                rb.AddForce(-Vector2.right * force * Time.deltaTime);
                break;
            case 2:
                rb.AddForce(Vector2.up * force * Time.deltaTime);
                break;
            case -1:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        gameObject.SetActive(false);
    }
}
