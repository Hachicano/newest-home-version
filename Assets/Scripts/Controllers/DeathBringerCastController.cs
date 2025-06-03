using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerCastController : PooledFX
{
    [SerializeField] private Transform checkbox;
    [SerializeField] private Vector2 boxsize;
    [SerializeField] private LayerMask whatIsPlayer;

    private CharacterStats founderStat;
    private Animator anim;
    private string initialAnimation;

    public void SetUpCast(CharacterStats _founderStat)
    {
        founderStat = _founderStat;
    }

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        initialAnimation = "cast";
    }

    private void AnimationCastTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(checkbox.position, boxsize, whatIsPlayer);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                hit.GetComponent<Entity>().SetupKnockbackDir(transform);
                founderStat.DoMagicalDamage(hit.GetComponent<CharacterStats>());
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkbox.position, boxsize);
    }

    private void SelfDestroy() => ObjectPoolManager.instance.returnToPool(gameObject);

    public override void resetFX()
    {
        base.resetFX();
        if (anim != null)
        {
            // �������в���
            anim.Rebind(); // �ؼ�������ǿ�����ö���״̬
            anim.Update(0f); // �������¶���״̬

            // ǿ�Ʋ��ų�ʼ��������ѡ��
            anim.Play(initialAnimation, 0, 0f);
        }

    }
}
