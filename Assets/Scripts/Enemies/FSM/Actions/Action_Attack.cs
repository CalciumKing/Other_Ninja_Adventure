using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Attack : FSM_Action
{
    [SerializeField] float attackSpeed;
    [SerializeField] float damage;
    private EnemyBrain brain;
    private float attackTimer;
    private void Awake()
    {
        brain = GetComponent<EnemyBrain>();
    }
    private void Start()
    {
        attackTimer = attackSpeed;
    }
    public override void Act()
    {
        AttackPlayer();
    }
    private void AttackPlayer()
    {
        if (brain.Player == null) return;
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            IDamagable player = brain.Player.GetComponent<IDamagable>();
            player.TakeDamage(damage);
            attackTimer = attackSpeed;
        }
    }
}