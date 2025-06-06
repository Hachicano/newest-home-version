using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    [SerializeField] private ItemData itemData;
    [SerializeField] private float protectPickupTime = 1.5f;
    [SerializeField] public bool canPick;

    private void OnEnable()
    {
        StartCoroutine("BusyFor", protectPickupTime);
    }

    private void SetupVisual()
    {
        if (itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item Object - " + itemData.itemName;
    }

    public void SetupItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;
        SetupVisual();
    }

    public void PickUpItem()
    {
        if (!Inventory.instance.CanAddItem() && itemData.itemType == ItemType.Equipment)
        {
            rb.velocity = new Vector2(0, 7f);
            PlayerManager.instance.player.fx.CreatePopUpText("Inventory is full !");
            return;
        }
        Debug.Log("Picked up item " + itemData.itemName);
        AudioManager.instance.PlayerSFX(17, transform);  // sfx_item pickup
        Inventory.instance.AddItem(itemData);
        ObjectPoolManager.instance.returnToPool(gameObject);
        // Destroy(gameObject);
    }

    public void resetItem()
    {
        itemData = null;
        canPick = false;
    }

    public IEnumerator BusyFor(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);

        canPick = true;
    }
}
