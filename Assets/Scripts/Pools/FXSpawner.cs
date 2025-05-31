using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSpawner : ObjectSpawner
{
    private PooledFX fx;

    private void Awake()
    {
        fx = GetComponent<PooledFX>();
    }

    public override void initialize()
    {
        base.initialize();
        fx.resetFX();
        popCount++;
        Debug.Log(gameObject.name + " :FX initialization has down");
    }

}
