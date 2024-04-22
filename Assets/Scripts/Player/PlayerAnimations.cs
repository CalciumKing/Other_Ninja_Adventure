using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private readonly int moveX = Animator.StringToHash("moveX");
    private readonly int moveY = Animator.StringToHash("moveY");
    private readonly int isMoving = Animator.StringToHash("isMoving");
    private readonly int gotKilled = Animator.StringToHash("gotKilled");
    private readonly int revived = Animator.StringToHash("revived");
    private readonly int isAttacking = Animator.StringToHash("isAttacking");

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void HandleDeadAnimation()
    {
        anim.SetTrigger(gotKilled);
    }
    public void HandleMoveBoolAnimation(bool value)
    {
        anim.SetBool(isMoving, value);
    }
    public void HandleMovingAnimations(Vector2 direction)
    {
        anim.SetFloat(moveX, direction.x);
        anim.SetFloat(moveY, direction.y);
    }
    public void SetAttackingAnimation(bool value)
    {
        anim.SetBool(isAttacking, value);
    }
    public void handleReviveAnimation()
    {
        HandleMovingAnimations(Vector2.down);
        anim.SetTrigger(revived);
    }
}