using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // ���� ����

    public Text ConnetionInfoText; // ��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ
    public Button JoinButton; // �� ���� ��ư

    //���� ����� ���ÿ� ������ ���� ����
    // Start is called before the first frame update
    void Start()
    {
        // ���ӿ� �ʿ��� ���� ����
        PhotonNetwork.GameVersion = gameVersion;
        //������ ������ ������ ������ ���� ���� �õ�
        PhotonNetwork.ConnectUsingSettings();

        //�� ���� ��ư�� ��� ��Ȱ��ȭ
        JoinButton.interactable = false;
        //������ �õ� ������ �ؽ�Ʈ�� ǥ��
        ConnetionInfoText.text = "������ ������ ���� ��...";
    }
}
