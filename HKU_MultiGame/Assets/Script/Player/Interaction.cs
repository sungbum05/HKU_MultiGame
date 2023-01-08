using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public BoxManager BoxManager;
    [SerializeField]
    private BoxData CurBoxData;
    [SerializeField]
    private PlayerInfo PlayerInfo;

    [SerializeField]
    private GameObject InBox;
    [SerializeField]
    private bool CanHide = false;

    private void Start()
    {
        InBox = GameManager.Instance.InBox;
        BoxManager = GameObject.Find("BoxMgr").GetComponent<BoxManager>();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Chest"))
        {
            CurBoxData = Other.GetComponent<BoxData>();
        }

        else if (Other.CompareTag("Closet"))
        {
            CanHide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D Other)
    {
        if (Other.CompareTag("Chest"))
        {
            CurBoxData = null;
        }

        else if (Other.CompareTag("Closet"))
        {
            CanHide = false;
        }
    }

    private void Update()
    {
        this.gameObject.transform.localPosition = Vector3.zero;

        if (PlayerInfo.photonView.IsMine == true)
        {
            if (CurBoxData != null && InBox.activeSelf == false && Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Open");
                InBox.SetActive(true);

                BoxManager.CurBoxData = this.CurBoxData;
                BoxManager.CurInBoxInfo = CurBoxData.InBoxItemList;
                BoxManager.OpenChest();
            }

            else if (CurBoxData != null && InBox.activeSelf == true && Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Close");
                InBox.SetActive(false);

                BoxManager.CloseChest();
            }

            if (CanHide == true && PlayerInfo.IsHide == false && Input.GetKeyDown(KeyCode.F))
            {
                PlayerInfo.HideCheck(true);
            }

            else if (CanHide == true && PlayerInfo.IsHide == true && Input.GetKeyDown(KeyCode.F))
            {
                PlayerInfo.HideCheck(false);
            }
        }
    }
}
