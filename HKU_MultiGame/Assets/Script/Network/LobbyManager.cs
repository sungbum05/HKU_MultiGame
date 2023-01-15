using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    //로비 정보
    private string gameVersion = "1"; // 게임 버전
    [SerializeField]
    private int RoomCnt = 1;

    public Transform ScrollViewContent;

    public Button CreateRoomButton;
    public GameObject JoinRoomButton;

    public List<RoomData> RoomList;

    //게임 실행과 동시에 마스터 서버 접속
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("마스터 서버 접속");
        //자동 씬 동기화
        PhotonNetwork.AutomaticallySyncScene = true;
        // 접속에 필요한 버전 설정
        PhotonNetwork.GameVersion = gameVersion;
        //설정한 정보를 가지고 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Start()
    {
        //버튼 등록
        CreateRoomButton.onClick.AddListener(() => CreatRoom());
        SoundMgr.In.ChangeBGM("BGM_Fugitive_Normal_1");
    }

    public void CreatRoom()
    {
        Debug.Log("생성 중...");
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

    #region Photon CallBack함수
    //마스터 서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터 서버 접속 성공");
        PhotonNetwork.JoinLobby();
    }

    //마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {
        //마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("로비 접속 성공");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("룸 접속 성공");
        PhotonNetwork.LoadLevel("MainScene");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(returnCode);
        Debug.Log(message);
        Debug.Log("방 생성 실패");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("방 접속 실패");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("룸 리스트 업데이트");
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
