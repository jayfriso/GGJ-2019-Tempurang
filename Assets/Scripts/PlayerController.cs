using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwipeController))]
public class PlayerController : MonoBehaviour {

    [Header("Game Configuration")]
    [SerializeField]
    private float _boomerangMovementSpeed;
    
    [SerializeField]
    private float _minScale;
    [SerializeField]
    private float _maxScale;

    [Header("Swipe Configuration")]
    //expected values are what power you get when you try 
    //desired values are what you want
    //you might want these as public values so they can be set from the inspector
    [SerializeField]
    private float _expectedMin = 50;
    [SerializeField]
    private float _expectedMax = 60;
    [SerializeField]
    private float _maxThrowYPos;

    [Header("Component References")]
    [SerializeField]
    private BoomerangMovement _boomerangMovement;
    [SerializeField]
    private Transform _basePoint;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    DebugValueController _debugValueController;

    private SwipeController _swipeController;

    [HideInInspector]
    public bool isControllerActive = true;

	// Use this for initialization
	void Start () 
    {
        _swipeController = GetComponent<SwipeController>();

        InitValues();
        InitDebugController();

        _swipeController.onSwipe += HandleSwipe;
        _debugValueController.onToggled += (bool toggled) => { isControllerActive = !toggled; };

        isControllerActive = true;
    }

    private void InitValues()
    {
        _swipeController.Init(_expectedMin, _expectedMax);
        _swipeController.Init(_expectedMin, _expectedMax);
        _boomerangMovement.boomerangPath.Init(_maxScale, _minScale, _maxThrowYPos);
        _boomerangMovement.Init(_boomerangMovementSpeed);
    }

    private void InitDebugController()
    {
        _debugValueController.AddNewDebugValue("Boomerang Movement Speed", _boomerangMovementSpeed, (float newValue) => { this._boomerangMovementSpeed = newValue; InitValues(); });

        _debugValueController.AddNewDebugValue("Min Scale", _minScale, (float newValue) => { this._minScale = newValue; InitValues(); });
        _debugValueController.AddNewDebugValue("Max Scale", _maxScale, (float newValue) => { this._maxScale = newValue; InitValues(); });

        _debugValueController.AddNewDebugValue("Expected Min", _expectedMin, (float newValue) => { this._expectedMin = newValue; InitValues(); });
        _debugValueController.AddNewDebugValue("Expected Max", _expectedMax, (float newValue) => { this._expectedMax = newValue; InitValues(); });
    }

    private void HandleSwipe(float swipeStrength, Vector3 startPos, Vector3 endPos) 
    {
        if (!isControllerActive)
            return;

        _boomerangMovement.boomerangPath.TranformForThrow((Vector2)startPos, (Vector2)endPos, swipeStrength);
        _boomerangMovement.TriggerPathWalk();
    }
}
