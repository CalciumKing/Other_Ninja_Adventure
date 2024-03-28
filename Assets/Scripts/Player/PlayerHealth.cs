using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] PlayerStats playerStats;
    PlayerAnimations playerAnimation;
    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimations>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1f);
        }
    }
    public void TakeDamage(float amount)
    {
        if (playerStats.CurrentHealth <= 0f) return;
        playerStats.CurrentHealth -= amount;
        DamageManager.i.ShowDamageText(amount, this.transform);
        if (playerStats.CurrentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        playerAnimation.HandleDeadAnimation();
    }
}