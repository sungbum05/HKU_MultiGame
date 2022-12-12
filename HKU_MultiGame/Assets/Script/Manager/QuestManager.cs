using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

[System.Serializable]
public class Quest
{
    public bool QuestCheck;
    public TextMeshProUGUI Content;
}

public class QuestManager : MonoBehaviourPunCallbacks
{
    public static QuestManager Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    List<Quest> QuestList;

    public void ClearQuest(int Idx)
    {
        photonView.RPC("ClearQueseRpc", RpcTarget.All, Idx);
    }

    [PunRPC]
    public void ClearQueseRpc(int Idx)
    {
        QuestList[Idx].QuestCheck = true;
        QuestList[Idx].Content.color = Color.green;
    }
}
