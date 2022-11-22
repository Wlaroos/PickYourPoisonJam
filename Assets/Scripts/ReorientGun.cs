using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReorientGun : MonoBehaviour
{
    Vector3 _defaultPos;
    Quaternion _defaultRot;

    [SerializeField] float _metersPerSecond;
    [SerializeField] float _degreesPerSecond;

    private void Awake()
    {
        // Set inital values
        _defaultPos = transform.localPosition;
        _defaultRot = transform.localRotation;
    }

    private void Update()
    {
        Reorient();
    }

    void Reorient()
    {
        if (transform.localPosition.x != _defaultPos.x && transform.localPosition.y != _defaultPos.y)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _defaultPos, _metersPerSecond * Time.deltaTime);
        }

        if (transform.localRotation.eulerAngles.z != _defaultRot.z)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _defaultRot, _degreesPerSecond * Time.deltaTime);
        }
    }

}
