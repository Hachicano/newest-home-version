using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

[CreateAssetMenu(fileName = "Thunder Strike Effect", menuName = "Data/Item_Effect/Thunder_Strike")]
public class ThunderStrikeEffect : ItemEffect
{
    [SerializeField] private GameObject thunderStrikePrefab;
    private GameObject newThunderStrike;

    public override void ExecuteEffect(Transform _executeTransform)
    {
        newThunderStrike = ObjectPoolManager.instance.getPooledObject(thunderStrikePrefab, _executeTransform.position, _executeTransform.rotation);
        CoroutineHelper.Instance.ExecuteAfterDelay(1f, returnToPool);
        //Destroy(newThunderStrike, 1f);
    }

    private void returnToPool()
    {
        ObjectPoolManager.instance.returnToPool(newThunderStrike);
    }
}
