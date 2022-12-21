using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UiManager : MonoBehaviourPun
{
    [SerializeField]
    private Text PlayerCountText = null;
    [SerializeField]
    private Text TimerText = null;

    [PunRPC]
    public void SettingPlyerCount()
    {
        PlayerCountText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}";

        if(PhotonNetwork.CurrentRoom.PlayerCount.Equals(PhotonNetwork.CurrentRoom.MaxPlayers))
        {

        }
    }
}
