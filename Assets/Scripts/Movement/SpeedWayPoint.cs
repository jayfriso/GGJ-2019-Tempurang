using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedWayPoint : MonoBehaviour {

    // Modifies the speed by the multiplier until the next way point
    [Range(0, 1)]
    public float speedModifier;

    public Vector3 position { get { return transform.position; } }
}
