using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageFX : PooledFX
{
    [SerializeField] private SpriteRenderer sr;  // If it is private then just make it serializeField and drag it in by yourself
    private float colorLoseRate;

    protected override void Update()
    {
        base.Update();
        float alpha = sr.color.a - colorLoseRate * Time.deltaTime;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

        if (sr.color.a <= 0)
            ObjectPoolManager.instance.returnToPool(gameObject);
            //Destroy(gameObject);
    }

    public override void resetFX()
    {
        base.resetFX();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
    }

    public void SetupAfterImage(float _losingSpeed, Sprite _spriteImage)
    {
        sr.sprite = _spriteImage;
        colorLoseRate = _losingSpeed;
    }
}
