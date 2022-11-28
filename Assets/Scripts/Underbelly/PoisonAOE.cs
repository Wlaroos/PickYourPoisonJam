using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PoisonAOE : MonoBehaviour
{
    [SerializeField] float _damage = 3;
    [SerializeField] float _scale = 10;

    private void Awake()
    {
        transform.localScale = Vector3.one * _scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CameraShaker.Instance.ShakeOnce(3f, 2f, 0.2f, 0.2f);
            collision.GetComponent<PlayerHealth>().ChangeDOT(_damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().DelayedDetox(-_damage);
        }
    }
}
