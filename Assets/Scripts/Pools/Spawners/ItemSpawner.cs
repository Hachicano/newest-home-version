using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : ObjectSpawner
{
    private ItemObject item;

    private void Awake()
    {
        item = GetComponent<ItemObject>();
    }

    public override void initialize()
    {
        base.initialize();
        item.resetItem();
        popCount++;
    }
}
