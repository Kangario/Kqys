using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglerMovement : NetworkBehaviour
{
    [SerializeField] private PlayerType _playerType;
    [SerializeField] private CursorController _cursorController;
    [SerializeField] private FirstPersonController _firstPersonController;
    [SerializeField] private FreeCam _gameMasterCam;
    [SerializeField] Camera _cam1;
    [SerializeField] Camera _cam2;


    public void Start()
    {
        if (!isLocalPlayer) { return; }
        if (_playerType == PlayerType.Player)
        {
            _cursorController.enabled = true;
            _firstPersonController.enabled = true;
            _gameMasterCam.enabled = false;
            _cam1.targetDisplay = 0;
            _cam2.targetDisplay = 1;
           
        }
        else
        {
            _firstPersonController.enabled = false;
            _gameMasterCam.enabled = true;
            _cursorController.enabled = false;
            _cam1.targetDisplay = 1;
            _cam2.targetDisplay = 0;
        }
    }



}

public enum PlayerType
{
    None,
    Player,
    GameMaster
}