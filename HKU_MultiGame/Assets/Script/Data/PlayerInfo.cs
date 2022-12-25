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
    public TextMeshProUGUI PlayerName;
    [SerializeField]
    private Player[] players;

    //�÷��̾� �ʵ�
    [Header("�÷��̾� �ʵ�")]
    [SerializeField]
    private PlayerType Type;

    //�÷��̾� ���� ������Ʈ
    [Header("�÷��̾� ���� �ʵ�")]
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

    public void SetType(PlayerType playerType)
    {
        Type = playerType;
    }

    [PunRPC]
    public void SettingPlyerCount()
    {
        PlayerCountText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}";

        if (PhotonNetwork.CurrentRoom.PlayerCount.Equals(PhotonNetwork.CurrentRoom.MaxPlayers) && PhotonNetwork.IsMasterClient)
        {
            GameManager.Instance.photonView.RPC("PlayersTypeSelect", RpcTarget.All);
        }
    }
}
