using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InBoxInfo
{
    public ItemInfo ItemInfo;
    public int ItemIndex;

    public InBoxInfo(ItemInfo info, int Index)
    {
        ItemInfo = info;
        ItemIndex = Index;
    }

}

public class BoxData : MonoBehaviour
{
    public const int MinIndex = 0;
    public const int MaxIndex = 15;

    public int BoxNumber = 0;

    public List<InBoxInfo> InBoxItemList;
}
