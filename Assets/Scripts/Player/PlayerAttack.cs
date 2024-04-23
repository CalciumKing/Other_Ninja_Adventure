using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerStats ps;
    [SerializeField] Weapon initWeapon;
    [SerializeField] Transform[] attackPositions;
    private PlayerMovement playerMovement;

    private Transform currentAttackPosition;
    private float currentAttackRotation;

    private PlayerActions actions;
    private PlayerMana playerMana;
    private PlayerAnimations playerAnim;
    private EnemyBrain target;
    private Coroutine attackCoroutine;

    [SerializeField] ParticleSystem slashFX;
    [SerializeField] float attackRange;

    public Weapon CurrentWeapon { get; set; }

    private void Awake()
    {
        actions = new PlayerActions();
        playerAnim = GetComponent<PlayerAnimations>();
        playerMovement = GetComponent<PlayerMovement>();
        playerMana = GetComponent<PlayerMana>();
    }
    private void Start()
    {
        CurrentWeapon = initWeapon;
        actions.Attack.ClickAttack.performed += ctx => Attack();
        SelectionManager.OnEnemySelected += SetCurrentTarget;
        SelectionManager.OnNoSelection += ResetCurrentTarget;
        EnemyHealth.OnEnemyDead += KilledEnemy;
    }
    private void KilledEnemy()
    {
        target = null;
    }
    private void Update()
    {
        GetFirePosition();
    }
    private void Attack()
    {
        if (target == null)
            return;
        if(attackCoroutine != null)
            StopCoroutine(attackCoroutine);
        attackCoroutine = StartCoroutine(AttackCo());
    }
    private IEnumerator AttackCo()
    {
        if (currentAttackPosition == null)
            yield break;

        if (CurrentWeapon.WeaponType == WeaponType.MAGIC)
        {
            if(playerMana.CurrentMana < CurrentWeapon.RequiredMana)
                yield break;
            MagicAttack();
        }
        else
            MeleeAttack();

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
    private void GetFirePosition()
    {
        Vector2 moveDirection = playerMovement.MoveDirection;

        if (moveDirection.x > 0f)
        {
            currentAttackPosition = attackPositions[1];
            currentAttackRotation = -90f;
        }
        else if (moveDirection.x < 0f)
        {
            currentAttackPosition = attackPositions[3];
            currentAttackRotation = -270f;
        }

        if (moveDirection.y > 0f)
        {
            currentAttackPosition = attackPositions[0];
            currentAttackRotation = 0f;
        }
        else if (moveDirection.y < 0f)
        {
            currentAttackPosition = attackPositions[2];
            currentAttackRotation = -180f;
        }
    }

    private void MagicAttack()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, currentAttackRotation));
        Projectile projectile = Instantiate(CurrentWeapon.ProjectilePrefab, currentAttackPosition.position, rotation);
        projectile.Direction = Vector3.up;
        projectile.damage = CurrentWeapon.Damage;
        projectile.damage = GetAttackDamage();
        playerMana.UseMana(CurrentWeapon.RequiredMana);
    }
    private void MeleeAttack()
    {
        slashFX.transform.position = currentAttackPosition.position;
        slashFX.Play();

        var distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

        if (distanceToTarget <= attackRange)
            target.GetComponent<IDamagable>()?.TakeDamage(GetAttackDamage());
    }
    float GetAttackDamage()
    {
        var damage = ps.BaseDamage;
        damage += CurrentWeapon.Damage;

        var critChance = Random.Range(0, 100);
        if (critChance <= ps.CriticalChance)
            damage += damage * (ps.CriticalDamage / 100);

        return damage;
    }
}