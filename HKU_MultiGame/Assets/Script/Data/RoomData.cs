using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomData : MonoBehaviourPun
{
    private string roomname;
    public string RoomName
    {
        get
        {
            return roomname;
        }
        set
        {
            roomname = value;
        }
    }
    private int maxplayer;
    public int MaxPlayer
    {
        get
        {
            return maxplayer;
        }

        set
        {
            maxplayer = value;
        }
    }
    private int curplayer;
    public int CurPlayer
    {
        get
        {
            return curplayer;
        }

        set
        {
            curplayer = value;
        }
    }

    public Text RoomNameText;
    public Text PlayerCountText;

    public void RoomSetting(string RoomName, int MaxPlayer, int CurPlayer)
    {
        this.RoomName = RoomName;

        this.MaxPlayer = MaxPlayer;
        this.CurPlayer = CurPlayer;

        ButtonSetting();
    }

    void ButtonSetting()
    {
        RoomNameText.text = roomname;

        PlayerCountText.text = $"{curplayer} / {maxplayer}";
    }
}
