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

        //네트워크 상의 모든 클라이언트들에서 생성
        //단, 해당 게임 오브젝트 주도권은 생성 클라가 가지고 있음
        PhotonNetwork.LocalPlayer.TagObject = PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0), Quaternion.identity);

        LocalPlayerCharacter = PhotonNetwork.LocalPlayer.TagObject as GameObject;
        UIMgr.photonView.RPC("SettingPlyerCount", RpcTarget.All);
    }

    [PunRPC]
    public void PlayersTypeSelect()
    {
        Debug.Log("TYPE");

        int RunnerNum = Random.Range(0, PhotonNetwork.CurrentRoom.MaxPlayers);

        for (int i = 0; i < PhotonNetwork.CurrentRoom.Players.Count; i++)
        {
            GameObject Player = PhotonNetwork.CurrentRoom.Players[i].TagObject as GameObject;
            PlayerInfo Info = Player.GetComponent<PlayerInfo>();

            if (i.Equals(RunnerNum))
            {
                Info.SetType(PlayerType.Runner);
                Info.PlayerName.color = Color.green;
            }

            else
            {
                Info.SetType(PlayerType.Chaser);
                Info.PlayerName.color = Color.red;
            }
        }

    }

    private void Update()
    {

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
