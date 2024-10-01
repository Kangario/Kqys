using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolateCamera : MonoBehaviour
{
    public Transform player;
    public Transform target; // Head bone или другой объект, за которым должна следить камера
    public float smoothSpeed = 0.125f; // Скорость сглаживания
    public Vector3 offset; // Смещение относительно цели

    private void LateUpdate()
    {
        // Преобразуем смещение в локальные координаты относительно текущего поворота игрока
        Vector3 rotatedOffset = player.rotation * offset;

        // Вычисляем желаемую позицию камеры с учетом смещения и поворота игрока
        Vector3 desiredPosition = target.position + rotatedOffset;

        // Интерполяция между текущей позицией камеры и желаемой позицией
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Обновляем только позицию камеры
        transform.position = smoothedPosition;

    }
}
