using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralAnimations : MonoBehaviour
{
    public Transform[] footTargets; // IK цели для каждой ноги
    public Transform body;
    public float stepDistance = 0.5f;
    public float stepHeight = 0.3f;
    public float moveSpeed = 2f;
    public LayerMask groundMask;

    private Vector3[] initialPositions;
    private Vector3[] targetPositions;
    private bool[] isMoving;

    void Start()
    {
        // Инициализация позиций ног
        initialPositions = new Vector3[footTargets.Length];
        targetPositions = new Vector3[footTargets.Length];
        isMoving = new bool[footTargets.Length];

        for (int i = 0; i < footTargets.Length; i++)
        {
            initialPositions[i] = footTargets[i].position;
            targetPositions[i] = footTargets[i].position;
        }
    }

    void Update()
    {
        // Двигаем тело вперёд
        body.position += body.forward * moveSpeed * Time.deltaTime;

        for (int i = 0; i < footTargets.Length; i++)
        {
            MoveFoot(i);
        }
    }

    void MoveFoot(int footIndex)
    {
        // Если нога достигла цели
        if (Vector3.Distance(footTargets[footIndex].position, targetPositions[footIndex]) < 0.05f)
        {
            isMoving[footIndex] = false;
        }

        // Если нога не двигается, определяем новую цель
        if (!isMoving[footIndex])
        {
            Vector3 offset = body.forward * stepDistance;
            Ray ray = new Ray(footTargets[footIndex].position + offset, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 2f, groundMask))
            {
                targetPositions[footIndex] = hit.point;
                isMoving[footIndex] = true;
            }
        }

        // Если нога двигается, перемещаем её
        if (isMoving[footIndex])
        {
            Vector3 newFootPosition = Vector3.Lerp(footTargets[footIndex].position, targetPositions[footIndex], Time.deltaTime * moveSpeed);
            newFootPosition.y += Mathf.Sin(Mathf.Clamp01(Vector3.Distance(footTargets[footIndex].position, targetPositions[footIndex]) / stepDistance) * Mathf.PI) * stepHeight;
            footTargets[footIndex].position = newFootPosition;
        }
    }
}
