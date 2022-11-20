using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowPlayer : MonoBehaviour
{
    private Rigidbody2D _playerRB;
    public float _followDistance;
    public float _distCheck;
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Follow();
    }

    void Follow()
    {
        float dist = Vector2.Distance(_rb.position, _playerRB.position); //distance between player and follower

        if (dist < _distCheck)
        {
            //follower is to close to the player
            //Debug.Log(dist);
            StopAllCoroutines(); //stop future movements
            _rb.velocity = Vector2.zero; //stop in place
        }
        else
        {
            StartCoroutine(ReplaceRigidBody(_playerRB.position)); //move closer to player
        }
    }

    IEnumerator ReplaceRigidBody(Vector2 setTo)
    {
        yield return new WaitForSeconds(_followDistance);
        _rb.position = setTo;
    }
}
