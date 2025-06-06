using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Trigger must be on the Default Layer, otherwise it wont be detected by the player !!!!!!!!
public class ItemObjectTrigger : MonoBehaviour
{
    private ItemObject myItemObject => GetComponentInParent<ItemObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<CharacterStats>().isDead)
                return;

            if (!myItemObject.canPick)
                return;
            Debug.Log("Pick up item");
            myItemObject.PickUpItem();
        }
    }
}
