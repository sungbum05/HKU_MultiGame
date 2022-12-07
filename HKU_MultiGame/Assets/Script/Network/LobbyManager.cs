using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // 게임 버전

    public Text ConnectionInfoText; // 네트워크 정보를 표시할 텍스트
    public Button JoinButton; // 룸 접속 버튼

    //게임 실행과 동시에 마스터 서버 접속
    // Start is called before the first frame update
    void Start()
    {
        // 접속에 필요한 버전 설정
        PhotonNetwork.GameVersion = gameVersion;
        //설정한 정보를 가지고 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();

        //룸 접속 버튼을 잠시 비활성화
        JoinButton.interactable = false;
        //접속을 시도 중임을 텍스트로 표시
        ConnectionInfoText.text = "마스터 서버에 접속 중...";
    }

    //마스터 서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster()
    {
        //룸 접속 버튼을 활성화
        JoinButton.interactable = true;
        //접속 정보 표시
        ConnectionInfoText.text = "온라인 : 마스터 서버와 연결";
    }

    //마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {
        //룸 접속 버튼을 비활성화
        JoinButton.interactable = false;
        //접속 정보 표시
        ConnectionInfoText.text = "오프라인 : 마스터 서버와 연결 되지 않음\n접속 재시도 중...";

        //마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        //중복 접속 시도를 막기 위해, 접속 버튼 잠시 비활성화
        JoinButton.interactable = false;

        //마스터 서버에 접속중이라면
        if(PhotonNetwork.IsConnected)
        {
            //룸 접속 실행
            ConnectionInfoText.text = "룸에 접속...";
            PhotonNetwork.JoinRandomRoom();
        }

        else
        {
            //마스터 서버 접속 다시 시도
            ConnectionInfoText.text = "오프라인 : 마스터 서버와 접속 시도 중..";
            //마스터 서버 접속 재시도
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //빈방이 없어 랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //접속 상태 표시
        ConnectionInfoText.text = "빈방 없음, 새로운 방 생성..";
        //최대 4명을 수용 가능한 빈방 생성
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        //접속 상태 표시
        ConnectionInfoText.text = "방 참가 성공";
        //모든 룸 참가자들이 Main씬 로드
        PhotonNetwork.LoadLevel("Main");
    }
}
