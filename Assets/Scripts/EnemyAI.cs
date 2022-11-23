using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private ColorInversion ci;
    //private AIDestinationSetter mover;
    public float range;

    private float distToPlayer;

    public Transform playerTransform;

    void Start(){
        
    }

    void Update(){
        if(playerTransform != null)
        distToPlayer = Vector2.Distance(transform.position,playerTransform.position);
        else Debug.Log("Null player transform");

        if(distToPlayer > range){
            //mover.enabled = false;
        }
        else{}
        //mover.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" || other.tag == "Bullet"){
            Explode();
        }
    }

    public void Explode(){
        ci = GameObject.Find("Labyrinth").GetComponent<ColorInversion>();
        ci.Flash();
        AudioManager.PlaySound("EnemyExplode");
        Destroy(gameObject);
    }

}
