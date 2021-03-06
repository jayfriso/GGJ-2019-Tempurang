﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShrimpController : MonoBehaviour
{
    public Action onSuccess;

    // put the points from unity interface
    [SerializeField]
    private BoomerangPath _boomerangPath;
    public BoomerangPath boomerangPath { get { return _boomerangPath; } }

    [SerializeField]
    private ShrimpCollider _shrimpCollider;

    [SerializeField]
    private Transform _movementTransform;

    [SerializeField]
    private float _spinSpeed;

    private int currentIndex = 0;
    private SpeedWayPoint currentWayPoint 
    { 
        get 
        {
            if (currentIndex - 1 > 0 && currentIndex - 1 < _boomerangPath.pathPoints.Length)
                return _boomerangPath.pathPoints[currentIndex - 1];
            else
                return _boomerangPath.pathPoints[0];
        } 
    }
    SpeedWayPoint targetWayPoint
    {
        get
        {
            if (currentIndex > 0 && currentIndex < _boomerangPath.pathPoints.Length)
                return _boomerangPath.pathPoints[currentIndex];
            else
                return _boomerangPath.pathPoints[0];
        }
    }

    private Animator _animator;
    private bool _isMoving = false;
    private float _speed = 4f;
    private float _currentSpeed = 0f;
    private float _distanceToCheckForCollision = 3f;

    // Use this for initialization
    void Start()
    {
        _currentSpeed = _boomerangPath.pathPoints[currentIndex].speedModifier * _speed;
        _shrimpCollider.onDeathCountdown += HandleShrimpDeathCountdown;
        _shrimpCollider.onDeath += HandleShrimpDeath;
        _shrimpCollider.onSuccess += HandleShrimpSuccess;
        _animator = GetComponent<Animator>();
    }

    public void Init(float boomerangSpeed, float distanceToCheckForCollision)
    {
        _speed = boomerangSpeed;
        _distanceToCheckForCollision = distanceToCheckForCollision;
        _shrimpCollider.Init();
    }

    private void HandleShrimpDeathCountdown()
    {
        _isMoving = false;
    }
    private void HandleShrimpDeath()
    {
        Destroy(this.gameObject);
    }

    private void HandleShrimpSuccess()
    {
        _isMoving = false;
        _animator.SetTrigger("Fry");
        if (onSuccess != null)
            onSuccess.Invoke();
    }

    public void HandleFryFinish() { Destroy(this.gameObject); }

    // Update is called once per frame
    void Update()
    {
        if (!_isMoving)
            return;
        // check if we have somewere to walk
        if (currentIndex < this._boomerangPath.pathPoints.Length)
        {
            Walk();
        }
    }

    public void TriggerThrow() 
    {
        currentIndex = 0;
        _isMoving = true;
        _movementTransform.position = currentWayPoint.position;
        _shrimpCollider.InitThrow();
    }

    private void Walk()
    {   
        float progress = (_movementTransform.position - currentWayPoint.position).magnitude / (targetWayPoint.position - currentWayPoint.position).magnitude;
        _currentSpeed = _speed * Mathf.Lerp(currentWayPoint.speedModifier, targetWayPoint.speedModifier, progress);
        
        Rigidbody2D rigidBody2D = _movementTransform.GetComponent<Rigidbody2D>();

        RaycastHit2D hit = Physics2D.Raycast(_movementTransform.position, targetWayPoint.position - _movementTransform.position, _distanceToCheckForCollision, LayerMask.GetMask("Pot"));
        Debug.DrawRay(_movementTransform.position, targetWayPoint.position - _movementTransform.position);
        if (hit.collider != null)
        {
            rigidBody2D.AddForce((targetWayPoint.position - _movementTransform.position).normalized * _currentSpeed * 25f);
            _isMoving = false;
        }
        else
        {
            _movementTransform.position = Vector3.MoveTowards(_movementTransform.position, targetWayPoint.position, _currentSpeed * Time.deltaTime);
            _movementTransform.Rotate(new Vector3(0, 0, _spinSpeed * Time.deltaTime));
            
            if (_movementTransform.position == targetWayPoint.position)
            {
                currentIndex++;
                if (currentIndex >= _boomerangPath.pathPoints.Length)
                {
                    _isMoving = false;
                    _shrimpCollider.StartDeathCountdown();
                }
            }
        }
    }
}