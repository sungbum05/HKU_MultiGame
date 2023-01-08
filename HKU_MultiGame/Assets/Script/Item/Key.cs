using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Key : Item
{
    [SerializeField]
    private int QuestIdx = 0;

    [PunRPC]
    protected override void ItemEffect()
    {
        base.ItemEffect();
        Debug.Log("Child");

        QuestManager.Instance.ClearQuest(QuestIdx);
    }
}
