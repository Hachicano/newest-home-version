using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockStrikeController : PooledFX
{
    [SerializeField] private CharacterStats targetStats;
    [SerializeField] private float speed;
    private float damage;

    private Animator anim;
    private string initialAnimation;
    private bool triggered;

    public void Setup(float _damage, CharacterStats _targetStats)
    {
        damage = _damage;
        targetStats = _targetStats;
    }

    protected override void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        initialAnimation = "shockStrike_idle";
    }

    protected override void Update()
    {
        if (!targetStats)
        {
            ObjectPoolManager.instance.returnToPool(gameObject);
            return;
        }


        if (triggered)
            return;

        transform.position = Vector2.MoveTowards(transform.position, targetStats.transform.position, speed * Time.deltaTime);
        transform.right = transform.position - targetStats.transform.position;

        if (Vector2.Distance(transform.position, targetStats.transform.position) < .1f && !triggered)
        {
            anim.transform.localPosition = new Vector3(0, .5f);
            anim.transform.localRotation = Quaternion.identity;

            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(3, 3);

            Invoke("DamageAndSelfDestroy", .2f);
            anim.SetTrigger("Hit");
            triggered = true;
        }
    }

    private void DamageAndSelfDestroy()
    {
        targetStats.TakeDamage(damage);
        targetStats.ApplyShock(true);
        StartCoroutine(returnPoolAfterDelay(0.4f));
        //Destroy(gameObject, .4f);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void resetFX()
    {
        base.resetFX();
        triggered = false;
        targetStats = null;

        if (anim != null)
        {
            // �������в���
            anim.Rebind(); // �ؼ�������ǿ�����ö���״̬
            anim.Update(0f); // �������¶���״̬

            // ǿ�Ʋ��ų�ʼ��������ѡ��
            anim.Play(initialAnimation, 0, 0f);
        }
    }

    IEnumerator returnPoolAfterDelay(float delay)
    {
        // �ȴ�0.5��
        yield return new WaitForSeconds(delay);
        ObjectPoolManager.instance.returnToPool(gameObject);
    }
}
