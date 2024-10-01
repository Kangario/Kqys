using kcp2k;
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IPAdd : MonoBehaviour
{
    [SerializeField] private NetworkManager _networkManager;
    [SerializeField] private KcpTransport _kcp_Transport;
    [SerializeField] private TMP_InputField _inputField;

    public void TransferIp()
    {
        bool _isPort = false;
        string ip = "";
        string port = "";

        foreach (char simvol in _inputField.text)
        {
            if (!_isPort)
                ip += simvol;
            else
                port += simvol;

            if (simvol == ':')
            {
                _isPort= true;
            }
        }

        ip = ip.Remove(ip.Length - 1);
        _networkManager.networkAddress = ip;
        if (ushort.TryParse(port, out ushort portNumber))
            _kcp_Transport.port = portNumber;
        else
            Debug.LogError($"Некорректный порт: {port}");
    }
}
