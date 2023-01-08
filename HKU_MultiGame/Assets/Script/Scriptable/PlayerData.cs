using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName = "Scriptable/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string Name;
    public PlayerType PlayerType;
    public RuntimeAnimatorController Animator;
    public int Index;
    public float Speed;
}
