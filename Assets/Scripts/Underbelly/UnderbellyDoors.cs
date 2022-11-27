using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnderbellyDoors : MonoBehaviour
{
    [SerializeField] Sprite[] _sprites;
    private SpriteRenderer _sr;
    private BoxCollider2D _boxCollider;
    private CircleCollider2D _triggerCollider;

    [SerializeField] private float _maxHealth = 100;
    private float _currentHealth;
    private float _DoT = 0;

    [SerializeField] ParticleSystem _initalBreakPS;
    [SerializeField] ParticleSystem _lockPS;
    [SerializeField] ParticleSystem _finalBreakPS;

    [SerializeField] AstarPath _pathfinder;

    bool _initial = false;
    bool _final = false;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _triggerCollider = GetComponent<CircleCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _currentHealth = _maxHealth;
        _sr.sortingOrder = 0;
    }

    private void Update()
    {
        _currentHealth -= (_DoT * Time.deltaTime);

        if(_currentHealth <= (_maxHealth / 2))
        {
            _sr.sprite = _sprites[1];
            if (_initalBreakPS != null && !_initial)
            {
                Instantiate(_initalBreakPS, transform.position, Quaternion.Euler(0, 0, 210));
                _initial = true;
            }

        }

        if (_currentHealth <= 0)
        {
            _sr.sprite = _sprites[2];
            _sr.sortingOrder = 3;

            _boxCollider.enabled = false;
            _triggerCollider.enabled = false;

            if (_finalBreakPS != null && !_final)
            {
                Instantiate(_initalBreakPS, transform.position, Quaternion.Euler(0,0,210));
                Instantiate(_lockPS, transform.position, Quaternion.Euler(0,0,210));
                Instantiate(_finalBreakPS, transform.position, Quaternion.Euler(0, 0, 60));

                _pathfinder.Scan(_pathfinder.graphs);

                transform.GetChild(0).gameObject.SetActive(false);

                _final = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet" && !_final)
        {
            _DoT += 10;

            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "x" + (int)(_DoT / 10);

            collision.GetComponent<PlayerBullets>().Destroy();
        }
    }
}
