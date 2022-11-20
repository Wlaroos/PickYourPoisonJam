using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector2 _input;
    Vector2 _dampedInput;
    Vector2 _currentVelocity;

    Rigidbody2D _rb;

    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _smoothSpeed = 0.2f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");
        _dampedInput = Vector2.SmoothDamp(_dampedInput, _input, ref _currentVelocity, _smoothSpeed);
    }

    private void FixedUpdate()
    {
        moveCharacter(_dampedInput);
    }

    void moveCharacter(Vector2 direction)
    {
        _rb.velocity = direction * _moveSpeed;
    }
}
