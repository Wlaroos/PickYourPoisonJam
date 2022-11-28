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
    public GameObject ringPrefab;
    [SerializeField] private float distToPlayer;
    public float atkCD = 1;
    public float atkTimer;
    public Transform playerTransform;
    PlayerHealth player;
    [SerializeField] private AIPath _AIPath;
    public bool anim;
    public GameObject dp;
    void Start(){
        _AIPath = GetComponent<AIPath>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update(){
        // if(atkCD >= 0){
        //     atkTimer -= Time.deltaTime;
        // }

        if(playerTransform != null)
        distToPlayer = Vector2.Distance(transform.position,playerTransform.position);
        else Debug.Log("Null player transform");

        if(Mathf.Abs(distToPlayer) > range || anim){
            _AIPath.maxSpeed = 0;
        }
        else
        _AIPath.maxSpeed = Spd;

        if(Mathf.Abs(distToPlayer) < dmgR && !anim && GameObject.FindObjectOfType<PlayerMovement>() != null)
        {
           Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            Explode();
        }
        
        if(other.tag == "Bullet"){
            Destroy();
        }
    }

    public void Explode(){
        atkTimer = atkCD;
        this.gameObject.GetComponent<Animator>().Play("Attack",-1,0);
        GameObject ring = Instantiate(ringPrefab,transform.position,Quaternion.identity);
        Destroy(ring,1f);
        // ci = GameObject.Find("Labyrinth").GetComponent<ColorInversion>();
        // ci.Flash();
        AudioManager.PlaySound("LabyrinthAttack");
        if(Mathf.Abs(distToPlayer) < dmgR){
            playerTransform.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);
        }
        StartCoroutine(AnimSet());
        //Destroy(gameObject);
    }
    public void Destroy(){
        if(dp != null){
        GameObject partiles = Instantiate(dp,transform.position,Quaternion.identity);
        Destroy(partiles,2f);
        }
        AudioManager.PlaySound("EnemyExplode");
        Destroy(gameObject);
    }
    public void SetMoveSpeed0(){
        anim = true;
        _AIPath.maxSpeed = 0;
    }

    public void ResetMoveSpeed(){
        anim = false;
        _AIPath.maxSpeed = Spd;
    }

    public IEnumerator AnimSet(){
        yield return new WaitForSeconds(1.6f);
        anim = false;
    }

}
