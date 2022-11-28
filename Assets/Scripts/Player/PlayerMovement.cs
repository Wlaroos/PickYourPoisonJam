using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerMovement : MonoBehaviour
{

    private Vector2 _input;
    private Vector2 _dampedInput;
    private Vector2 _currentVelocity;

    private Rigidbody2D _rb;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _smoothSpeed = 0.2f;

    public bool allowInput = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (allowInput)
        {
            Inputs();
        }
    }

    void Inputs()
    {
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");
        _dampedInput = Vector2.SmoothDamp(_dampedInput, _input, ref _currentVelocity, _smoothSpeed);

        moveCharacter(_dampedInput);
    }

    void moveCharacter(Vector2 direction)
    {
        _rb.velocity = direction * _moveSpeed;
    }

    public void Force(Vector2 dir)
    {
        CameraShaker.Instance.ShakeOnce(7.5f, 4f, 0.2f, 0.2f);
        _rb.AddForce(dir * 750);

    }
}
