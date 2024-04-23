using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] float health;
    private Animator anim;
    private EnemyBrain brain;
    private EnemySelector selector;

    public static event Action OnEnemyDead;
    public float CurrentHealth { get; private set; }

    void Awake()
    {
        anim = GetComponent<Animator>();
        brain = GetComponent<EnemyBrain>();
        selector = GetComponent<EnemySelector>();
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
            anim.SetTrigger("gotKilled");
            brain.enabled = false;
            selector.DeactivateSelector();
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            OnEnemyDead?.Invoke();
        }
        else
        {
            DamageManager.i.ShowDamageText(amount, transform);
        }
    }
}