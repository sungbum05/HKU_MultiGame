using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // ���� ����
    [SerializeField]
    private int RoomCnt = 1;

    public Button CreateRoomButton;

    //���� ����� ���ÿ� ������ ���� ����
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("������ ���� ����");
        //�ڵ� �� ����ȭ
        PhotonNetwork.AutomaticallySyncScene = true;
        // ���ӿ� �ʿ��� ���� ����
        PhotonNetwork.GameVersion = gameVersion;
        //������ ������ ������ ������ ���� ���� �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Start()
    {
        //��ư ���
        CreateRoomButton.onClick.AddListener(() => CreatRoom());
    }

    public void CreatRoom()
    {
        Debug.Log("���� ��...");
        RoomOptions RoomOptions = new RoomOptions();

        RoomOptions.IsOpen = true;
        RoomOptions.IsVisible = true;
        RoomOptions.MaxPlayers = 4;

        PhotonNetwork.CreateRoom($"Room_{RoomCnt}", RoomOptions);
        RoomCnt++;
    }

    #region Photon CallBack�Լ�
    //������ ���� ���� ������ �ڵ� ����
    public override void OnConnectedToMaster()
    {
        Debug.Log("������ ���� ���� ����");
        PhotonNetwork.JoinLobby();
    }

    //������ ���� ���� ���н� �ڵ� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
        //������ �������� ������ �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� ���� ����");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� ����");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("�� ����Ʈ ������Ʈ");
        Debug.Log(roomList.Count);
    }
    #endregion
}
