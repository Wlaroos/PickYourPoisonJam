using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShadow : MonoBehaviour
{

    RotateHoverUtil _itemRef;
    Vector3 _defaultScale;

    private void Awake()
    {
        _defaultScale = transform.localScale;
        _itemRef = transform.parent.GetComponentInChildren<RotateHoverUtil>();
        transform.localPosition = new Vector3(0, -_itemRef.Amplitude - 1.25f, 0);
    }

    private void Update()
    {
        transform.localScale = _defaultScale * 0.66f * (-_itemRef.Sine + 2);
    }

}
