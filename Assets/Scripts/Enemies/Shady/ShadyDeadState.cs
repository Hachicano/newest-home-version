using UnityEngine;


public class ShadyDeadState : EnemyState
{
    protected Enemy_Shady enemy;
    [SerializeField] private int explodeSfxIndex = 6;

    public ShadyDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Shady _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlayerSFX(explodeSfxIndex, null);
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
            enemy.SelfDestroy();
    }
}
