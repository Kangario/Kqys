using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedNpc : NetworkBehaviour
{
    // Указываем слой для выбора объектов
    public Camera CameraGM;
    public Canvas UIGM;
    public LayerMask selectableLayer;
    // Объект, на который наведен курсор
    public GameObject currentSelectedObject;

    void Update()
    {
        // Создаем луч от камеры через позицию мыши
        Ray ray = CameraGM.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Выполняем Raycast, проверяя только объекты на нужном слое
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Если объект изменился (наведен на новый объект)
            if (currentSelectedObject != hitObject)
            {
                // Сбрасываем подсветку предыдущего объекта, если он был
                if (currentSelectedObject != null)
                {
                    DeselectObject(currentSelectedObject);
                }

                // Выделяем новый объект
                SelectObject(hitObject);
                currentSelectedObject = hitObject;
            }
        }
        else
        {
            // Если курсор больше не наведен на объект, сбрасываем предыдущий выбор
            if (currentSelectedObject != null)
            {
                DeselectObject(currentSelectedObject);
                currentSelectedObject = null;
            }
        }



        if (Input.GetMouseButton(0) && currentSelectedObject != null)
        {
            currentSelectedObject.GetComponent<NPCController>().SetGMStats(CameraGM, UIGM, GetComponent<FreeCam>());
            currentSelectedObject.GetComponent<NPCController>().SetActiveNPC();
        }
    }

    // Метод для выделения объекта (например, изменение его цвета)
    void SelectObject(GameObject obj)
    {
        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (objRenderer != null)
        {
            objRenderer.material.color = Color.green; // Меняем цвет для индикации выбора
        }
    }

    // Метод для сброса выделения (восстанавливаем оригинальный цвет)
    void DeselectObject(GameObject obj)
    {
        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (objRenderer != null)
        {
            objRenderer.material.color = Color.white; // Восстанавливаем оригинальный цвет
        }
    }

    [Command]
    public void cmdSyncPlanePosition(string npc, Vector3 currentPosition, Quaternion currentRotation)
    {
        GameObject NPC = GameObject.Find(npc);
        NPC.transform.position = currentPosition;
        NPC.transform.rotation = currentRotation;
        ServerSyncPlayer(npc, currentPosition, currentRotation);
    }

    [ClientRpc]
    public void ServerSyncPlayer(string npc, Vector3 currentPosition, Quaternion currentRotation)
    {
        GameObject NPC = GameObject.Find(npc);
        NPC.transform.position = currentPosition;
        NPC.transform.rotation = currentRotation;
    }

    [Command]
    public void CmdHideNpc(string name)
    {
        GameObject NPC = GameObject.Find(name);
        NPC.SetActive(false);
        RpcHideNpc(name);
    }

    [ClientRpc]
    public void RpcHideNpc(string name)
    {
        GameObject NPC = GameObject.Find(name);
        NPC.SetActive(false);
    }
}
