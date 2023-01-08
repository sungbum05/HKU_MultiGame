using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoes : Item
{
    [PunRPC]
    protected override void ItemEffect()
    {
        base.ItemEffect();
        Debug.Log("Child");

        StartCoroutine(SpeedUp());
    }

    IEnumerator SpeedUp()
    {
        yield return null;

        if(Player.Instance.PlayerInfo.Type == PlayerType.Runner && Player.Instance.PlayerInfo.IsShose == false)
        {
            Player.Instance.PlayerInfo.IsShose = true;
            Player.Instance.PlayerMove.Speed = 10.0f;

            yield return new WaitForSeconds(2.0f);

            Player.Instance.PlayerMove.Speed = Player.Instance.PlayerMove.BasicSpeed;
            Player.Instance.PlayerInfo.IsShose = false;
        }

        yield break;
    }
}
