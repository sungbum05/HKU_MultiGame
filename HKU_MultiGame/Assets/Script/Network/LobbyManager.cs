using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // 게임 버전
    [SerializeField]
    private int RoomCnt = 1;

    public Button CreateRoomButton;

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
    }

    public void CreatRoom()
    {
        Debug.Log("생성 중...");
        RoomOptions RoomOptions = new RoomOptions();

        RoomOptions.IsOpen = true;
        RoomOptions.IsVisible = true;
        RoomOptions.MaxPlayers = 4;

        PhotonNetwork.CreateRoom($"Room_{RoomCnt}", RoomOptions);
        RoomCnt++;
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
        Debug.Log("로비 접속 성곡");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("룸 접속 성곡");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("룸 리스트 업데이트");
        Debug.Log(roomList.Count);
    }
    #endregion
}
