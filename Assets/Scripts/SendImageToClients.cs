using Mirror;
using Org.BouncyCastle.Crypto.Macs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SendImageToClients : NetworkBehaviour
{
    public GameObject currentPlayer;
    public TMP_Dropdown selectPlayers;
    public PlayerType _playerType;
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private Image _previewImage;
    [SerializeField] private TMP_InputField _inputText;
    [Header("Диалоговое окно")]
    [SerializeField] private GameObject _dialogMenu;
    [SerializeField] private Image _icoCharacter;
    [SerializeField] private TextMeshProUGUI _namCharacter;
    [SerializeField] private TextMeshProUGUI _textCharacter;


    public void Send()
    {
        int selectedIndex = selectPlayers.value;

        // Check if the selectedIndex is within range
        if (selectedIndex < 0 || selectedIndex >= selectPlayers.options.Count)
        {
            Debug.LogError("Selected index is out of range.");
            return;
        }
        string currentName = currentPlayer.name;
        MessagePlayer message = new MessagePlayer();
        message.Sprite = _previewImage.sprite;
        message.Name = _name.text;
        message.InputText = _inputText.text;
        CmdSendImageToClient(selectPlayers.options[selectedIndex].text, currentName, message);
    }

    [Command]
    public void CmdSendImageToClient(string name,string currentName,MessagePlayer message)
    {
        GameObject player = GameObject.Find(name);

        Debug.LogWarning("CMD:"+player.name);

        RtcSendImageToClient(name, currentName,message);
    }

    [ClientRpc]
    public void RtcSendImageToClient(string name,string currentName, MessagePlayer message)
    {
        GameObject player = GameObject.Find(name);
        GameObject currentPlayer = GameObject.Find(currentName);
        SendImageToClients sendsImage = player.GetComponentInChildren<SendImageToClients>();
        Debug.LogWarning(sendsImage._name.text);
        ShowInformation(sendsImage, message);
    }

    public void ShowInformation(SendImageToClients sendsImage, MessagePlayer curent)
    {
        Debug.LogWarning(sendsImage.selectPlayers.value + " updating UI.");
        sendsImage._dialogMenu.GetComponent<TogglerPanel>().TogglePanel();
        // Assign the current image and text to all clients
        sendsImage._icoCharacter.sprite = curent.Sprite;
        sendsImage._namCharacter.text = curent.Name;
        sendsImage._textCharacter.text = curent.InputText;
    }
}

public struct MessagePlayer
{
    public Sprite Sprite;
    public string Name; 
    public string InputText;

}
