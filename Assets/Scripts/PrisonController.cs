using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using DG.Tweening;

public class PrisonController : MonoBehaviour
{
    public Transform camTransform;
    public GameObject prisonFloor;
    public Sprite prisonFloorRubbleSprite;
    public GameObject prisonWall;
    public GameObject prisonWallBroken;
    public GameObject flashImage;

    public bool behindWall = false;

    void Start(){
        
    }

    public void BreakWall(){
        prisonWall.SetActive(false);
        prisonWallBroken.SetActive(true);
        prisonFloor.GetComponent<SpriteRenderer>().sprite = prisonFloorRubbleSprite;
        flashImage.GetComponent<Animator>().Play("Flash");
        CameraShaker.Instance.ShakeOnce(5f,6f,0.1f,1f);
        AudioManager.PlaySound("Explosion");
    }

    public void BehindWall(){
        TweenCamera(camTransform,10.7f);
    }

    public void InFrontOfWall(){
        TweenCamera(camTransform,0f);
    }

    public void TweenCamera(Transform t,float yval){ //10,7f for top room, 0 for bottom
        Sequence mySequence = DOTween.Sequence();
        t.DOMove(new Vector3(0f,yval,-10f),0.9f).SetEase(Ease.InOutQuint);
    }
}
