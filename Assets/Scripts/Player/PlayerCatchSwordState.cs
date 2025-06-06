using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCatchSwordState : PlayerState
{
    private Transform sword;
    public PlayerCatchSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void SetupTransitions()
    {
        base.SetupTransitions();
        this.transitions.Add(new Transition(player.idleState, () => triggerCalled));
    }

    public override void Enter()
    {
        base.Enter();

        sword = player.sword.transform;

        player.fx.PlayDustFX();
        player.fx.ScreenShake(player.fx.shakeCatchSword);

        if (player.transform.position.x < sword.position.x && player.facingDir == -1)
        {
            player.Flip();
        }
        else if (player.transform.position.x > sword.position.x && player.facingDir == 1)
        {
            player.Flip();
        }

        rb.velocity = new Vector2(player.swordReturnImpact * -player.facingDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine(nameof(player.BusyFor), .1f);
    }

    public override void Update()
    {
        base.Update();

        /*
        if (triggerCalled)
        {
            stateMachine.changeState(player.idleState);
        }
        */
    }
}
