using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class OutputJornalInformations : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _outputText;
    [SerializeField] private RectTransform _content;

    private void OnEnable()
    {
        if (!isLocalPlayer) return;
    }
   
    [Command]
    public void CmdSendMessageRoll(string message)
    {
        Debug.LogWarning("CmdSendMessageRoll " + message);
        List<OutputJornalInformations> oji = FindObjectsOfType<OutputJornalInformations>().ToList();
        foreach (var outJornal in oji) 
        {
            DisplayMessage(outJornal, message);
            RpcDisplayMessage(outJornal, message); 
        }
    }

    [ClientRpc]
    private void RpcDisplayMessage(OutputJornalInformations currentJornal, string newValue)
    {
        DisplayMessage(currentJornal, newValue); // Display the message on the client
    }

    public void DisplayMessage(OutputJornalInformations currentJornalstring,string newValue)
    {
        Debug.LogWarning("UpdateChanges " + newValue);
        if (_outputText == null) return; // Check if _outputText is assigned

        currentJornalstring._outputText.text += newValue + "\n";
        StartCoroutine(UpdateContentSize());
    }

    private IEnumerator UpdateContentSize()
    {
        yield return new WaitForEndOfFrame();
        _content.sizeDelta = new Vector2(_content.sizeDelta.x, _outputText.preferredHeight);
    }
}

