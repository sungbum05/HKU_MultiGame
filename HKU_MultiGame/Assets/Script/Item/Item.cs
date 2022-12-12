using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item : MonoBehaviourPunCallbacks
{
    public void Use()
    {
        photonView.RPC("ItemEffect", RpcTarget.All);
    }

    [PunRPC]
    protected virtual void ItemEffect()
    {
        this.gameObject.SetActive(false);
    }
}
