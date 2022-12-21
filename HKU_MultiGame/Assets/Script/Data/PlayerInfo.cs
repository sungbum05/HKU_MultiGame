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

    //�÷��̾� ���� ������Ʈ
    [Header("�÷��̾� ���� ������Ʈ")]
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
