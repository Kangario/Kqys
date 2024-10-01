using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerTypeContainer", menuName = "Player/Choice Player Container")]
public class ChoisePlayerContainer : ScriptableObject
{
    public PlayerType PlayerType;
    public string PlayerName;
    public GameObject PlayerModel;
    public List<Animation> Animations;
}
