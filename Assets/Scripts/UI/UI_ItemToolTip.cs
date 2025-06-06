using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : UI_ToolTip
{

    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [SerializeField] private int defaultItemNameTextFontSize = 36;

    public void ShowTollTip(ItemData_Equipment _item)
    {
        if (_item == null)
            return;

        itemNameText.text = _item.itemName;
        itemTypeText.text = _item.GetEquipmentType();
        itemDescription.text = _item.GetDiscription();

        if (itemNameText.text.Length > 15)  // Maybe can use font size attribute ?
        {
            itemNameText.fontSize = itemNameText.fontSize * .85f;
        }
        else
        {
            itemNameText.fontSize = defaultItemNameTextFontSize;
        }

        AdjustPosition();

        gameObject.SetActive(true);
    }


    public void HideToolTip()
    {
        gameObject.SetActive(false);
        itemNameText.fontSize = defaultItemNameTextFontSize;
    }
}
