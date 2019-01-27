using UnityEngine;
using System.Collections;
using System;

public class SwipeController : MonoBehaviour
{
    public delegate void OnSwipe(float strength, Vector3 startPos, Vector3 endPos);
    public OnSwipe onSwipe;

    Vector3 startPos;
    float startTime;

    private float _expectedMin;
    private float _expectedMax;
    
    public bool isSwipeControllerActive = false;

    public void Init(float expectedMin, float expectedMax)
    {
        _expectedMax = expectedMax;
        _expectedMin = expectedMin;
    }

    public void WaitThenEnable() { StartCoroutine(WaitThenEnableCoroutine()); }
    private IEnumerator WaitThenEnableCoroutine() { yield return new WaitForSeconds(0.2f); isSwipeControllerActive = true; }

    void Update()
    {
        if (!isSwipeControllerActive)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            //Store initial values
            startPos = Input.mousePosition;
            startTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Get end values
            Vector3 endPos = Input.mousePosition;
            float endTime = Time.time;

            //Mouse positions distance from camera. Might be a better idea to use the cameras near plane
            startPos.z = 0.1f;
            endPos.z = 0.1f;

            //Makes the input pixel density independent
            startPos = Camera.main.ScreenToWorldPoint(startPos);
            endPos = Camera.main.ScreenToWorldPoint(endPos);

            //The duration of the swipe
            float duration = endTime - startTime;

            //The direction of the swipe
            Vector3 dir = endPos - startPos;

            //The distance of the swipe
            float distance = dir.magnitude;

            //Faster or longer swipes give higher power
            float power = distance / duration;

            //Measure expected power here
            Debug.Log(power);

            //change power from the range 50...60 to 0...1
            power -= _expectedMin;
            power /= _expectedMax - _expectedMin;

            //clamp value to between 0 and 1
            power = Mathf.Clamp01(power);
            // Invoke the callback
            onSwipe.Invoke(power, startPos, endPos);
        }
    }
}
