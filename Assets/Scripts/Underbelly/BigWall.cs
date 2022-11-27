using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class BigWall : MonoBehaviour
{
    [SerializeField] Sprite _holeSprite;
    private SpriteRenderer _sr;
    private BoxCollider2D _boxCollider;

    [SerializeField] GameObject _hiddenRoof;

    [SerializeField] GameObject flashImage;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _sr.sortingOrder = 0;
    }

    public void Break()
    {
        _sr.sprite = _holeSprite;
        _sr.sortingOrder = 3;

        _boxCollider.enabled = false;

        _hiddenRoof.SetActive(false);

        flashImage.GetComponent<Animator>().Play("Flash");
        CameraShaker.Instance.ShakeOnce(5f, 6f, 0.1f, 1f);
        AudioManager.PlaySound("Explosion");
    }
}
