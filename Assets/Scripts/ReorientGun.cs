using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReorientGun : MonoBehaviour
{
    Vector3 _defaultPos;
    Quaternion _defaultRot;

    [SerializeField] float _metersPerSecond;
    [SerializeField] float _degreesPerSecond;

    //Rigidbody2D _rb;

    private void Awake()
    {
        //_rb = GetComponent<Rigidbody2D>();
        _defaultPos = transform.localPosition;
        _defaultRot = transform.localRotation;
    }

    private void Update()
    {

/*       
        if (!Mathf.Approximately(transform.localPosition.x, _defaultPos.x) && !Mathf.Approximately(transform.localPosition.y, _defaultPos.y))
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _defaultPos, _metersPerSecond * Time.deltaTime);
        }

        if (!Mathf.Approximately(transform.localRotation.eulerAngles.x, _defaultRot.x) && !Mathf.Approximately(transform.localRotation.eulerAngles.y, _defaultRot.y))
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _defaultRot, _degreesPerSecond * Time.deltaTime);
        }
*/

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _defaultPos, _metersPerSecond * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _defaultRot, _degreesPerSecond * Time.deltaTime);
    }

}
