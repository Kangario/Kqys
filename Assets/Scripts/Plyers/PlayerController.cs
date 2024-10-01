using Mirror;
using Mirror.Examples.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public bool isActive;

    // Имя игрока
    [SyncVar] public string playerName;

    public PlayerType PlayerType;
    public GameObject SelectedPlayer;
    public Camera Camera;
    public Canvas CurrentCanvas;
    public Animator Animator;

}