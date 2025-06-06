using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string __animBoolName) : base(_player, _stateMachine, __animBoolName)
    {
    }

    public override void SetupTransitions()
    {
        base.SetupTransitions();
        this.transitions.Add(new Transition(player.moveState, () => xInput != 0 && !player.isBusy));
    }

    public override void Enter()
    {
        base.Enter();
        player.setZeroVelocity(); // 做冰面的话另算，就不在在里将速度置为0，应该改在PlayerGroundState中置0
    }

    public override void Update()
    {
        base.Update();

        /*
        if (xInput != 0 && !player.isBusy)
        {
            stateMachine.changeState(player.moveState);
            return;
        }
        */
    }

    public override void Exit()
    {
        base.Exit();
    }
}
