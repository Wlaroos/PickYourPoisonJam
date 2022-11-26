using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonTriggerVolume : MonoBehaviour
{
    public bool behindTrigger;
    public bool frontTrigger;
    public bool behind = false;

    private GameObject wallBroken;
    private GameObject props;

    private PrisonController pc;

    public void Start(){
        pc =  this.gameObject.transform.parent.gameObject.GetComponent<PrisonController>();
        props = GameObject.Find("Props");
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(wallBroken == null){
                    wallBroken = GameObject.Find("WallBroken");
            }
            
            pc.BehindWall();   
            wallBroken.GetComponent<SpriteRenderer>().sortingOrder = 2;
            props.GetComponent<SpriteRenderer>().sortingOrder = 3;
            AudioManager.PlaySound("CameraUp");
        }
    }
    
    public void OnTriggerExit2D(Collider2D other){
         if(other.tag == "Player"){
            if(wallBroken == null){
                wallBroken = GameObject.Find("WallBroken");
            }
            
            pc.InFrontOfWall();
            wallBroken.GetComponent<SpriteRenderer>().sortingOrder = -2;
            props.GetComponent<SpriteRenderer>().sortingOrder = -1;
            AudioManager.PlaySound("CameraDown");
        }
    }

}
