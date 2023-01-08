using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[System.Serializable]
public class Quest
{
    public bool QuestCheck;
    public Image Content;
}

public class QuestManager : MonoBehaviourPunCallbacks
{
    public static QuestManager Instance;

    [SerializeField]
    Text Timer;
    [SerializeField]
    float m_Timer;
    string minutesS = "";
    string secondsS = "";
    bool m_TimerRunning = false;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    List<Quest> QuestList;

    #region Å° Äù½ºÆ®
    public void ClearQuest(int Idx)
    {
        photonView.RPC("ClearQueseRpc", RpcTarget.All, Idx);
    }

    [PunRPC]
    public void ClearQueseRpc(int Idx)
    {
        bool Ending = false;

        QuestList[Idx].QuestCheck = true;
        QuestList[Idx].Content.color = Color.white;

        foreach (var item in QuestList)
        {
            Ending = item.QuestCheck;

            if (Ending == false)
                break;
        }
        
        if(Ending == true)
        {
            if (Player.Instance.PlayerInfo.Type == PlayerType.Runner)
                UiManager.Instance.OnHappyEnding();

            else if (Player.Instance.PlayerInfo.Type == PlayerType.Chaser)
                UiManager.Instance.OnSadEnding();
        }
    }
    #endregion

    [PunRPC]
    public void StartTimer()
    {
        m_TimerRunning = true;
    }

    void Update()
    {
        if (m_TimerRunning)
        {
            m_Timer -= Time.deltaTime;
            float minutes = Mathf.Floor(m_Timer / 60);
            float seconds = Mathf.RoundToInt(m_Timer % 60);

            if (minutes < 10)
            {
                minutesS = "0" + minutes.ToString();
            }
            else
            {
                minutesS = minutes.ToString();
            }
            if (seconds < 10)
            {
                secondsS = "0" + Mathf.RoundToInt(seconds).ToString();
            }
            else
            {
                secondsS = Mathf.RoundToInt(seconds).ToString();
            }

            Timer.text = string.Format("{0}:{1}", minutesS, secondsS);
        }

        if(m_Timer <= 0)
        {
            m_TimerRunning = false;

            if (Player.Instance.PlayerInfo.Type == PlayerType.Runner)
                UiManager.Instance.OnSadEnding();

            else if (Player.Instance.PlayerInfo.Type == PlayerType.Chaser)
                UiManager.Instance.OnHappyEnding();
        }
    }
}
