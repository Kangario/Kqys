using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Telepathy;
using Unity.VisualScripting;
using UnityEngine;

public class DiceManager : NetworkBehaviour
{
    [SerializeField] private GameObject _namePlayer;
    [SerializeField] private UpdateNumbers _numbers;
    [SerializeField] private DiceRollerController _controllerRolls;
    [SerializeField] private List<DiceCounty> dices;
    [SerializeField] private OutputJornalInformations _jornal;

    private void OnEnable()
    {
        if (!isLocalPlayer) return;
    }

    public void Roll()
    {
        StartCoroutine(RollWithDelay());
    }

    private IEnumerator RollWithDelay()
    {
        foreach (var dice in dices)
        {
            if (dice.County != 0)
            {
                _controllerRolls.Roll(dice.Dice, dice.County);
            }
        }

        yield return new WaitForSeconds(1f);

        string allRolls = "";
        foreach (var roll in _controllerRolls.rollValue)
        {
            Debug.Log(roll.diceNumber);
            allRolls += "1d" + _controllerRolls._dices[roll.diceNumber].NumberDice + "(" + roll.value + ")" + " ";
        }
       Debug.LogWarning(allRolls);
            _jornal.CmdSendMessageRoll(_namePlayer.name+":"+allRolls + "+" + _numbers.Number);

        _controllerRolls.rollValue.Clear();
        ResetButtons();
    }

    private void ResetButtons()
    {
        foreach (var dice in dices)
        {
            dice.ResetCubes();
        }
        _numbers.ResetNumber();
    }
}

