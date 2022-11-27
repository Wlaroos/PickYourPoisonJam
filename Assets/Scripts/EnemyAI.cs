using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{
    private ColorInversion ci;
    //private AIDestinationSetter mover;
    public float range;
    public float dmgR;
    public float Spd;
    [SerializeField] private float distToPlayer;

    public Transform playerTransform;
    PlayerHealth player;
    [SerializeField] private AIPath _AIPath;
    void Start(){
        _AIPath = GetComponent<AIPath>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update(){
        if(playerTransform != null)
        distToPlayer = Vector2.Distance(transform.position,playerTransform.position);
        else Debug.Log("Null player transform");

        if(Mathf.Abs(distToPlayer) > range){
            _AIPath.maxSpeed = 0;
        }
        else
        _AIPath.maxSpeed = Spd;
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
        if(Mathf.Abs(distToPlayer) < dmgR){
            playerTransform.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);
        }
        Destroy(gameObject);
    }

}
