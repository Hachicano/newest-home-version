using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string __animBoolName) : base(_player, _stateMachine, __animBoolName)
    {
    }

    public override void SetupTransitions()
    {
        base.SetupTransitions();
        this.transitions.Add(new Transition(player.idleState, () => xInput == 0));
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlayerSFX(14, null);
    }

    public override void Update()
    {
        base.Update();

        
        if (Time.time < player.primaryAttack.lastTimeAttack + 0.12f && player.primaryAttack.comboCounter != 3)
        {
            // Debug.Log("low speed");
            player.setVelocity(xInput * player.moveSpeed * 0.4f, rb.velocity.y);
        }
        else
        {
            player.setVelocity(xInput * player.moveSpeed, rb.velocity.y);
        }
        /*
        if (xInput == 0)
        {
            stateMachine.changeState(player.idleState);
            return;
        }
        */
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(14);  // sfx_footsteps
    }
}
