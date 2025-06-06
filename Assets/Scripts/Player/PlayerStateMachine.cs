using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState currentState {  get; private set; }
    public PlayerState oldState { get; private set; }


    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void changeState(PlayerState _newState)
    {
        currentState.Exit();
        oldState = currentState;
        currentState = _newState;
        currentState.Enter();
        Debug.Log("�����״̬" + currentState);
    }
}
