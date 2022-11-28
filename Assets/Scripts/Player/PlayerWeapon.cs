using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZCameraShake;

public class PlayerWeapon : MonoBehaviour
{
    public event Action Fired = delegate {};

    [SerializeField] private GameObject bulletRef;

    private Vector3 gunEndPointPosition;

    [SerializeField] private Transform shootTransform;

    private Vector3 mousePos;

    [SerializeField] private bool _isAuto;
    [SerializeField] private float _fireDelay;
    private float _startFireTime;

    public bool allowInput = true;

    [SerializeField] private float _bulletSize;

    private void OnEnable()
    {
        allowInput = true;
    }

    private void Update()
    {
        if (allowInput)
        {
            Aim();
            ShootCheck();
        }
    }

    private void Aim()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 aimDir = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
        }
        else
        {
            aimLocalScale.y = 1f;
        }
        transform.localScale = aimLocalScale;
    }

    private void ShootCheck()
    {
        if (_isAuto)
        {
            if (Input.GetMouseButton(0) && Time.time > _fireDelay + _startFireTime)
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && Time.time > _startFireTime + _fireDelay)
            {
                Shoot();
    }
        }
            }

    private void Shoot()
    {
        AudioManager.PlaySound("Gunshot1");
        CameraShaker.Instance.ShakeOnce(3f, 2f, 0.2f, 0.2f);
        gunEndPointPosition = shootTransform.position;
        Transform bulletTransform = Instantiate(bulletRef.transform, gunEndPointPosition, Quaternion.identity);
        Vector3 shootDir = (gunEndPointPosition - transform.position).normalized;

        Vector3 aimDir = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        bulletTransform.GetComponent<PlayerBullets>().BulletSetup(shootDir, angle, 20, 1, 3, _bulletSize);

        // Audio
        // Camera Shake

        // Recoil
        transform.GetChild(0).position = transform.GetChild(0).position += (-shootDir * 0.75f);
        transform.GetChild(0).localRotation = (Quaternion.Euler(0,0,30));

        _startFireTime = Time.time;

        // Event
        Fired?.Invoke();
    }
}
