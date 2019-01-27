using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangPath : MonoBehaviour {

    public SpeedWayPoint[] pathPoints;

    private float _maxScale;
    private float _minScale;
    private float _maxThrowYPos;

    private Vector3 _pathDirection;

    public void Awake()
    {
        _pathDirection = pathPoints[1].position - pathPoints[0].position;
    }

    public void Init(float maxScale, float minScale, float maxThrowYPos)
    {
        _maxScale = maxScale;
        _minScale = minScale;
        _maxThrowYPos = maxThrowYPos;
    }

    public void TranformForThrow(Vector2 startPoint, Vector2 endPoint, float throwStrength)
    {
        // Reset scale to start
        transform.localScale = Vector2.one;

        Vector2 directionVector = endPoint - startPoint;
        // First scale the y to match the shape
        float yScale = Mathf.Lerp(_minScale, _maxScale, throwStrength);

        // Scale the w vector to be the same height as the pathDirection
        float pathScale = directionVector.y / (_pathDirection.y * yScale);
        Vector2 scaledPath = pathScale * _pathDirection;
        float boomerangXScale = directionVector.x / scaledPath.x;

        transform.localScale = new Vector2(boomerangXScale, yScale);

        float yPos = Mathf.Clamp(startPoint.y, -10000, _maxThrowYPos);
        transform.position = new Vector2(startPoint.x, yPos);
    }
}
