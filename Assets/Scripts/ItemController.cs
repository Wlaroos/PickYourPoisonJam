using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Animator itemAnimation;
    public Sprite weaponSprite;
    public float pickupRadius;

    void Start(){
        gameObject.GetComponentInChildren<CircleCollider2D>().radius = pickupRadius;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = weaponSprite;
    }
    void OnTriggerEnter2D(Collider other){
        if(other.tag == "Player"){
            Destroy(this.gameObject,0f);
            AudioManager.PlaySound("ItemPickup");
        }
    }
}
