using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FSM_State
{
    public EnemyState EnemyStateID;
    public FSM_Action[] Actions;
    public FSM_Transitions[] Transitions;

    public void UpdateState(EnemyBrain brain)
    {
        ExecuteActions();
        ExecuteTransitions(brain);
    }
    public void ExecuteActions()
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act();
        }
    }
    public void ExecuteTransitions(EnemyBrain brain)
    {
        if (Transitions == null || Transitions.Length <= 0) return;
        for (int i = 0; i < Transitions.Length; i++)
        {
            bool value = Transitions[i].Decision.Decide();
            if (value)
            {
                if (Transitions[i].TrueState != EnemyState.NONE)
                {
                    brain.ChangeState(Transitions[i].TrueState);
                }
            }
            else
            {
                if (Transitions[i].TrueState != EnemyState.NONE)
                {
                    brain.ChangeState(Transitions[i].FalseState);
                }
            }
        }
    }
}