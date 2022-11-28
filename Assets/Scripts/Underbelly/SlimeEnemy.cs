using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pathfinding;
using EZCameraShake;

public class SlimeEnemy : MonoBehaviour
{
    private AIPath _AIPath;
    public float range;
    private float distToPlayer;
    public Transform playerTransform;

    [SerializeField] private float _maxHealth = 100;
    private float _currentHealth;
    private float _DoT;
    [SerializeField] private float _DoTAmount = 5f;
    [SerializeField] private float _posionTime = 2f;

    private bool _grow = false;
    private bool _noDamage = false;
    private bool _dead = false;

    private SpriteRenderer _sr;

    public enum State
    {
        SmallWalkState,
        GrowState,
        BigWalkState,
        ExplodeState,
    }

    [SerializeField] private Sprite[] _smallWalkAnim;
    [SerializeField] private Sprite[] _bigWalkAnim;
    [SerializeField] private Sprite[] _growAnim;
    [SerializeField] private Sprite[] _explodeAnim;

    [SerializeField] private ParticleSystem _helmParticles;
    [SerializeField] private ParticleSystem _bubbleParticles;

    SpriteRenderer _hpsr;

    private float _animSpeed = 0.2f;

    public State state;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _AIPath = GetComponent<AIPath>();
        _sr = GetComponent<SpriteRenderer>();
        _hpsr = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        state = State.SmallWalkState;
        NextState();
    }

    IEnumerator SmallWalkState()
    {
        Debug.Log("SmallWalk: Enter");

        int index = 0;
        _animSpeed = 0.2f;

        while (state == State.SmallWalkState)
        {
            _sr.sprite = _smallWalkAnim[index];

            index++;
            if (index >= _smallWalkAnim.Length) index = 0;

            yield return new WaitForSeconds(_animSpeed);
        }
    }

    IEnumerator GrowState()
    {
        Debug.Log("Grow: Enter");

        int index = 0;
        _animSpeed = 0.2f;
        _noDamage = true;
        _AIPath.maxSpeed = 0;

        transform.GetChild(0).localPosition = new Vector2(transform.GetChild(0).localPosition.x, transform.GetChild(0).localPosition.y + 1.2f);

        while (state == State.GrowState)
        {
            _sr.sprite = _growAnim[index];

            index++;
            if (index >= _growAnim.Length)
            {
                state = State.BigWalkState;
                _noDamage = false ;
                NextState();
                Debug.Log("Grow: Exit");
            }

            yield return new WaitForSeconds(_animSpeed);
        }
    }

    IEnumerator BigWalkState()
    {
        Debug.Log("BigWalk: Enter");

        CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();
        col.offset = new Vector2(-0.5f, 1.4375f);
        col.size = new Vector2(1f, 2.8f);

        int index = 0;
        _animSpeed = 0.2f;

        _AIPath.maxSpeed = 6f;

        while (state == State.BigWalkState)
        {
            _sr.sprite = _bigWalkAnim[index];

            index++;
            if (index >= _bigWalkAnim.Length) index = 0;

            yield return new WaitForSeconds(_animSpeed);
        }
    }

    IEnumerator ExplodeState()
    {
        Debug.Log("Explode: Enter");

        int index = 0;
        _animSpeed = 0.1f;
        _noDamage = true;

        _AIPath.maxSpeed = 0;

        GetComponent<CircleCollider2D>().enabled = false;

        while (state == State.ExplodeState)
        {
            _sr.sprite = _explodeAnim[index];

            index++;
            if (index >= _explodeAnim.Length)
            {
                Instantiate(_bubbleParticles, transform.position + new Vector3(0, 1.5f, 0), Quaternion.Euler(0, 0, 0));
                Destroy(gameObject);
                Debug.Log("Explode: Exit");
            }

            yield return new WaitForSeconds(_animSpeed);
        }
    }

    void NextState()
    {
        string methodName = state.ToString();
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    void Update()
    {
        if (playerTransform != null)
        {
            distToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            Vector3 scale = transform.localScale;
            if(playerTransform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1;
                transform.GetChild(0).localScale = new Vector2(-1,1);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x);
                transform.GetChild(0).localScale = new Vector2(1, 1);
            }
            transform.localScale = scale;
        }
        else
        {
            Debug.Log("Null player transform");
        }

        if(distToPlayer > range)
        {
            //mover.enabled = false;
        }
        else
        {}
        //mover.enabled = true;

        if (_currentHealth > 0 && !_dead && !_noDamage)
        {
            _currentHealth -= _DoT * Time.deltaTime;
            _hpsr.transform.localScale =new Vector3( _currentHealth / _maxHealth * 1.5f, .15f,1);
        }

        if(_currentHealth <= _maxHealth * 0.75 && !_grow)
        {
            _grow = true;
            Grow();
        }

        if (_currentHealth <= 0 && !_dead)
        {
            _dead = true;
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !_dead)
        {
            _dead = true;
            Explode();
            other.GetComponent<PlayerHealth>().TakeDamage(10);

            other.GetComponent<PlayerHealth>().ChangeDOT(2);
            other.GetComponent<PlayerMovement>().Force(other.transform.position - transform.position);
            other.GetComponent<PlayerHealth>().DelayedDetox(-2);
        }
        else if(other.tag == "Bullet" && !_dead && !_noDamage)
        {
            _DoT += _DoTAmount;

            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
            transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "x" + (int)(_DoT / 5);

            Invoke(nameof(Detox), _posionTime);
        }
    }

    void Detox()
    {
        _DoT -= _DoTAmount;

        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "x" + (int)(_DoT / 5);

        if (_DoT <= 0)
        {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().color = Color.clear;
        }
    }

    private void Grow()
    {
        AudioManager.PlaySound("SlimeGrow");
        Instantiate(_helmParticles, transform.position, Quaternion.Euler(0, 0, 0));
        state = State.GrowState;
        NextState();
    }

    public void Explode()
    {
        state = State.ExplodeState;
        AudioManager.PlaySound("RatExplode");
        NextState();
    }

}
