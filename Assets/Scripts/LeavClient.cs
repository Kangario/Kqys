using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeavClient : MonoBehaviour
{
    [SerializeField] private Button _button;
    private MyNetworkManager _networkManager;

    private void Start()
    {
        _networkManager = FindObjectOfType<MyNetworkManager>();

        _button.onClick.AddListener(_networkManager.UpdateDropdown);
        _button.onClick.AddListener(_networkManager.StopClient);
    }
}
