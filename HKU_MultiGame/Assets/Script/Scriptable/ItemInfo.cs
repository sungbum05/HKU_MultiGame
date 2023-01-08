using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItemType
{
    ActiveItem, Key
}

[CreateAssetMenu(fileName = "ItemInfo",menuName = "Scriptable/ItemInfo")]
public class ItemInfo : ScriptableObject
{
    public ItemType Type;
    public GameObject ItemPrefab;
    public string ItemName;
    public Sprite ItemImage;
    public Item ItemData;
}
