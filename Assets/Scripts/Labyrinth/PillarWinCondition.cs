using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarWinCondition : MonoBehaviour
{
    [SerializeField] private int _pillarAmount = 4;

    public void Decrease()
    {
        _pillarAmount -= 1;

        if (_pillarAmount <= 0)
        {
            GameObject.FindObjectOfType<WinLossController>().WinEvent();
        }
    }
}
