using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // 게임 버전

    public Text ConnetionInfoText; // 네트워크 정보를 표시할 텍스트
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
        ConnetionInfoText.text = "마스터 서버에 접속 중...";
    }
}
