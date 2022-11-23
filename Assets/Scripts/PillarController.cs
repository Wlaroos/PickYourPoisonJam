using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarController : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Bullet"){
            transform.Rotate(0,0,Random.Range(0,360));
        }
    }
}
