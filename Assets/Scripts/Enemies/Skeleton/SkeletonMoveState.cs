public class SkeletonMoveState : SkeletonGroundedState
{
    public SkeletonMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.setVelocity(enemy.facingDir * enemy.moveSpeed, rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.changeState(enemy.idleState);
            return;
        }
    }
}
