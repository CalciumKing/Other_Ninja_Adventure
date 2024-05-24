using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    private EnemyLoot eml;
    [SerializeField] float health;
    private Animator anim;
    private EnemyBrain brain;
    private EnemySelector selector;
    private Rigidbody2D rb;

    public static event Action OnEnemyDead;
    public float CurrentHealth { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        brain = GetComponent<EnemyBrain>();
        selector = GetComponent<EnemySelector>();
        eml = GetComponent<EnemyLoot>();
    }

    private void Start()
    {
        CurrentHealth = health;
    }
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0f)
        {
            DisablePlayer();
        }
        else
        {
            DamageManager.i.ShowDamageText(amount, transform);
        }
    }

    private void DisablePlayer()
    {
        anim.SetTrigger("gotKilled");
        brain.enabled = false;
        selector.DeactivateSelector();
        rb.bodyType = RigidbodyType2D.Static;
        OnEnemyDead?.Invoke();
        GameManager.i.AddPlayerXP(eml.XPDropped);
    }
}