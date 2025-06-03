using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyExplosionController : MonoBehaviour
{
    [SerializeField] private CharacterStats founderStat;
    [SerializeField] private float growSpeed = 15;
    [SerializeField] private float maxSize = 8;
    [SerializeField] private float explosionRadius;
    public Vector2 knockbacnPower = new Vector2(15, 5);

    private bool canGrow = true;
    private Animator anim;

    private void Update()
    {
        if (canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }

        if (maxSize - transform.localScale.x < .5f)
        {
            canGrow = false;
            anim.SetTrigger("Explode");
        }
    }

    public void SetupExplosion(CharacterStats _founderStat, float _growSpeed, float _maxSize, float _radius)
    {
        anim = GetComponent<Animator>();
        founderStat = _founderStat;
        growSpeed = _growSpeed;
        maxSize = _maxSize;
        explosionRadius = _radius;
    }

    private void AnimationExplodeEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<CharacterStats>() != null)
            {
                hit.GetComponent<Entity>().SetupKnockbackDir(transform);
                hit.GetComponent<Entity>().SetupKnockbackPower(knockbacnPower);
                founderStat.DoDamage(hit.GetComponent<CharacterStats>());
            }
        }
    }

    private void SelfDestroy() => Destroy(gameObject);
}
