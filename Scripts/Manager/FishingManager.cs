using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishingManager : Singleton<FishingManager>
{
    [SerializeField] List<GameItem> fishItems;

    List<GameItemData> fishList = new List<GameItemData>();

    public void FishingStart()
    {
        if (EquipmentManager.Instance.FindSlot(EquipmentType.HandR).equippedItem != null)
        {
            EquipmentManager.Instance.FindSlot(EquipmentType.HandR).equippedItem.Hide();
        }

        if (EquipmentManager.Instance.FindSlot(EquipmentType.HandL).equippedItem != null)
        {
            EquipmentManager.Instance.FindSlot(EquipmentType.HandL).equippedItem.Hide();
        }
    }

    public void CatchFish()
    {
        EquipmentSlot slot = EquipmentManager.Instance.EquipItem(EquipmentType.objectRooting, GetRandomFishName());

        GameItemData existingFish = fishList.Find(f => f.itemName == slot.equippedItem.itemName);

        if (existingFish != null)
        {
            existingFish.itemCount++;
        }
        else
        {
            fishList.Add(new GameItemData
            {
                itemName = slot.equippedItem.itemName,
                itemType = slot.equippedItem.itemType,
                itemCount = 1
            });
        }

        UIHelper.Instance.uILog.Add(UIHelper.Instance.GetItemIcon(slot.equippedItem.itemName), slot.equippedItem.itemName + " x 1");
    }

    public FishingData SaveData()
    {
        FishingData saveData = new FishingData();
        saveData.dataList = fishList;
        return saveData;
    }

    public void LoadFromFile(FishingData saveData)
    {
        foreach (var data in saveData.dataList)
        {
            fishList.Add(new GameItemData
            {
                itemName = data.itemName,
                itemType = data.itemType,
                itemCount = data.itemCount
            });
        }
    }

    public string GetRandomFishName()
    {
        if (fishItems == null || fishItems.Count == 0)
        {
            Debug.LogWarning("FishItems list is empty or null.");
            return string.Empty;
        }

        float totalWeight = 0f;
        foreach (var fish in fishItems)
        {
            totalWeight += fish.probability;
        }

        float randomValue = Random.Range(0, totalWeight);

        float cumulativeWeight = 0f;
        foreach (var fish in fishItems)
        {
            cumulativeWeight += fish.probability;
            if (randomValue <= cumulativeWeight)
            {
                return fish.GetResourcesPath();
            }
        }

        Debug.LogWarning("Random selection failed. Check probabilities.");
        return string.Empty;
    }

    public List<GameItemData> GetFishList() 
    {
        return fishList;
    }
}
