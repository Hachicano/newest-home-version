using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrikeController : PooledFX
{
    private Animator anim;
    private string initialAnimation;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>()!= null)
        {
            PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            EnemyStats target = collision.GetComponent<EnemyStats>();

            playerStats.DoMagicalDamage(target);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        initialAnimation = "thunderStrike_idle";

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(returnPoolAfterDelay(1f));
    }

    public override void resetFX()
    {
        base.resetFX();
        if (anim != null)
        {
            // 重置所有参数
            anim.Rebind(); // 关键方法：强制重置动画状态
            anim.Update(0f); // 立即更新动画状态

            // 强制播放初始动画（可选）
            anim.Play(initialAnimation, 0, 0f);
        }
    }

    IEnumerator returnPoolAfterDelay(float delay)
    {
        // 等待1秒
        yield return new WaitForSeconds(delay);
        ObjectPoolManager.instance.returnToPool(gameObject);
    }

}
