using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    [SerializeField] UpgradeValues[] settings;
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