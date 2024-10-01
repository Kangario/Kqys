using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceCounty : NetworkBehaviour
{
    public int Dice;
    public int County;
    [SerializeField] private TextMeshProUGUI _label;

    private void OnEnable()
    {
        if (!isLocalPlayer) { return;}
    }

    public void AddCube()
    {
        County++;
        _label.text = County.ToString();
    }
    public void RemoveCube()
    {
        County--;
        _label.text = County.ToString();
    }
    public void ResetCubes()
    {
        County= 0;
        _label.text = null;
    }
}
