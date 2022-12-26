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
    public List<GameObject> LocalPlayerList;

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

        //네트워크 상의 모든 클라이언트들에서 생성
        //단, 해당 게임 오브젝트 주도권은 생성 클라가 가지고 있음
        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0), Quaternion.identity);

        UIMgr.photonView.RPC("SettingPlyerCount", RpcTarget.All);
    }

    public void PlayersTypeSelect()
    {
        Debug.Log("TYPE");

        int RunnerNum = Random.Range(0, PhotonNetwork.CurrentRoom.MaxPlayers);

        for (int i = 0; i < PhotonNetwork.CurrentRoom.Players.Count; i++)
        {
            Debug.Log(i);
            Debug.Log(PhotonNetwork.CurrentRoom.Players.Count);

            GameObject Player = LocalPlayerList[i];
            Debug.Log(Player.name);
            PlayerInfo Info = Player.GetComponent<PlayerInfo>();

            if (i.Equals(RunnerNum))
            {
                Info.photonView.RPC("SetType", RpcTarget.All, PlayerType.Runner);
            }

            else
            {
                Info.photonView.RPC("SetType", RpcTarget.All, PlayerType.Chaser);
            }

            Info.photonView.RPC("SetTypeColor", RpcTarget.All);
        }
    }

    private void Update()
    {

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
