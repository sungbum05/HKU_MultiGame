using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    //�κ� ����
    private string gameVersion = "1"; // ���� ����
    [SerializeField]
    private int RoomCnt = 1;

    public Transform ScrollViewContent;

    public Button CreateRoomButton;
    public GameObject JoinRoomButton;

    public List<RoomData> RoomList;

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
        SoundMgr.In.ChangeBGM("BGM_Fugitive_Normal_1");
    }

    public void CreatRoom()
    {
        Debug.Log("���� ��...");
        RoomOptions RoomOptions = new RoomOptions();

        RoomOptions.IsOpen = true;
        RoomOptions.IsVisible = true;
        RoomOptions.MaxPlayers = 4;
        Debug.Log(PhotonNetwork.CountOfRooms);

        PhotonNetwork.CreateRoom($"Room_{PhotonNetwork.CountOfRooms}", RoomOptions);
    }

    public void JoinRoom(RoomData data)
    {
        PhotonNetwork.JoinRoom(data.RoomName);
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
        PhotonNetwork.LoadLevel("MainScene");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(returnCode);
        Debug.Log(message);
        Debug.Log("�� ���� ����");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("�� ���� ����");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("�� ����Ʈ ������Ʈ");
        Debug.Log(roomList.Count);

        foreach (var RoomInfo in roomList)
        {
            Debug.Log($"RoomName:{RoomInfo.Name}, MaxPlayer:{RoomInfo.MaxPlayers}, CurPlayer:{RoomInfo.PlayerCount}");
            RoomData RoomInfoButton =
                Instantiate(JoinRoomButton.gameObject, this.transform.position, Quaternion.identity).GetComponent<RoomData>();

            RoomInfoButton.transform.SetParent(ScrollViewContent.transform);

            RoomInfoButton.RoomSetting(RoomInfo.Name, RoomInfo.MaxPlayers, RoomInfo.PlayerCount);
            RoomList.Add(RoomInfoButton);
        }

        foreach (var m_RoomData in RoomList)
        {
            if(m_RoomData.CurPlayer <= 0)
            {
                RoomData Data = m_RoomData;

                RoomList.Remove(Data);
                Destroy(Data);
            }
        }
    }
    #endregion
}
