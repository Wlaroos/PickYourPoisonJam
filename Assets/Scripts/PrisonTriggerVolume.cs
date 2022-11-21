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

    public void Start(){
        
        props = GameObject.Find("Props");
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
        
            if(behindTrigger){
                if(wallBroken == null){
                    wallBroken = GameObject.Find("WallBroken");
                }
                this.gameObject.transform.parent.gameObject.GetComponent<PrisonController>().BehindWall();   
                wallBroken.GetComponent<SpriteRenderer>().sortingOrder = 2;
                props.GetComponent<SpriteRenderer>().sortingOrder = 3;
            }
            
            else if(frontTrigger){
                if(wallBroken == null){
                    wallBroken = GameObject.Find("WallBroken");
                }
                this.gameObject.transform.parent.gameObject.GetComponent<PrisonController>().InFrontOfWall();
                wallBroken.GetComponent<SpriteRenderer>().sortingOrder = -2;
                props.GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
        
        }
    }
}
