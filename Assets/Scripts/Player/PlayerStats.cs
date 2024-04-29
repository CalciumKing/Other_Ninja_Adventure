using UnityEngine;

public enum AttributeType
{
    STRENGTH,
    DEXTERITY,
    INTELLIGENCE
}

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

    [Header("Attributes")]
    public int Strength;
    public int Dexterity;
    public int Intelligence;
    public int AvailablePoints;

    public void ResetPlayer()
    {
        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;
        CurrentLevel = 1;
        CurrentXP = 0f;
        NextLevelXP = InitLevelXP;
        TotalXP = 0;

        BaseDamage = 2;
        CriticalChance = 10;
        CriticalDamage = 50;
        Strength = 0;
        Dexterity = 0;
        Intelligence = 0;
        AvailablePoints = 0;
    }
}