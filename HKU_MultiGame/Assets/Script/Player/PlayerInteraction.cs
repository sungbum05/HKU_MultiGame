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
    private Coroutine AttackCoroutine;

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

        if (Input.GetKeyDown(KeyCode.RightArrow))
            HorizontalSendAttack(1);

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            HorizontalSendAttack(-1);

        else if (Input.GetKeyDown(KeyCode.UpArrow))
            VerticalSendAttack(1);

        else if (Input.GetKeyDown(KeyCode.DownArrow))
            VerticalSendAttack(-1);
    }

    public void HorizontalSendAttack(int Dir)
    {
        AttackCoroutine = StartCoroutine(HorizontalAttack(Dir));
    }

    public void VerticalSendAttack(int Dir)
    {
        AttackCoroutine = StartCoroutine(VerticalAttack(Dir));
    }

    IEnumerator HorizontalAttack(int Dir)
    {
        yield return null;

        float X = 0.0f;
        X = Mathf.Abs(HorizontalRange.transform.localPosition.x) * Dir;

        HorizontalRange.transform.localPosition = new Vector3(X, 0, 0);

        HorizontalRange.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        HorizontalRange.SetActive(false);

        StopCoroutine(AttackCoroutine);
        yield break;
    }

    IEnumerator VerticalAttack(int Dir)
    {
        yield return null;

        float Y = 0;
        Y = Mathf.Abs(VerticalRange.transform.localPosition.y) * Dir;

        VerticalRange.transform.localPosition = new Vector3(0, Y, 0);

        VerticalRange.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        VerticalRange.SetActive(false);

        StopCoroutine(AttackCoroutine);
        yield break;
    }
}
