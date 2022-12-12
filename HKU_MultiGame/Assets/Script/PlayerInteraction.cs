using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInteraction : MonoBehaviourPunCallbacks
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Potion") && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("InteractionPotion");
            other.GetComponent<Item>().Use();
        }
    }
}
