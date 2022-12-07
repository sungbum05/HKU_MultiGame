using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // ���� ����

    public Text ConnectionInfoText; // ��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ
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
        ConnectionInfoText.text = "������ ������ ���� ��...";
    }

    //������ ���� ���� ������ �ڵ� ����
    public override void OnConnectedToMaster()
    {
        //�� ���� ��ư�� Ȱ��ȭ
        JoinButton.interactable = true;
        //���� ���� ǥ��
        ConnectionInfoText.text = "�¶��� : ������ ������ ����";
    }

    //������ ���� ���� ���н� �ڵ� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
        //�� ���� ��ư�� ��Ȱ��ȭ
        JoinButton.interactable = false;
        //���� ���� ǥ��
        ConnectionInfoText.text = "�������� : ������ ������ ���� ���� ����\n���� ��õ� ��...";

        //������ �������� ������ �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        //�ߺ� ���� �õ��� ���� ����, ���� ��ư ��� ��Ȱ��ȭ
        JoinButton.interactable = false;

        //������ ������ �������̶��
        if(PhotonNetwork.IsConnected)
        {
            //�� ���� ����
            ConnectionInfoText.text = "�뿡 ����...";
            PhotonNetwork.JoinRandomRoom();
        }

        else
        {
            //������ ���� ���� �ٽ� �õ�
            ConnectionInfoText.text = "�������� : ������ ������ ���� �õ� ��..";
            //������ ���� ���� ��õ�
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //����� ���� ���� �� ������ ������ ��� �ڵ� ����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //���� ���� ǥ��
        ConnectionInfoText.text = "��� ����, ���ο� �� ����..";
        //�ִ� 4���� ���� ������ ��� ����
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        //���� ���� ǥ��
        ConnectionInfoText.text = "�� ���� ����";
        //��� �� �����ڵ��� Main�� �ε�
        PhotonNetwork.LoadLevel("MainScene");
    }
}
