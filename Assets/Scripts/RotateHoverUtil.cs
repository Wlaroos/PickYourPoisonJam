using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHoverUtil : MonoBehaviour
{
    [SerializeField] bool canRotate;
    [SerializeField] Vector3 degreesPerSecond = new Vector3(0f,0f,15f);

    [SerializeField] bool canHover;
    // Amount the object will go up and down. Range of movement is (position - amplitude) to (position + amplitude)
    [SerializeField] float amplitude = 0.5f;
    // Time needed for one full cycle
    [SerializeField] float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start()
    {
        posOffset = transform.position;
    }

    void Update()
    {
        if (canRotate)
        {
            transform.Rotate(Time.deltaTime * degreesPerSecond, Space.Self);
        }

        if (canHover)
        {
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }
    }
}
