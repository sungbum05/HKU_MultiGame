using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<GameManager>();
            }

            return m_Instance;
        }
    }

    private UiManager UIMgr;

    public GameObject PlayerPrefab;
    public GameObject LocalPlayerCharacter;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UIMgr = FindObjectOfType<UiManager>();

        //��Ʈ��ũ ���� ��� Ŭ���̾�Ʈ�鿡�� ����
        //��, �ش� ���� ������Ʈ �ֵ����� ���� Ŭ�� ������ ����
        PhotonNetwork.LocalPlayer.TagObject = PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0), Quaternion.identity);

        LocalPlayerCharacter = PhotonNetwork.LocalPlayer.TagObject as GameObject;
        UIMgr.photonView.RPC("SettingPlyerCount", RpcTarget.All);
    }

    private void Update()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
