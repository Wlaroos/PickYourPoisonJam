using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletRef;

    private Vector3 gunEndPointPosition;

    [SerializeField] private Transform shootTransform;

    private Vector3 mousePos;

    [SerializeField] private bool _isAuto;
    [SerializeField] private float _fireDelay;
    private float _startFireTime;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Aim();
        Shoot();
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

    private void Shoot()
    {
        if (_isAuto)
        {
            if (Input.GetMouseButton(0) && Time.time > _fireDelay + _startFireTime)
            {
                gunEndPointPosition = shootTransform.position;
                Transform bulletTransform = Instantiate(bulletRef.transform, gunEndPointPosition, Quaternion.identity);
                Vector3 shootDir = (gunEndPointPosition - transform.position).normalized;

                Vector3 aimDir = (mousePos - transform.position).normalized;
                float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

                bulletTransform.GetComponent<PlayerBullets>().BulletSetup(shootDir, angle, 20, 1, 3, 1);

                // Audio
                // Camera Shake

                // Recoil (Broken)
                //_rb.AddForce(-shootDir * 250);

                _startFireTime = Time.time;
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0) && Time.time > _startFireTime + _fireDelay)
            {
                gunEndPointPosition = shootTransform.position;
                Transform bulletTransform = Instantiate(bulletRef.transform, gunEndPointPosition, Quaternion.identity);
                Vector3 shootDir = (gunEndPointPosition - transform.position).normalized;

                Vector3 aimDir = (mousePos - transform.position).normalized;
                float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

                bulletTransform.GetComponent<PlayerBullets>().BulletSetup(shootDir, angle, 20, 1, 3, 1);

                // Audio
                // Camera Shake

                // Recoil (Broken)
                //_rb.AddForce(-shootDir * 250);

                _startFireTime = Time.time;
            }
        }
    }
}
