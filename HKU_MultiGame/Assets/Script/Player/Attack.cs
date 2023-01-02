using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Attack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.CompareTag("Runner"))
        {
            Debug.Log("Hit");

            Other.GetComponent<PlayerInteraction>().photonView.RPC("Hit", RpcTarget.All, 50);
        }
    }
}
