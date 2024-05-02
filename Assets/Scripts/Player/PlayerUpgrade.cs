using System;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public static event Action OnPlayerUpgrade;

    [SerializeField] PlayerStats ps;
    [SerializeField] UpgradeValues[] settings;

    private void Start()
    {
        AttributeButton.OnAttributePurchased += AttributePurchased;
    }

    private void AttributePurchased(AttributeType attributeType)
    {
        if (ps.AvailablePoints == 0) return;

        switch (attributeType)
        {
            case AttributeType.STRENGTH:
                UpgradePlayer(0);
                ps.Strength++;
                break;
            case AttributeType.DEXTERITY:
                UpgradePlayer(1);
                ps.Dexterity++;
                break;
            case AttributeType.INTELLIGENCE:
                UpgradePlayer(2);
                ps.Intelligence++;
                break;
        }
        ps.AvailablePoints--;
        OnPlayerUpgrade?.Invoke();
    }

    void UpgradePlayer(int index)
    {
        ps.BaseDamage += settings[index].DamageIncrease;
        ps.TotalDamage += settings[index].DamageIncrease;
        ps.MaxHealth += settings[index].HealthIncrease;
        ps.CurrentHealth += ps.MaxHealth;
        ps.MaxMana += settings[index].ManaIncrease;
        ps.CurrentMana += ps.MaxMana;
        ps.CriticalChance += settings[index].CChanceIncrease;
        ps.CriticalDamage += settings[index].CDamageIncrease;
    }

}

[System.Serializable]
public class UpgradeValues
{
    public string name;
    public float DamageIncrease;
    public float HealthIncrease;
    public float ManaIncrease;
    public float CChanceIncrease;
    public float CDamageIncrease;
}