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
    [SerializeField]
    private float _distanceToCheckColl = 3f;

    [Header("Component References")]
    [SerializeField]
    private GameObject _shripPrefab;
    [SerializeField]
    private GameObject _shrimpContainer;
    [SerializeField]
    private Camera _camera;

    private GameController _gameController;
    private SwipeController _swipeController;

    [HideInInspector]
    public bool isControllerActive = true;


    public void Init(GameController gameController, UIController uiController) 
    {
        _gameController = gameController;
        InitDebugController(uiController.debugValueController);
        _swipeController = GetComponent<SwipeController>();
        InitValues();
        _swipeController.onSwipe += HandleSwipe;
        isControllerActive = false;
    }

    private void InitValues()
    {
        _swipeController.Init(_expectedMin, _expectedMax);
        _swipeController.Init(_expectedMin, _expectedMax);
    }

    private void InitDebugController(DebugValueController debugValueController)
    {
        debugValueController.onToggled += (bool toggled) => { isControllerActive = !toggled; };

        debugValueController.AddNewDebugValue("Boomerang Movement Speed", _boomerangMovementSpeed, (float newValue) => { this._boomerangMovementSpeed = newValue; InitValues(); });

        debugValueController.AddNewDebugValue("Min Scale", _minScale, (float newValue) => { this._minScale = newValue; InitValues(); });
        debugValueController.AddNewDebugValue("Max Scale", _maxScale, (float newValue) => { this._maxScale = newValue; InitValues(); });

        debugValueController.AddNewDebugValue("Expected Min", _expectedMin, (float newValue) => { this._expectedMin = newValue; InitValues(); });
        debugValueController.AddNewDebugValue("Expected Max", _expectedMax, (float newValue) => { this._expectedMax = newValue; InitValues(); });
    }
    public void StartGame()
    {
        isControllerActive = true;
        _swipeController.WaitThenEnable();
    }

    private void HandleSwipe(float swipeStrength, Vector3 startPos, Vector3 endPos) 
    {
        if (!isControllerActive)
            return;
        ShrimpController shrimpController = Instantiate(_shripPrefab, Vector3.zero, Quaternion.identity, _shrimpContainer.transform).GetComponent<ShrimpController>();
        shrimpController.onSuccess += HandleShrimpSuccess;
        shrimpController.boomerangPath.Init(_maxScale, _minScale, _maxThrowYPos);
        shrimpController.Init(_boomerangMovementSpeed, _distanceToCheckColl);
        shrimpController.boomerangPath.TranformForThrow((Vector2)startPos, (Vector2)endPos, swipeStrength);
        shrimpController.TriggerThrow();
    }

    private void HandleShrimpSuccess()
    {
        _gameController.IncremementPoints();
    }
}
