using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    NONE, WANDER, PATROL, CHASE, ATTACK
}
public class EnemyBrain : MonoBehaviour
{
    [SerializeField] EnemyState enemyState;
    [SerializeField] FSM_State[] states;
    public FSM_State CurrentState { get; set; }
    public Transform Player { get; set; }
    private void Start()
    {
        ChangeState(enemyState);
    }
    private void Update()
    {
        if (CurrentState == null) return;
        UpdateState(this);
        CurrentState.UpdateState(this);
    }
    public void UpdateState(EnemyBrain brain)
    {
        CurrentState.ExecuteActions();
        CurrentState.ExecuteTransitions(brain);
    }
    private FSM_State GetState(EnemyState enemyStateID)
    {
        for (int i = 0;  i < states.Length; i++)
        {
            if (states[i].EnemyStateID == enemyStateID)
            {
                return states[i];
            }
        }
        return null;
    }
    public void ChangeState(EnemyState newStateID)
    {
        FSM_State newState = GetState(newStateID);
        if (newState == null) return;
        CurrentState = newState;
    }
}