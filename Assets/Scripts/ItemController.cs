using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ItemController : MonoBehaviour
{
    public Sprite weaponSprite;
    public float pickupRadius;

    public bool isTeleporter;
    private bool pickedUp;

    public GameObject titleCard;
    public GameObject title;
    public GameObject description;
    public GameObject instruction;
    public GameObject flashImage;

    public string levelToLoad;

    

    void Start(){
        gameObject.GetComponentInChildren<CircleCollider2D>().radius = pickupRadius;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = weaponSprite;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(!pickedUp){
                pickedUp = true;
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = null;
                GameObject ItemShadow;
                ItemShadow = GetComponentInChildren<ItemShadow>().gameObject;
                Destroy(ItemShadow);
                if(!isTeleporter)
                    AudioManager.PlaySound("ItemPickup");
                if(isTeleporter)
                    StartCoroutine(Teleport());
            }
        }
    }

    public IEnumerator Teleport(){
        flashImage.GetComponent<Animator>().Play("Flash",-1,0);
        titleCard.SetActive(true);
        yield return new WaitForSeconds(1f);
        title.GetComponent<Animator>().Play("CardIn");
        yield return new WaitForSeconds(0.6f);
        description.GetComponent<Animator>().Play("CardIn");
        yield return new WaitForSeconds(0.6f);
        instruction.GetComponent<Animator>().Play("CardIn");
        yield return new WaitForSeconds(4f);
        title.GetComponent<Animator>().Play("CardOut");
        description.GetComponent<Animator>().Play("CardOut");
        instruction.GetComponent<Animator>().Play("CardOut");
        yield return new WaitForSeconds(2f);
        flashImage.GetComponent<Animator>().Play("Flash");
        yield return new WaitForSeconds(0.1f);
        Application.LoadLevel(levelToLoad);
        Destroy(this.gameObject,0f);
    }
}
