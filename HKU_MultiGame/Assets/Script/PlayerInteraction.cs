using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInteraction : MonoBehaviourPunCallbacks
{
    [Header("플레이어 하위 상호작용 필드_아이템")]
    [SerializeField]
    private GameObject EnterItem;

    [Header("플레이어 하위 상호작용 필드_공격")]
    [SerializeField]
    private GameObject HorizontalRange;
    [SerializeField]
    private GameObject VerticalRange;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Potion") && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("InteractionPotion");
            other.GetComponent<Item>().Use();
        }
    }

    private void Update()
    {

    }

    public void HorizontalSendAttack()
    {
        float X = 0;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            X = Mathf.Abs(transform.position.x);

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            X = Mathf.Abs(transform.position.x) * -1;

        HorizontalRange.transform.position = new Vector3(X, 0, 0);
    }

    public void VerticalSendAttack()
    {
        float Y = 0;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            Y = Mathf.Abs(transform.position.y);

        else if (Input.GetKeyDown(KeyCode.DownArrow))
            Y = Mathf.Abs(transform.position.y) * -1;

        VerticalRange.transform.position = new Vector3(0, Y, 0);
    }
}
