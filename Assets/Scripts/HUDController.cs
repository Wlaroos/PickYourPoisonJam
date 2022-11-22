using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    [SerializeField] PlayerHealth _playerHealthRef;
    [SerializeField] PlayerWeapon _playerWepRef;

    [SerializeField] Slider _healthSlider;


    private void Awake()
    {
        _healthSlider.maxValue = _playerHealthRef.MaxHealth;
        _healthSlider.minValue = -_playerHealthRef.MaxHealth / 10;
    }

    private void OnEnable()
    {
        _playerHealthRef.HealthChanged += OnHealthChanged;
        _playerHealthRef.MaxHealthChanged += OnMaxHealthChanged;
        _playerWepRef.Fired += OnFired;
    }

    private void OnDisable()
    {
        _playerHealthRef.HealthChanged -= OnHealthChanged;
        _playerHealthRef.MaxHealthChanged -= OnMaxHealthChanged;
        _playerWepRef.Fired -= OnFired;
    }

    void OnHealthChanged(float health)
    {
        _healthSlider.value = health;
    }

    void OnMaxHealthChanged(float newMaxHealth)
    {
        _healthSlider.GetComponent<RectTransform>().sizeDelta = new Vector2((newMaxHealth / _healthSlider.maxValue) * _healthSlider.GetComponent<RectTransform>().sizeDelta.x, 60);
        _healthSlider.maxValue = _playerHealthRef.MaxHealth;
        _healthSlider.minValue = -_playerHealthRef.MaxHealth / 10;
        _healthSlider.value = _playerHealthRef.CurrentHealth;
    }

    void OnFired()
    {

    }
}
