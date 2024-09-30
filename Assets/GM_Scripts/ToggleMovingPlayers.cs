using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToggleMovingPlayers : NetworkBehaviour
{
    public void OnMove()
    {
        CmdToggleMoving(true);
    }

    public void OffMove()
    {
        CmdToggleMoving(false);
    }

    [Command]
    public void CmdToggleMoving(bool move)
    {
        List<MovingController> mv = FindObjectsOfType<MovingController>().ToList();
        foreach(MovingController controller in mv)
        {
            controller.UnLimitedMove = move;
            controller.StopMoving();
        }
        RpcToggleMoving(move);
    }

    [ClientRpc]
    public void RpcToggleMoving(bool move)
    {
        List<MovingController> mv = FindObjectsOfType<MovingController>().ToList();
        foreach (MovingController controller in mv)
        {
            controller.UnLimitedMove = move;
            controller.StopMoving();
        }
    }

}
