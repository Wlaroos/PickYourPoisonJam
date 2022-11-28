using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnableTrigger : MonoBehaviour
{
    [SerializeField] GameObject _enableRef;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_enableRef.active)
        _enableRef.SetActive(true);
    }
}
