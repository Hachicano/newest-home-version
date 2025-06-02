using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Animator anim; 
    public bool isOpened = false;
    public bool playerInRange = false;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private ItemData dropItemData;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            OpenChest();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            playerInRange = false;
    }

    public void OpenChest()
    {
        // 播放开宝箱动画
        anim.SetBool("isOpen", true);
        isOpened = true;

        Debug.Log("宝箱被打开！");
    }

    public void Drop()
    {
        // GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);
        GameObject newDrop = ObjectPoolManager.instance.getPooledObject(dropPrefab, transform.position, Quaternion.identity);

        Vector2 randomVelocity = new Vector2(Random.Range(0, 0), Random.Range(15, 20));

        newDrop.GetComponent<ItemObject>().SetupItem(dropItemData, randomVelocity);
    }


}
