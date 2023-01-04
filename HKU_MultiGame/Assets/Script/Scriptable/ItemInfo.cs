using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo",menuName = "Scriptable/ItemInfo")]
public class ItemInfo : ScriptableObject
{
    public string ItemName;
    public Sprite ItemImage;
}
