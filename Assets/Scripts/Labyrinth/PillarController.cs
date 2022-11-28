using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PillarController : MonoBehaviour
{
    [SerializeField] PillarWinCondition _winRef;
    [SerializeField] private int _maxHealth = 4;
    private int _currentHealth;

    [SerializeField] ParticleSystem _pillarExplosion;

    private void Awake()
    {
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
            
            _currentHealth -= 1;
            if(_currentHealth <= 0)
            {
                Death();
            }
            else AudioManager.PlaySound("PillarDamage");
        }
    }

    void Death()
    {
        _winRef.Decrease();
        AudioManager.PlaySound("PillarDestroyed");
        GameObject particles = Instantiate(_pillarExplosion, transform.position, Quaternion.Euler(0,0,0)).gameObject;
        Destroy(particles, 3.5f);
        gameObject.SetActive(false);
    }
}
