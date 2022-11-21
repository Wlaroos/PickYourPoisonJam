using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonTriggerVolume : MonoBehaviour
{
    public bool behindTrigger;
    public bool frontTrigger;
    public bool behind = false;

    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
        
            if(behindTrigger){
                this.gameObject.transform.parent.gameObject.GetComponent<PrisonController>().BehindWall();   
            }
            
            else if(frontTrigger){
                this.gameObject.transform.parent.gameObject.GetComponent<PrisonController>().InFrontOfWall();
            }
        
        }
    }
}
