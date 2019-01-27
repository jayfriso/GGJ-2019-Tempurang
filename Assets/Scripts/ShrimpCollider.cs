using UnityEngine;
using System.Collections;
using System;
using UnityEngine.U2D;
using DG.Tweening;

public class ShrimpCollider : MonoBehaviour
{
    public Action onDeathCountdown;
    public Action onDeath;
    public Action onSuccess;

    [SerializeField]
    private float onHitDeathTime = 1f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private bool _hasStartedDeathCountdown = false;
    private float _timeSinceHit = 0f;
   
    public void Init()
    {
        _timeSinceHit = 0f;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_hasStartedDeathCountdown)
        {
            _timeSinceHit += Time.fixedDeltaTime;
            if (_timeSinceHit >= onHitDeathTime)
            {
                if (onDeath != null)
                    onDeath.Invoke();
            }
        }
    }

    public void InitThrow()
    {
        _rigidbody2D.gravityScale = 0f;
        _hasStartedDeathCountdown = false;
        _timeSinceHit = 0f;
        _rigidbody2D.velocity = Vector2.zero;
    }

    public void TriggerSuccess()
    {
        // If it has collided with a barrier, then failure
        if (_hasStartedDeathCountdown)
            return;

        Debug.Log("SUCCESS!");
        if (onSuccess != null)
            onSuccess.Invoke();
    }

    public void StartDeathCountdown()
    {
        _hasStartedDeathCountdown = true;
        _rigidbody2D.gravityScale = 1f;
        _spriteRenderer.DOFade(0f, onHitDeathTime);

        if (onDeathCountdown != null)
            onDeathCountdown.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        StartDeathCountdown();
    }
}
