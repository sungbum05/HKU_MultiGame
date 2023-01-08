using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UiManager : MonoBehaviourPunCallbacks
{
    private static UiManager m_Instance;
    public static UiManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<UiManager>();
            }

            return m_Instance;
        }
    }

    [SerializeField]
    private Text TimerText = null;

    [SerializeField]
    private Image PlayerHpBar;
    [SerializeField]
    private Image PlayerStaminaBar;

    [Header("¿£µù")]
    [SerializeField]
    private GameObject HappyEnding;
    [SerializeField]
    private GameObject SadEnding;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

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
        if (PhotonNetwork.CurrentRoom.PlayerCount.Equals(PhotonNetwork.CurrentRoom.MaxPlayers) && PhotonNetwork.IsMasterClient)
        {
            GameManager.Instance.PlayersTypeSelect();
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    public void OnHappyEnding()
    {
        HappyEnding.SetActive(true);
    }

    public void OnSadEnding()
    {
        SadEnding.SetActive(true);
    }
}
