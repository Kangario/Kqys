using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorController : NetworkBehaviour
{
    [SerializeField] private FirstPersonController firstPersonController;

    private void OnEnable()
    {
        if (!isLocalPlayer) { return; }
        LockCursor();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            firstPersonController.cameraCanMove = false;
            firstPersonController.playerCanMove = false;
            UnlockCursor();
        }
        if (Input.GetMouseButton(1))
        {
            firstPersonController.cameraCanMove = true;
            firstPersonController.playerCanMove = true;
            LockCursor();
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // Блокируем курсор в центре экрана
        Cursor.visible = false; // Скрываем курсор
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None; // Разблокируем курсор
        Cursor.visible = true; // Делаем курсор видимым
    }
}
