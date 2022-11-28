using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderbllyMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AudioManager.PlayFade("UnderbellyMusic",5,0,0.2f));
    }

}
