using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] PlayerStats ps;
    private PlayerAnimations playerAnimation;
    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimations>();
    }
    public void TakeDamage(float amount)
    {
        if (ps.CurrentHealth <= 0f) return;
        ps.CurrentHealth -= amount;
        DamageManager.i.ShowDamageText(amount, this.transform);
        if (ps.CurrentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        playerAnimation.HandleDeadAnimation();
    }
    public bool CanRestoreHealth()
    {
        return ps.CurrentHealth > 0f && ps.CurrentHealth < ps.MaxHealth;
    }
    public void RestoreHealth(float amount)
    {
        ps.CurrentHealth += amount;
        if (ps.CurrentHealth >= ps.MaxHealth)
            ps.CurrentHealth = ps.MaxHealth;
    }
}