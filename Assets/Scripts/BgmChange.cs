using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmChange : MonoBehaviour
{
    [SerializeField] private int defaultBgmIndex = 0;
    [SerializeField] private int changeBgmIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            AudioManager.instance.PlayBGM(changeBgmIndex);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            AudioManager.instance.PlayBGM(defaultBgmIndex);
        }
    }
}
