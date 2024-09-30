using Den.Tools.GUI;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Camera Camera;
    public FirstPersonController FirstPersonController;
    public Canvas UI;
    [Header("GM stats")]
    public Camera CameraGM;
    public Canvas UIGM;
    public FreeCam GMFreeCam;

    public void SetGMStats(Camera cameraGM, Canvas UIGM, FreeCam GMFreeCam)
    {
        this.CameraGM = cameraGM;
        this.UIGM = UIGM;
        this.GMFreeCam = GMFreeCam;
    }

    public void SetActiveNPC()
    {
        Camera.enabled = true;
        FirstPersonController.enabled = true;
        UI.enabled = true;
        CameraGM.enabled = false;
        UIGM.enabled = false;
        GMFreeCam.enabled = false;
    }

    public void SetDisactiveNPC()
    {
        Camera.enabled = false;
        FirstPersonController.enabled = false;
        UI.enabled = false;
        CameraGM.enabled = true;
        UIGM.enabled = true;
        GMFreeCam.enabled = true;
    }

    public void HideNpc()
    {
        CameraGM.enabled = true;
        UIGM.enabled = true;
        GMFreeCam.enabled = true;
        GMFreeCam.GetComponent<SelectedNpc>().CmdHideNpc(gameObject.name);
    }

    private void FixedUpdate()
    {
        if (GMFreeCam!= null) 
        GMFreeCam.GetComponent<SelectedNpc>().cmdSyncPlanePosition(gameObject.name,transform.position, transform.rotation);
    }
}
