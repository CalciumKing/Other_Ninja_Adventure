using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Wander : FSM_Action
{
    [SerializeField] float moveSpeed;
    [SerializeField] float wanderTime;
    [SerializeField] Vector2 moveRange;

    private float moveTimer;
    private Vector3 moveDestination;

    private void Start()
    {
        GetNewDestination();
    }
    public override void Act()
    {
        moveTimer -= Time.deltaTime;
        var moveDirection = (moveDestination - transform.position).normalized;
        var movement = moveDirection * (moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, moveDirection) >= .5)
        {
            transform.Translate(movement);
        }
        if (moveTimer <= 0)
        {
            GetNewDestination();
            moveTimer = wanderTime;
        }
    }
    private void GetNewDestination()
    {
        var randomX = Random.Range(-moveDestination.x, moveDestination.x);
        var randomY = Random.Range(-moveDestination.y, moveDestination.y);

        moveDestination = transform.position + new Vector3(randomX, randomY);
        print(moveDestination);
    }
    private void OnDrawGizmosSelected()
    {
        if (moveRange != Vector2.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, moveRange * 2f);
            Gizmos.DrawLine(transform.position, moveDestination);
        }
    }
}