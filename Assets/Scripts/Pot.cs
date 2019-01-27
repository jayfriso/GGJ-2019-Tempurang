using UnityEngine;
using System.Collections;

public class Pot : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Shrimp"))
        {
            col.GetComponent<ShrimpCollider>().TriggerSuccess();
        }
    }
}
