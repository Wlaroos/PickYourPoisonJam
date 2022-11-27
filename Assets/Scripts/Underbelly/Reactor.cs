using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour
{

    [SerializeField] Transform _bar1;
    [SerializeField] Transform _bar2;

    [SerializeField] BigWall wallRef;

    [SerializeField] ParticleSystem explodeParticles;

    private Vector2 _scale;

    [SerializeField] private float _amountNeeded;
    private float _amountCurrent;

    private bool _ready = false;

    private void Awake()
    {
        _scale = _bar1.localScale;
    }

    public void Increase()
    {
        if (_amountCurrent < _amountNeeded)
        {
            _amountCurrent++;

            if (_amountCurrent >= _amountNeeded) { _ready = true; }
                
            _scale.y = (_amountCurrent / _amountNeeded) * 4.6875f;
            _bar1.localScale = _scale;
            _bar2.localScale = _scale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet" && _ready)
        {
            Explode();
        }
    }

    void Explode()
    {
        // Add flash of white and open the next area
        wallRef.Break();
        Instantiate(explodeParticles, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }
}
