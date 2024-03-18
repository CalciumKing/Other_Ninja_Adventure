using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseMana(2f);
        }
    }
    public void UseMana(float amount)
    {
        if(playerStats.CurrentMana >= amount)
        {
            playerStats.CurrentMana -= amount;
        }
    }
}