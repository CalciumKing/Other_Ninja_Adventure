using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerActions actions;
    private PlayerAnimations playerAnim;
    private EnemyBrain target;
    Coroutine attackCoroutine;
    private void Awake()
    {
        actions = new PlayerActions();
        playerAnim = GetComponent<PlayerAnimations>();

    }
    private void Start()
    {
        actions.Attack.ClickAttack.performed += ctx => Attack();
        SelectionManager.OnEnemySelected += SetCurrentTarget;
        SelectionManager.OnNoSelection += ResetCurrentTarget;
    }
    private void Attack()
    {
        if (target == null) return;
        if(attackCoroutine != null)
            StopCoroutine(attackCoroutine);
        attackCoroutine = StartCoroutine(AttackCo());
    }
    IEnumerator AttackCo()
    {
        playerAnim.SetAttackingAnimation(true);
        yield return new WaitForSeconds(.5f);
        playerAnim.SetAttackingAnimation(false);
    }
    private void SetCurrentTarget(EnemyBrain selectedTarget)
    {
        target = selectedTarget;
    }
    private void ResetCurrentTarget()
    {
        target = null;
    }
    private void OnEnable()
    {
        actions.Enable();
    }
    private void OnDisable()
    {
        actions.Disable();
    }
}