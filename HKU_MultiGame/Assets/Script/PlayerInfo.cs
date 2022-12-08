using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerInfo : MonoBehaviourPun
{
    [SerializeField]
    private TextMeshProUGUI PlayerName;

    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            PlayerName.text = "Me";
        }

        else
        {
            PlayerName.text = $"Player{photonView.OwnerActorNr}";
        }
    }
}