using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Milk : Item
{
    [PunRPC]
    protected override void ItemEffect()
    {
        base.ItemEffect();
        Debug.Log("Child");

        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return null;

        if (Player.Instance.PlayerInfo.IsMilk == false)
        {
            Player.Instance.PlayerInfo.IsMilk = true;
            foreach (var item in GameManager.Instance.LocalPlayerList)
            {
                if(item.GetComponent<PlayerInfo>().Type == PlayerType.Runner)
                {
                    item.GetComponent<SpriteRenderer>().enabled = false;
                }
            }     

            yield return new WaitForSeconds(2.0f);

            foreach (var item in GameManager.Instance.LocalPlayerList)
            {
                if (item.GetComponent<PlayerInfo>().Type == PlayerType.Runner)
                {
                    item.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            Player.Instance.PlayerInfo.IsMilk = false;
        }

        yield break;
    }
}
