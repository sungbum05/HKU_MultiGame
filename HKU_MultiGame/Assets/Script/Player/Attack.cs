using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.CompareTag("Runner"))
        {
            Debug.Log("Hit");
        }
    }
}
