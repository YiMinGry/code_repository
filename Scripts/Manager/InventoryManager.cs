using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager: Singleton<InventoryManager>
{
    [SerializeField] List<GameItem> Items;

    List<GameItemData> itemList = new List<GameItemData>();

    public void GetItem(GameItem gameItem)
    {
        GameItemData existingItem = itemList.Find(f => f.itemName == gameItem.itemName);

        if (existingItem != null)
        {
            existingItem.itemCount++;
        }
        else
        {
            itemList.Add(new GameItemData
            {
                itemName = gameItem.itemName,
                itemType = gameItem.itemType,
                itemCount = 1
            });
        }

        UIHelper.Instance.uILog.Add(UIHelper.Instance.GetItemIcon(gameItem.itemName), gameItem.itemName + " x 1");
    }

    public InventoryData SaveData()
    {
        InventoryData saveData = new InventoryData();
        saveData.dataList = itemList;
        return saveData;
    }

    public void LoadFromFile(InventoryData saveData)
    {
        foreach (var data in saveData.dataList)
        {
            itemList.Add(new GameItemData
            {
                itemName = data.itemName,
                itemType = data.itemType,
                itemCount = data.itemCount
            });
        }
    }

    public List<GameItemData> GetItemList() 
    {
        return itemList;
    }
}
