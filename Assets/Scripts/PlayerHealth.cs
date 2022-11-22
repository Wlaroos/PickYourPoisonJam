using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{

    public event Action<float> HealthChanged = delegate { };
    public event Action<float> MaxHealthChanged = delegate { };

    [SerializeField] private float _maxHealth;
    public float MaxHealth => _maxHealth;

    private float _currentHealth;
    public float CurrentHealth => _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

//   Testing
     private void Update()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            TakeDamage(.1f);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Heal(5);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeMaxHealth(50);
        }
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Death();
        }
            HealthChanged?.Invoke(_currentHealth);
    }

    public void Heal(float amount)
    {
        _currentHealth += amount;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
            HealthChanged?.Invoke(_currentHealth);
    }

    public void ChangeMaxHealth(float newMaxHealth)
    {
        _maxHealth = newMaxHealth;
        if(_currentHealth > newMaxHealth)
        {
            _currentHealth = newMaxHealth;
        }
        MaxHealthChanged?.Invoke(newMaxHealth);
    }

    void Death()
    {
        gameObject.SetActive(false);
        // Death Code
    }
}
