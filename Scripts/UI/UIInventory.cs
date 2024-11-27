using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : UIBase
{
    [SerializeField] GameObject itemBase;
    [SerializeField] Transform content;

    private void OnEnable()
    {
        foreach (GameItemData data in InventoryManager.Instance.GetItemList())
        {
            UIItemBase item = Instantiate(itemBase, content).GetComponent<UIItemBase>();
            item.Set(data);
        }
    }
}
