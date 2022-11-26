using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PillarController : MonoBehaviour
{
    void Start(){
         
    }


    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Bullet"){
            transform.Rotate(0,0,Random.Range(0,360));
            AudioManager.PlaySound("PillarDamage");
        }
    }
}
