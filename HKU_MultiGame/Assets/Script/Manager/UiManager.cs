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

    [SerializeField]
    private Image PlayerHpBar;
    [SerializeField]
    private Image PlayerStaminaBar;

    private void Update()
    {
        if (Player.Instance != null)
        {
            PlayerHpBar.fillAmount = Player.Instance.PlayerInfo.Hp / Player.Instance.PlayerInfo.MaxHp;
            PlayerStaminaBar.fillAmount = Player.Instance.PlayerInfo.Stamina / Player.Instance.PlayerInfo.MaxStamina;
        }
    }

    [PunRPC]
    public void SettingPlyerCount()
    {
        PlayerCountText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}";

        if (PhotonNetwork.CurrentRoom.PlayerCount.Equals(PhotonNetwork.CurrentRoom.MaxPlayers) && PhotonNetwork.IsMasterClient)
        {
            GameManager.Instance.PlayersTypeSelect();
        }
    }

    public void PlayerInfoUpdate()
    {

    }
}
