using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberCounty : NetworkBehaviour
{
    public int CurrentNumber;
    [SerializeField] private UpdateNumbers _numbersUpdater;

    private void Start()
    {
        if (!isLocalPlayer) { return; }
    }

    public void AddNumber()
    {

        _numbersUpdater.Number += CurrentNumber;
    }
}
