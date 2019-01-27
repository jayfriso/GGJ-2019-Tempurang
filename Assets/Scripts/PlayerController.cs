using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Game Configuration")]
    [SerializeField]
    private float _boomerangMovementSpeed;

    [SerializeField]
    private float _minMagnitude;
    [SerializeField]
    private float _maxMagnitude;
    [SerializeField]
    private float _minScale;
    [SerializeField]
    private float _maxScale;

    [Header("Component References")]
    [SerializeField]
    private BoomerangMovement _boomerangMovement;
    [SerializeField]
    private Transform _basePoint;
    [SerializeField]
    private Camera _camera;

	// Use this for initialization
	void Start () 
    {
        _boomerangMovement.Init(_boomerangMovementSpeed);
        _boomerangMovement.boomerangPath.Init(_maxScale, _minScale);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            float swipeMagnitude = Mathf.Clamp((clickPos - (Vector2)_basePoint.position).magnitude, _minMagnitude, _maxMagnitude);
            float strength = (swipeMagnitude - _minMagnitude) / (_maxMagnitude - _minMagnitude);
            _boomerangMovement.boomerangPath.TranformForThrow(_basePoint.position, clickPos, strength);
            _boomerangMovement.TriggerPathWalk();
        }
    }
}
