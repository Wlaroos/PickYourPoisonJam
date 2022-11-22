using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    PlayerWeapon _wepRef;

    //[SerializeField] float _degreesPerSecond = 600;
    [SerializeField] float _scalePerSecond = 25;
    [SerializeField] float _scaleTo = 0.33f;

    //Quaternion initRot;
    Vector3 _defaultScale;

    void Awake()
    {
        _wepRef = FindObjectOfType<PlayerWeapon>();
        Cursor.visible = false;
        //initRot = transform.rotation;
        _defaultScale = transform.localScale;
    }

    private void OnEnable()
    {
        _wepRef.Fired += OnFired;
    }

    private void OnDisable()
    {
        _wepRef.Fired -= OnFired;
    }

    void FixedUpdate()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, initRot, _degreesPerSecond * Time.deltaTime);
        transform.localScale = Vector3.MoveTowards(transform.localScale, _defaultScale, _scalePerSecond * Time.deltaTime);
    }

    void OnFired()
    {
        //transform.rotation = Quaternion.Euler(0, 0, -180);
        transform.localScale = new Vector3(_scaleTo, _scaleTo, _scaleTo);
    }
}
