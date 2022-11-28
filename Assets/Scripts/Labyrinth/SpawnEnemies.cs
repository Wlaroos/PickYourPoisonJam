using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public float timer;
    public float time;
    public Transform[] spawns;
    public Transform distTransform;
    public GameObject enemyPrefab;
    public Transform playerTransform;

    public float spawnR = 10;
    [SerializeField] private float distToPlayer;
    public void SpawnEnemy(){
        AudioManager.PlaySound("SpawnEnemy");   
        timer = Random.Range(3,9);
        time = timer;
        Instantiate(enemyPrefab, spawns[Random.Range(0,spawns.Length)].position,Quaternion.identity);
    }
    void Start(){
         playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update(){
        if(playerTransform != null)
        distToPlayer = Vector2.Distance(distTransform.position,playerTransform.position);


        time-=Time.deltaTime;
        if(time<=0 && distToPlayer < spawnR){
            SpawnEnemy();
        }

    }

}
