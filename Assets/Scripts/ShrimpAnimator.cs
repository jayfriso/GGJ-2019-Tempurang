using UnityEngine;
using System.Collections;

public class ShrimpAnimator : MonoBehaviour
{

    [SerializeField]
    private float _spinSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, _spinSpeed);
    }
}
