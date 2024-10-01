using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        UpdateDropdown();
    }

    public void UpdateDropdown()
    {
        List<PlayerTypeTransfer> _playerTypeTransfer = FindObjectsOfType<PlayerTypeTransfer>().ToList();

        foreach (PlayerTypeTransfer pl in _playerTypeTransfer)
        {
            //pl.RpcUpdateDropdown(_playerTypeTransfer);
        }
    }
}
