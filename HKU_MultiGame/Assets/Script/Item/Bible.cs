using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bible : Item
{
    public List<GameObject> ChaserList = new List<GameObject>();

    [PunRPC]
    protected override void ItemEffect()
    {
        base.ItemEffect();
        Debug.Log("Child");

        if (Player.Instance.PlayerInfo.Type == PlayerType.Runner && Player.Instance.PlayerInfo.IsBible == false)
        {
            foreach (var m_Player in GameManager.Instance.LocalPlayerList)
            {
                if(m_Player.GetComponent<PlayerInfo>().Type == PlayerType.Chaser)
                {
                    ChaserList.Add(m_Player);
                }

                int Ran = Random.Range(0, ChaserList.Count);
                ChaserList.Shuffle();

                Vector3 Pos = new Vector3();
                Pos = Player.Instance.PlayerInfo.gameObject.transform.position;

                Player.Instance.PlayerInfo.gameObject.transform.position = ChaserList[0].transform.position;
                ChaserList[Ran].transform.position = Pos;
            }
        }
    }
}
