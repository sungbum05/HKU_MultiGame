using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

[System.Serializable]
public class ItemSpawnInfo
{
    public ItemInfo ItemInfo;
    public int SpawnCount;
}

public class BoxManager : MonoBehaviourPun
{
    public const int MinIndex = 0;
    public const int MaxIndex = 15;

    [Header("아이템 스폰 관련")]
    [SerializeField]
    private List<ItemSpawnInfo> ItemSpawnInfo;
    [SerializeField]
    private List<ItemInfo> ItemSpawnList;
    [SerializeField]
    private List<ItemInfo> CompleteSpawnList;

    [Header("아이템 박스 관련")]
    [SerializeField]
    private List<BoxData> FieldChestList;

    [SerializeField]
    private int SeletIndex = 0;
    [SerializeField]
    private GameObject SelectObject;

    [SerializeField]
    private Transform InBoxParant;
    [SerializeField]
    private GameObject[,] InBoxArray = new GameObject[4,4];
    [SerializeField]
    public BoxData CurBoxData;
    [SerializeField]
    public List<InBoxInfo> CurInBoxInfo;


    // Start is called before the first frame update
    void Start()
    {
        BasicSetting();
    }

    // Update is called once per frame
    void Update()
    {
        InputNextIndex();

        if(Player.Instance != null && Player.Instance.PlayerInfo.IsOpenChest == true && CurInBoxInfo.Count > 0)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                InBoxInfo boxInfo = null;
                GameObject ItemPrefab = null;

                foreach (var Info in CurInBoxInfo)
                {
                    if (SeletIndex == Info.ItemIndex)
                    {
                        boxInfo = Info;
                        ItemPrefab = PhotonNetwork.Instantiate(boxInfo.ItemInfo.ItemPrefab.name, new Vector3(200,200,200), Quaternion.identity);
                        break;
                    }
                }

                if (boxInfo != null)
                {

                    if (boxInfo.ItemInfo.Type == ItemType.Key)
                    {
                        ItemPrefab.GetComponent<Item>().Use();
                    }

                    // 아이템 이미지 투명 변경
                    Color A;
                    A = Color.white;
                    A.a = 0.0f;

                    //데이터 정리
                    PhotonNetwork.Destroy(ItemPrefab);
                    InBoxArray[boxInfo.ItemIndex / 4, boxInfo.ItemIndex % 4].gameObject.GetComponent<SpriteRenderer>().color = A;
                    CurBoxData.InBoxItemList.Remove(boxInfo);
                    CurInBoxInfo.Remove(boxInfo);
                }
            }
        }
    }

    public void BasicSetting()
    {
        //맵에 배치된 ChestList 가져옴
        foreach(GameObject Chest in GameObject.FindGameObjectsWithTag("Chest"))
        {
            FieldChestList.Add(Chest.GetComponent<BoxData>());
        }

        //InBox 인덱스 세팅
        for (int i = 0; i < InBoxParant.childCount; i++)
        {
            Transform Line = InBoxParant.GetChild(i);

            for (int j = 0; j < Line.childCount; j++)
            {
                InBoxArray[i,j] = Line.GetChild(j).gameObject;
            }
        }

        NextIndex();

        ItemListShuffle();
        DivideItem();
    }

    public void ItemListShuffle()
    {
        //아이템 스폰 리스트 변환작업
        foreach (var item in ItemSpawnInfo)
        {
            for (int i = 0; i < item.SpawnCount; i++)
            {
                ItemSpawnList.Add(item.ItemInfo);
            }
        }

        FieldChestList.Shuffle();
        ItemSpawnList.Shuffle();
    }

    public void DivideItem()
    {
        int ItemSpawnCount = 0;
        int BoxCount = 0;

        while(true)
        {
            int Ran = Random.Range(1, 3);
            int ItemIndex = 0;

            for (int i = 0; i < Ran; i++)
            {
                if (ItemSpawnCount >= ItemSpawnList.Count)
                    break;

                ItemIndex += Random.Range(i, 6);

                FieldChestList[BoxCount].InBoxItemList.Add(new InBoxInfo(ItemSpawnList[ItemSpawnCount], ItemIndex));
                ItemSpawnCount++;
            }

            BoxCount++;

            if(BoxCount > FieldChestList.Count || ItemSpawnCount > ItemSpawnList.Count)
            {
                break;
            }
        }
    }

    public void OpenChest()
    {
        foreach(var BoxInfo in CurInBoxInfo)
        {
            InBoxArray[BoxInfo.ItemIndex / 4, BoxInfo.ItemIndex % 4].gameObject.GetComponent<SpriteRenderer>().sprite = BoxInfo.ItemInfo.ItemImage;
            InBoxArray[BoxInfo.ItemIndex / 4, BoxInfo.ItemIndex % 4].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        Player.Instance.PlayerInfo.IsOpenChest = true;
    }

    public void CloseChest()
    {
        Color color;

        color = Color.white;
        color.a = 0.0f;

        foreach (var BoxInfo in CurInBoxInfo)
        {
            InBoxArray[BoxInfo.ItemIndex / 4, BoxInfo.ItemIndex % 4].gameObject.GetComponent<SpriteRenderer>().sprite = null;
            InBoxArray[BoxInfo.ItemIndex / 4, BoxInfo.ItemIndex % 4].gameObject.GetComponent<SpriteRenderer>().color = color;
        }

        Player.Instance.PlayerInfo.IsOpenChest = false;

        CurBoxData = null;
        CurInBoxInfo = null;
    }

    public void InputNextIndex()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (SeletIndex - 1 < MinIndex)
                return;

            SeletIndex--;

            NextIndex();
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (SeletIndex + 1 > MaxIndex)
                return;

            SeletIndex++;

            NextIndex();
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (SeletIndex - 4 < MinIndex)
                return;

            SeletIndex -= 4;

            NextIndex();
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (SeletIndex + 4 > MaxIndex)
                return;

            SeletIndex += 4;

            NextIndex();
        }
    }

    public void NextIndex()
    {
        SelectObject.transform.position = InBoxArray[SeletIndex / 4, SeletIndex % 4].transform.position;
    }
}
