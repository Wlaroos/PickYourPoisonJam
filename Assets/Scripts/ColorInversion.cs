using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInversion : MonoBehaviour
{
    public GameObject[] sceneSprite;
    [SerializeField] private Color lerpColorScene = Color.white;

    public SpriteRenderer floorSprite;
    [SerializeField] private Color lerpColorFloor = Color.black;


    public SpriteRenderer[] characterSprite;
    [SerializeField] private Color lerpColorCharacter = Color.white;
    private float timeElapsed;
    [SerializeField] float lerpSceneDuration;
    [SerializeField] float lerpFloorDuration;
    [SerializeField] float lerpCharDuration;
    
    void Start(){
        Flash();
    }
    
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(lerpColorScene != Color.white){
            for(int i = 0; i < sceneSprite.Length; i++){
                if(timeElapsed < lerpSceneDuration){
                    if(sceneSprite != null)
                    sceneSprite[i].GetComponent<SpriteRenderer>().color = Color.Lerp(sceneSprite[i].GetComponent<SpriteRenderer>().color,Color.white,timeElapsed/lerpSceneDuration);
                }
                else
                    sceneSprite[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        
        if(lerpColorFloor != Color.black){
            if(timeElapsed < lerpFloorDuration){
                floorSprite.color = Color.Lerp(floorSprite.color,Color.black,timeElapsed/lerpFloorDuration);
            }  
            else
                 floorSprite.color = Color.black;
        }
        
        if(lerpColorCharacter != Color.white){
            for(int i = 0; i < characterSprite.Length; i++){
                if(timeElapsed < lerpCharDuration){
                    if(characterSprite != null)
                characterSprite[i].color = Color.Lerp(characterSprite[i].color,Color.white,timeElapsed/lerpCharDuration);
            }
            else characterSprite[i].color = Color.white;
            }
        }
    }

    public void Flash(){
        Debug.Log("flash");
        timeElapsed = 0;
        lerpColorScene = Color.black;
        lerpColorCharacter = Color.black;
        lerpColorFloor = Color.white;
        for(int i = 0; i < sceneSprite.Length; i++){
                sceneSprite[i].GetComponent<SpriteRenderer>().color = lerpColorScene;
            }
        for(int i = 0; i < characterSprite.Length; i++){
                characterSprite[i].color = lerpColorCharacter;
            }
                floorSprite.color = lerpColorFloor;
    }
}
