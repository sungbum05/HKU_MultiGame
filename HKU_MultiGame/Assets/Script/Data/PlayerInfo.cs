using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public enum PlayerType
{
    Runner, Chaser
}

public class PlayerInfo : MonoBehaviourPun
{
    [SerializeField]
    private TextMeshProUGUI PlayerName;
    [SerializeField]
    private Player[] players;

    //플레이어 하위 오브젝트
    [Header("플레이어 하위 오브젝트")]
    [SerializeField]
    private GameObject Chaser = null;
    [SerializeField]
    private GameObject Runner = null;

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
