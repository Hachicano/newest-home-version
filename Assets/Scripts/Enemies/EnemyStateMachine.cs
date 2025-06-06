using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState {  get; private set; }
    public EnemyState oldState { get; private set; }

    public void Initialize(EnemyState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void changeState(EnemyState _newState)
    {
        currentState.Exit();
        oldState = currentState;
        currentState = _newState;
        currentState.Enter();
    }
}
