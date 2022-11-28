using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.StopSound("UnderbellyMusic");
        AudioManager.StopSound("LabyrinthAmbience");
    }

   
}
