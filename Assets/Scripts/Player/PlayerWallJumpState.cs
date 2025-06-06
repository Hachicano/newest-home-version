using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    private float wallJumpWindow = 0.6f;

    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void SetupTransitions()
    {
        base.SetupTransitions();
        this.transitions.Add(new Transition(player.airborneState, () => stateTimer < 0));
        this.transitions.Add(new Transition(player.wallSlideState, () => player.IsWallDetected()));
        this.transitions.Add(new Transition(player.idleState, () => player.IsGroundDetected()));
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = wallJumpWindow;
        float xv = player.moveSpeed * -player.facingDir * 0.8f;
        float yv = player.jumpForce;
        Debug.Log("wallJump xVelocity is :" + xv);
        player.setVelocity(xv, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
        stateTimer = 0;
    }

    public override void Update()
    {
        base.Update();
        /*
        if (stateTimer < 0)
        {
            stateMachine.changeState(player.airborneState);
            return;
        }

        if (player.IsGroundDetected())
        {
            stateMachine.changeState(player.idleState);
            return;
        }*/
    }
}
