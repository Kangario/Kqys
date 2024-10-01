using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateNumbers : NetworkBehaviour
{
    public int Number;
    [SerializeField] private TextMeshProUGUI _label;

    private void OnEnable()
    {
        if (!isLocalPlayer) { return; }
        _label = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateNumber()
    {
        if (Number > 0)
        {
            _label.text ="+"+ Number.ToString();
        }else if (Number < 0)
        {
            _label.text =  Number.ToString();
        }
        else
        {
            _label.text = "0";
        }
    }

    public void ResetNumber()
    {
        Number = 0;
        _label.text = "";
    }
}
