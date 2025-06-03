using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    [SerializeField] private int deathScreenSfxIndex = 11;

    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void SetupTransitions()
    {
        base.SetupTransitions();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlayerSFX(deathScreenSfxIndex, null);
        GameObject.Find("Canvas").GetComponent<UI>().SwitchOnEndScreen();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.setZeroVelocity();
    }
}
