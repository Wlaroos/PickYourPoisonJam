using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PrisonController : MonoBehaviour
{
    public GameObject prisonFloor;
    public Sprite prisonFloorRubbleSprite;
    public GameObject prisonWall;
    public GameObject prisonWallBroken;
    public GameObject flashImage;

    void Update(){

    }

    public void BreakWall(){
        prisonWall.SetActive(false);
        prisonWallBroken.SetActive(true);
        prisonFloor.GetComponent<SpriteRenderer>().sprite = prisonFloorRubbleSprite;
        flashImage.GetComponent<Animator>().Play("Flash");
        CameraShaker.Instance.ShakeOnce(5f,6f,0.1f,1f);
    }

    public void BehindWall(){
        //Player Sorting layer behind wall
    }

    public void InFrontOfWall(){
        //Player Sorting layer in front of wall
    }

    public void OnTriggerEnter2D(Collider other){
        //Detect when player behind wall
    }
}
