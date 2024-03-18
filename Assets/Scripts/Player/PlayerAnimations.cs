using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    readonly int moveX = Animator.StringToHash("moveX");
    readonly int moveY = Animator.StringToHash("moveY");
    readonly int isMoving = Animator.StringToHash("isMoving");
    readonly int gotKilled = Animator.StringToHash("gotKilled");
    readonly int revived = Animator.StringToHash("revived");

    Animator anim;

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
    public void handleReviveAnimation()
    {
        HandleMovingAnimations(Vector2.down);
        anim.SetTrigger(revived);
    }
}