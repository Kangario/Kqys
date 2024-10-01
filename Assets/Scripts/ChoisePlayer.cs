using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoisePlayer : MonoBehaviour
{
    [SerializeField] private ChoisePlayerContainer _choisePlayerContainer;
    public PlayerType _playerType;
    public GameObject _player;
    public GameObject _gameMaster;

    private void Start()
    {
        switch (_playerType)
        {
            case PlayerType.Player:
                _player.SetActive(true);
                _gameMaster.SetActive(false);
                break;
            case PlayerType.GameMaster:
                _player.SetActive(false);
                _gameMaster.SetActive(true);
                break;
            case PlayerType.None:
                _player.SetActive(false);
                _gameMaster.SetActive(false);   
                break;
        }
    }
}
