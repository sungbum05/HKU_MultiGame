using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnergeDrink : Item
{
    [PunRPC]
    protected override void ItemEffect()
    {
        base.ItemEffect();
        Debug.Log("Child");

        if (Player.Instance.PlayerInfo.Type == PlayerType.Runner && Player.Instance.PlayerInfo.IsEnergeDrink == false)
        {
            if(Player.Instance.PlayerInfo.Hp + 10 > Player.Instance.PlayerInfo.MaxHp)
            {
                Player.Instance.PlayerInfo.Hp = Player.Instance.PlayerInfo.MaxHp;
            }

            else
            {
                Player.Instance.PlayerInfo.Hp += 10;
            }
        }
    }
}
