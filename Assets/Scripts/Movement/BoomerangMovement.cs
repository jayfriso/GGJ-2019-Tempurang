using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangMovement : MonoBehaviour
{
    // put the points from unity interface
    [SerializeField]
    private BoomerangPath _boomerangPath;
    public BoomerangPath boomerangPath { get { return _boomerangPath; } }

    [SerializeField]
    private Transform _movementTransform;
    
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

    private bool _isMoving = false;
    private float _speed = 4f;
    private float _currentSpeed = 0f;

    // Use this for initialization
    void Start()
    {
        _currentSpeed = _boomerangPath.pathPoints[currentIndex].speedModifier * _speed;
    }

    public void Init(float boomerangSpeed)
    {
        _speed = boomerangSpeed;
    }

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

    public void TriggerPathWalk() 
    {
        currentIndex = 0;
        _isMoving = true;
    }

    private void Walk()
    {   
        float progress = (_movementTransform.position - currentWayPoint.position).magnitude / (targetWayPoint.position - currentWayPoint.position).magnitude;
        _currentSpeed = _speed * Mathf.Lerp(currentWayPoint.speedModifier, targetWayPoint.speedModifier, progress);

        // move towards the target
        _movementTransform.position = Vector3.MoveTowards(_movementTransform.position, targetWayPoint.position, _currentSpeed * Time.deltaTime);

        if (_movementTransform.position == targetWayPoint.position)
        {
            currentIndex++;
            if (currentIndex >= _boomerangPath.pathPoints.Length)
            {
                _isMoving = false;
            }
        }
    }
}