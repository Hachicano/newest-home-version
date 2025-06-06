using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfArcherAttackState : EnemyState
{
    protected Enemy_ElfArcher enemy;

    public ElfArcherAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_ElfArcher _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.setZeroVelocity();
        if (triggerCalled)
        {
            stateMachine.changeState(enemy.battleState);
            return;
        }
    }
}
