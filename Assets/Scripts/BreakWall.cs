using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour
{
    // Start is called before the first frame update
  void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Bullet"){
            Destroy(other.gameObject,0f);
            AudioManager.PlaySound("BulletCollide");
            this.gameObject.transform.parent.GetComponent<PrisonController>().BreakWall();
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
