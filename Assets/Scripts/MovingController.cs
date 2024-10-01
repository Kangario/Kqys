using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MovingController : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnCanMoveChanged))]
    public bool UnLimitedMove;
    public bool CanMove;
    public GameObject CurrentPlayer;
    public float maxFoots = 10f;
    public Vector2 startPosition;
    public FirstPersonController firstPersonController;
    public TextMeshProUGUI outputFoots;

    public void OnCanMoveChanged(bool oldValue, bool newValue)
    {
        CanMove = !CanMove;
    }

    // Начинаем движение и сохраняем начальную позицию
    public void StartMoving()
    {
        currentFoots = 0;
        CanMove = true;
        startPosition = CurrentPlayer.transform.position;
        if (firstPersonController != null)
        {
            firstPersonController.playerCanMove = true; // Включаем модуль передвижения
        }
    }

    // Останавливаем движение
    public void StopMoving()
    {
        if (firstPersonController != null)
        {
            firstPersonController.playerCanMove = false; // Отключаем модуль передвижения
        }
    }

    public void ShowFoots(float distanceMoved)
    {
        outputFoots.text = Math.Round(distanceMoved).ToString() + " FT";
    }

    float currentFoots;
    void Update()
    {
        if (!UnLimitedMove)
        {
                
        if (CanMove)
        {
            // Если бесконечное передвижение включено, не проверяем дистанцию
          
                float distanceMoved = Vector2.Distance(startPosition, CurrentPlayer.transform.position);
                currentFoots = distanceMoved;
                ShowFoots(currentFoots*3);
                // Проверяем, достиг ли игрок максимального расстояния
                if (distanceMoved >= maxFoots)
                {
                    currentFoots = maxFoots;
                    CanMove = false; // Останавливаем движение
                    StopMoving();
                }
            }
        }
    }

    // Метод для переключения режима бесконечного передвижения
    [Command]
    public void CmdToggleUnlimitedMove(bool unlimitedMove)
    {
        UnLimitedMove = unlimitedMove; // Обновляем флаг на сервере
    }
}
