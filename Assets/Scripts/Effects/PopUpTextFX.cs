using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpTextFX : PooledFX
{
    private TextMeshPro myText;

    [SerializeField] private float speed;
    [SerializeField] private float disappearanceSpeed;
    [SerializeField] private float colorDisappearanceSpeed;
    [SerializeField] private float lifeTime;
    private float textTimer;

    protected override void Awake()
    {
        myText = GetComponent<TextMeshPro>();
    }

    protected override void Start()
    {
        textTimer = lifeTime;
    }

    protected override void Update()
    {
        textTimer -= Time.deltaTime;

        if (textTimer < 0) 
        {
            float alpha = myText.color.a - colorDisappearanceSpeed * Time.deltaTime;
            myText.color = new Color(myText.color.r, myText.color.g, myText.color.b ,alpha);

            if (myText.color.a <= 0.5f)
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), disappearanceSpeed * Time.deltaTime);
        }
        else
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), speed * Time.deltaTime);

        if (myText.color.a <= 0)
            ObjectPoolManager.instance.returnToPool(gameObject);
        //  Destroy(gameObject);
    }

    public override void resetFX()
    {
        base.resetFX();
        myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, 1.0f);
    }

}
