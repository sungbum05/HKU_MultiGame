using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public enum PlayerType
{
    Lobby, Runner, Chaser
}

public class PlayerInfo : MonoBehaviourPun
{
    [SerializeField]
    public TextMeshProUGUI PlayerName;
    [SerializeField]
    private Player[] players;

    //플레이어 필드
    [Header("플레이어 필드")]
    [SerializeField]
    private PlayerType Type;

    //플레이어 하위 오브젝트
    [Header("플레이어 하위 필드")]
    [SerializeField]
    private GameObject Chaser = null;
    [SerializeField]
    private GameObject Runner = null;

    private void Awake()
    {
        this.gameObject.name = $"Player{photonView.OwnerActorNr}";

        GameManager.Instance.LocalPlayerList.Add(this.gameObject);
    }

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

    [PunRPC]
    public void SetType(PlayerType playerType)
    {
        Type = playerType;
    }

    [PunRPC]
    public void SetTypeColor()
    {
        switch(Type)
        {
            case PlayerType.Runner:
                PlayerName.color = Color.green;
                break;

            case PlayerType.Chaser:
                PlayerName.color = Color.red;
                break;

            default:
                PlayerName.color = Color.white;
                break;
        }
    }
}
