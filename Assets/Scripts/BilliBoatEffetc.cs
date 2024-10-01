using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BilliBoatEffetc : MonoBehaviour
{
    public Camera mainCamera; // Камера, к которой будет поворачиваться текст
    public TextMeshPro textMeshPro; // Ссылка на компонент TextMeshPro

    void Start()
    {
        // Если камера не указана в инспекторе, найти главную камеру
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Поворачиваем объект так, чтобы его передняя часть всегда смотрела на камеру
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                             mainCamera.transform.rotation * Vector3.up);
        }
    }
}
