using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using DG.Tweening;

public class LabyrinthController : MonoBehaviour
{
    public Transform camTransform;
    public GameObject prisonFloor;
    public Sprite prisonFloorRubbleSprite;
    public GameObject prisonWall;
    public GameObject prisonWallBroken;

    public Transform camMoveLocation;
    public Transform camMoveLocation2;
    public Vector3 newPos;
    public float duration;
    public bool hasExitAction;
    public bool overridePos;

    void Start(){
      
    }

    public void TweenCamera(Transform t,Transform loc){ //10,7f for top room, 0 for bottom
        Sequence mySequence = DOTween.Sequence();
        t.DOMove(new Vector3(loc.position.x,loc.position.y,camTransform.position.z),duration).SetEase(Ease.InOutQuint);
    }
    
    public void TweenCameraOverride(Transform t,Vector3 loc){ //10,7f for top room, 0 for bottom
        Sequence mySequence = DOTween.Sequence();
        t.DOMove(loc,duration).SetEase(Ease.InOutQuint);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            TweenCamera(camTransform,camMoveLocation);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(hasExitAction){
            if(other.tag == "Player"){
                if(!overridePos)
                TweenCamera(camTransform,camMoveLocation2);
                else
                {
                    TweenCameraOverride(camTransform,newPos);
                }
               }
        }
    }

    
}
