using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddOptions : NetworkBehaviour
{
    public List<GameObject> SelectPlayers;
    public List<TMP_Dropdown.OptionData> Data = new List<TMP_Dropdown.OptionData>();
    public TMP_Dropdown Dropdown;

    public void ClearDropdown()
    {
        Dropdown.ClearOptions();
        Data.Clear();
    }

    // Метод для добавления имени в Dropdown (только на клиенте)
    public void AddOptionsToDropdown(string name)
    {
        Data.Add(new TMP_Dropdown.OptionData(name));
    }

    // Метод для обновления отображения Dropdown
    public void RefreshDropdown()
    {
        Dropdown.AddOptions(Data);
        Dropdown.RefreshShownValue();
    }

}
