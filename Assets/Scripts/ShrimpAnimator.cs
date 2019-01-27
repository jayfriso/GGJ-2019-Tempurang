using UnityEngine;
using System.Collections;
using System;

public class ShrimpAnimator : MonoBehaviour
{
    public Action onCollision;

    [SerializeField]
    private float _spinSpeed;
    [SerializeField]
    private float onHitDeathTime = 1f;

    private Rigidbody2D _rigidbody2D;
    private float _currentSpinSpeed;
    private bool _hasCollided = false;
    private float _timeSinceHit = 0f;

    // Use this for initialization
    void Start()
    {
        _currentSpinSpeed = _spinSpeed;
        _timeSinceHit = 0f;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_hasCollided)
        {
            _timeSinceHit += Time.fixedDeltaTime;
            _currentSpinSpeed = Mathf.Lerp(_currentSpinSpeed, 0f, _timeSinceHit / onHitDeathTime);
        }
        if (_currentSpinSpeed > 0f)
            _rigidbody2D.MoveRotation(_currentSpinSpeed);
    }

    public void InitThrow()
    {
        _rigidbody2D.gravityScale = 0f;
        _hasCollided = false;
        _currentSpinSpeed = _spinSpeed;
        _timeSinceHit = 0f;
        _rigidbody2D.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _hasCollided = true;
        _rigidbody2D.gravityScale = 1f;

        if (onCollision != null)
            onCollision.Invoke();
    }
}
