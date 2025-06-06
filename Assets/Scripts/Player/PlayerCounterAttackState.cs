using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    private bool canCreateClone;

    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string __animBoolName) : base(_player, _stateMachine, __animBoolName)
    {
    }

    public override void SetupTransitions()
    {
        base.SetupTransitions();
        this.transitions.Add(new Transition(player.idleState, () => stateTimer < 0 || triggerCalled));
    }

    public override void Enter()
    {
        base.Enter();
        canCreateClone = true;
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
        stateTimer = 0;
    }

    public override void Update()
    {
        base.Update();
        player.setZeroVelocity();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<ArrowController>() != null)
            {
                hit.GetComponent<ArrowController>().CounterArrow();
                SuccessfulCounterAttack();
                player.skill.parry.UseSkill();  // gonna use skill to restore health on parry
            }

            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    SuccessfulCounterAttack();

                    player.skill.parry.UseSkill(); // gonna use skill to restore health on parry

                    if (canCreateClone)
                    {
                        canCreateClone = false;
                        player.skill.parry.MakeMirageOnParry(hit.transform, .2f);
                    }
                }
            }
        }

        /*
        if (stateTimer < 0 || triggerCalled)
        {
            stateMachine.changeState(player.idleState);
            return;
        }
        */
    }

    private void SuccessfulCounterAttack()
    {
        stateTimer = 10; // any value bigger than 1, we need to make sure dont exit this state before we finish it actively
        player.anim.SetBool("SuccessfulCounterAttack", true);
    }
}
