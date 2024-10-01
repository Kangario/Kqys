using Den.Tools;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerTypeTransfer : NetworkBehaviour
{
    [SyncVar] public string Name;
    [SerializeField] private Canvas _choicePlayer;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TMP_Dropdown _dropDown;
    public List<GameObject> _playerPrefabs;

    public void Start()
    {
        if (!isLocalPlayer)
        {
            _choicePlayer.gameObject.SetActive(false);
        }
        else
        {
            CmdDisableComponentsForAllPlayers();
            _choicePlayer.gameObject.SetActive(true);
        }
    }

    public void Select()
    {
        if (isLocalPlayer)
        {
            string playerName = _nameInput.text;
            Name = playerName;
            CmdSpawnPlayer(_dropDown.value, playerName);
        }
    }

    [Command]
    public void CmdSpawnPlayer(int characterIndex, string playerName)
    {
        GameObject playerInstance = Instantiate(_playerPrefabs[characterIndex]);

        PlayerController playerController = playerInstance.GetComponent<PlayerController>();
        playerController.gameObject.SetActive(true);
        playerController.gameObject.name = playerName;
        playerController.playerName = playerController.gameObject.name;
        NetworkServer.ReplacePlayerForConnection(connectionToClient, playerController.gameObject, true);
        NetworkServer.Spawn(playerInstance, connectionToClient);
        RpcUpdatePlayers();
        RpcUpdateDropdown();
        NetworkServer.Destroy(gameObject);
    }

    [ClientRpc]
    public void RpcUpdateDropdown()
    {
        // На клиенте просто обновляем данные в Dropdown без повторного вызова команд
        List<AddOptions> options = FindObjectsOfType<AddOptions>().ToList();
        List<PlayerController> players = FindObjectsOfType<PlayerController>().ToList();

        foreach (AddOptions option in options)
        {
            option.ClearDropdown(); // Используем локальный метод для очистки (не команду)

            foreach (PlayerController player in players)
            {
                option.AddOptionsToDropdown(player.playerName); // Локальное добавление опций на клиенте
            }

            option.RefreshDropdown(); // Обновляем отображение
        }
    }

    [ClientRpc]
    public void RpcUpdatePlayers()
    {
        UpdatePlayers();
    }

    [Command]
    public void CmdDisableComponentsForAllPlayers()
    {
        RpcDisableComponentsForAllPlayers();
    }

    [ClientRpc]
    public void RpcDisableComponentsForAllPlayers()
    {
        UpdatePlayers();
    }

    public void UpdatePlayers()
    {
        List<PlayerController> players = FindObjectsOfType<PlayerController>().ToList();

        foreach (PlayerController player in players)
        {
            if (player.isLocalPlayer)
            {
                EnablePlayerComponents(player);
            }
            else
            {
                DisablePlayerComponents(player);
            }
        }
    }

    private void DisablePlayerComponents(PlayerController player)
    {
        if (player.GetComponent<FirstPersonController>() != null)
            player.GetComponent<FirstPersonController>().enabled = false;
        else
            player.GetComponent<FreeCam>().enabled = false;
        player.Camera.enabled = false;
        player.CurrentCanvas.enabled = false;
        player.gameObject.name = player.playerName;
    }

    private void EnablePlayerComponents(PlayerController player)
    {   
        if (player.GetComponent<FirstPersonController>()!=null)
            player.GetComponent<FirstPersonController>().enabled = true;
        else
            player.GetComponent<FreeCam>().enabled = true;
        player.Camera.enabled = true;
        player.CurrentCanvas.enabled = true;
        player.gameObject.name = player.playerName;
    }
}