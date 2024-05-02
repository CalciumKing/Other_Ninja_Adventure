using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] PlayerStats ps;
    public float CurrentMana { get; private set; }
    private void Start()
    {
        CurrentMana = ps.CurrentMana;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseMana(2f);
        }
    }
    public bool CanRestoreMana()
    {
        return ps.CurrentMana > 0f && ps.CurrentMana < ps.MaxMana;
    }
    public void UseMana(float amount)
    {
        ps.CurrentMana = Mathf.Max(ps.CurrentMana -= amount, 0f);
        CurrentMana = ps.CurrentMana;
    }
    public void ResetMana()
    {
        CurrentMana = ps.CurrentMana;
    }
    public void RestoreMana(float amount)
    {
        ps.CurrentMana += amount;
        ps.CurrentMana = Mathf.Min(ps.CurrentMana, ps.MaxMana);
    }
}