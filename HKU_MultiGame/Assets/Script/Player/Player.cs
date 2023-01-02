using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static public Player Instance;

    public PlayerInfo PlayerInfo;
    public PlayerMove PlayerMove;
    public PlayerInteraction PlayerInteraction;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerInfo = this.GetComponent<PlayerInfo>();
        PlayerMove = this.GetComponent<PlayerMove>();
        PlayerInteraction = this.GetComponent<PlayerInteraction>();
    }
}
