using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PillarController : MonoBehaviour
{
    private static int _pillarAmount = 0;
    [SerializeField] private int _maxHealth = 4;
    private int _currentHealth;

    [SerializeField] ParticleSystem _pillarExplosion;

    private void Awake()
    {
        _pillarAmount += 1;
        _currentHealth = _maxHealth;
    }

/*    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Death();
        }
    }*/

    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Bullet"){
            transform.Rotate(0,0,Random.Range(0,360));
            AudioManager.PlaySound("PillarDamage");
            _currentHealth -= 1;
            if(_currentHealth <= 0)
            {
                Death();
            }
        }
    }

    void Death()
    {
        _pillarAmount -= 1;

        if (_pillarAmount <= 0)
        {
            GameObject.FindObjectOfType<WinLossController>().WinEvent();
        }

        GameObject particles = Instantiate(_pillarExplosion, transform.position, Quaternion.Euler(0,0,0)).gameObject;
        Destroy(particles, 2f);
        gameObject.SetActive(false);
    }
}
