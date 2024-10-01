using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HpController : NetworkBehaviour
{
    [SerializeField] private TMP_InputField _currentHealth;
    [SerializeField] private TMP_InputField _maxHealth;

    public float MaxHealth = 16;
    public float Health = 16;
    public float TempHealth = 0;
    public List<GameObject> HelathObjects;
    public List<GameObject> TempHealthObjects;

    public void Update()
    {

        foreach (GameObject p in HelathObjects)
        {
            if (p.transform.localScale.x == 0)
                p.SetActive(false);
            else
                p.SetActive(true);
            p.transform.localScale = new Vector3((Health * 1) / MaxHealth, 1, 1);
        }
        foreach (GameObject p in TempHealthObjects)
        {
            if (p.transform.localScale.x == 0)
                p.SetActive(false);
            else
                p.SetActive(true);
            p.transform.localScale = new Vector3((TempHealth * 1) / MaxHealth, 1, 1);
        }
    }

    public void UpdateHealth()
    {
        float healthValue;
        if (float.TryParse(_maxHealth.text, out healthValue))
        {
            Health = healthValue;
        }
        float currentHealth;
        if (float.TryParse(_currentHealth.text, out currentHealth))
        {
            MaxHealth = currentHealth;
        }
        CmdUpdateHealth(Health, MaxHealth);
    }

    [Command]
    public void CmdUpdateHealth(float Health, float MaxHealth)
    {
        this.Health = Health;
        this.MaxHealth = MaxHealth;
        RpcUpdateHealth(Health, MaxHealth);
    }

    [ClientRpc]
    public void RpcUpdateHealth(float Health, float MaxHealth)
    {
        this.Health = Health;
        this.MaxHealth = MaxHealth;
    }
}
