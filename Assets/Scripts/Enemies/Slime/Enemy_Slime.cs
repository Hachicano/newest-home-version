using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlimeType { big, medium, small}

public class Enemy_Slime : Enemy
{
    [Header("Slime Specific")]
    [SerializeField] private SlimeType slimeType;
    [SerializeField] private int slimesToCreateValue;
    [SerializeField] private GameObject slimeToCreatePrefab;
    [SerializeField] private Vector2 minCreationVelocity;
    [SerializeField] private Vector2 maxCreationVelocity;

    #region States
    public SlimeIdleState idleState {  get; private set; }
    public SlimeMoveState moveState { get; private set; }
    public SlimeBattleState battleState { get; private set; }
    public SlimeAttackState attackState { get; private set; }
    public SlimeStunnedState stunnedState { get; private set; }
    public SlimeDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        SetupDefaultFacingDir(-1);
        idleState = new SlimeIdleState(this, stateMachine, "Idle", this);
        moveState = new SlimeMoveState(this, stateMachine, "Move", this);
        battleState = new SlimeBattleState(this, stateMachine, "Move", this);
        attackState = new SlimeAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SlimeStunnedState(this, stateMachine, "Stunned", this);
        deadState = new SlimeDeadState(this, stateMachine, "Idle", this);
        defaultState = idleState;
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.changeState(stunnedState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.changeState(deadState);

        if (slimeType == SlimeType.small)
            return;

        CreateSlimes(slimesToCreateValue, slimeToCreatePrefab);
    }

    private void CreateSlimes(int _amountOfSlimes, GameObject _slimePrefab)
    {
        for (int i = 0; i < _amountOfSlimes; i++)
        {
            //GameObject newSlime = Instantiate(_slimePrefab, transform.position, Quaternion.identity);
            GameObject newSlime = ObjectPoolManager.instance.getPooledObject(_slimePrefab, transform.position, Quaternion.identity);
            newSlime.GetComponent<Enemy_Slime>().SetupSlime(facingDir);
        }
    }

    public void SetupSlime(int _facingDir) // can inherit level from the parent as well
    {
        //if (_facingDir != facingDir)
        //    Flip();

        float xVelocity = Random.Range(minCreationVelocity.x, maxCreationVelocity.x);
        float yVelocity = Random.Range(minCreationVelocity.y, maxCreationVelocity.y);

        isKnocked = true;

        int ejectDir = Random.Range(-1, 1);
        if (ejectDir == 0)
            ejectDir = _facingDir;

        GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * -ejectDir, yVelocity);

        Invoke("CancelKnockback", 1.5f);
    }

    private void CancelKnockback() => isKnocked = false;

    public override void resetEnemy()
    {
        base.resetEnemy();
    }
}
