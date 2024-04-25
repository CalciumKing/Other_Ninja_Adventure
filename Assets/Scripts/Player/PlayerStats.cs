using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStats/Create new Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Health")]
    public float CurrentHealth;
    public float MaxHealth;

    [Header("Mana")]
    public float CurrentMana;
    public float MaxMana;

    [Header("XP")]
    public float CurrentLevel;
    public float CurrentXP;
    public float TotalXP;
    public float NextLevelXP;
    public float InitLevelXP;
    [Range(1f, 100)]
    public float XPMultiplier;

    [Header("Damage")]
    public float BaseDamage;
    public float CriticalDamage;
    public float CriticalChance;
    public float TotalDamage;
    public void ResetPlayer()
    {
        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;
        CurrentLevel = 1;
        CurrentXP = 0f;
        NextLevelXP = InitLevelXP;
        TotalXP = 0;
    }
}