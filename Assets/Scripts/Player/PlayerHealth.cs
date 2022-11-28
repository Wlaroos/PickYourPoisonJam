using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    public event Action<float> HealthChanged = delegate { };
    public event Action<float> MaxHealthChanged = delegate { };

    [SerializeField] private float _maxHealth;
    public float MaxHealth => _maxHealth;

    private float _currentHealth;
    public float CurrentHealth => _currentHealth;

    private float _DoT = 0;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

     private void Update()
    {
        /*        
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
        */

        if (_DoT > 0)
        {
            _currentHealth -= (_DoT * Time.deltaTime);

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Death();
            }

            HealthChanged?.Invoke(_currentHealth);
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
        GameObject.FindObjectOfType<WinLossController>().LoseEvent();
        gameObject.SetActive(false);
        // Death Code
    }

    public void ChangeDOT(float amount)
    {
        _DoT += amount;

        if (amount > 0)
        { /*AudioManager.PlaySound("PoisonMeterFill");*/ }
        else
        { /*AudioManager.PlaySound("PoisonMeterDeplete");*/ }

        transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        transform.GetChild(2).GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
        transform.GetChild(2).GetChild(0).GetComponent<TextMeshPro>().text = "x" + (int)(_DoT / 2);

        if(_DoT <= 0)
        {
            transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            transform.GetChild(2).GetChild(0).GetComponent<TextMeshPro>().color = Color.clear;
        }
        else
        {
            transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            transform.GetChild(2).GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
        }
    }

    public void DelayedDetox(float amount)
    {
        StartCoroutine(Delayed(amount));
    }

    IEnumerator Delayed(float amount)
    {
        yield return new WaitForSeconds(2f);
        ChangeDOT(amount);
    }
}
