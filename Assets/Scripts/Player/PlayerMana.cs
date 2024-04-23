using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    public float CurrentMana { get; private set; }
    private void Start()
    {
        CurrentMana = playerStats.CurrentMana;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseMana(2f);
        }
    }
    public void UseMana(float amount)
    {
        playerStats.CurrentMana = Mathf.Max(playerStats.CurrentMana -= amount, 0f);
        CurrentMana = playerStats.CurrentMana;
    }
    public void ResetMana()
    {
        CurrentMana = playerStats.CurrentMana;
    }
}