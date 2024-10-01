using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollerController : NetworkBehaviour
{
    public List<Dice> _dices = new List<Dice>();
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform spawnPointGM;
    [SerializeField] private float rollForce = 10f;
    [SerializeField] private float torqueForce = 10f;
    [SerializeField] private Vector3 randomOffsetRange = new Vector3(1f, 0f, 1f);
    [HideInInspector] public List<Roll> rollValue;

    private void OnEnable()
    {
        if (!isLocalPlayer) { return; }
    }

    public List<Roll> Roll(int currentDice ,int quantityRolls)
    {
        for (int i = 0; i < quantityRolls; i++)
        {

            StartCoroutine(RollDicesRoutine(currentDice));
        }
        return rollValue;
    }

    private IEnumerator RollDicesRoutine(int currentDice)
    {
        // Генерация рандомного смещения в пределах заданного диапазона
        Vector3 randomOffset = new Vector3(
            UnityEngine.Random.Range(-randomOffsetRange.x, randomOffsetRange.x),
            0f, // Смещение по Y не требуется, чтобы кубики оставались на одной высоте
            UnityEngine.Random.Range(-randomOffsetRange.z, randomOffsetRange.z)
        );

        // Создаем экземпляр кубика с учётом смещения
        GameObject diceInstance = Instantiate(
            _dices[currentDice].CurentDice,
            spawnPoint.position + randomOffset,
            UnityEngine.Random.rotation
        );

        diceInstance.AddComponent<Rigidbody>();
        diceInstance.transform.SetParent( spawnPointGM );
        // Получаем Rigidbody для применения физических сил
        Rigidbody rb = diceInstance.GetComponent<Rigidbody>();

        // Применяем силу для броска и вращения
        rb.AddForce(Vector3.up * rollForce, ForceMode.Impulse);
        rb.AddTorque(UnityEngine.Random.insideUnitSphere * torqueForce, ForceMode.Impulse);
        // Определяем, какая сторона кубика оказалась сверху
        rollValue.Add(new Roll(currentDice,DetermineDiceResult(currentDice)));

        yield return new WaitForSeconds(1f);
        // Удаляем кубик после броска
        Destroy(diceInstance);

        yield return new WaitForSeconds(0.5f); // Пауза между бросками
    }
    private int DetermineDiceResult(int currentDice)
    {
        // Если не удалось определить результат (например, кубик на ребре), возвращаем 0
        return UnityEngine.Random.Range(1, _dices[currentDice].NumberDice+1);
    }
}
[Serializable]
public struct Dice
{
    public GameObject CurentDice;
    public int NumberDice;
}

[Serializable]
public struct Roll
{
    public int diceNumber;
    public int value;

    public Roll(int diceNumber, int value)
    {
        this.diceNumber = diceNumber;
        this.value = value;
    }
}