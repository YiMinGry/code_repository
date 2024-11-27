using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public CharacterData characterData;
    public SettingsData settingsData;
    public EquipmentData equipmentData;
    public InventoryData inventoryData;
    public FishingData fishingData;
    public MiniGameData miniGameData;
}

[System.Serializable]
public class CharacterData
{
    public Vector3 playerPosition;
    public Vector3 playerRotation;
}

[System.Serializable]
public class GameItemData
{
    public string itemName;
    public ItemType itemType;
    public int itemCount;

    public string GetResourcesPath()
    {
        return itemType + "/" + itemName;
    }
}

[System.Serializable]
public class EquipmentSlotData
{
    public EquipmentType slotType;
    public GameItemData item;
}

[System.Serializable]
public class EquipmentData
{
    public List<EquipmentSlotData> dataList = new List<EquipmentSlotData>();
}

[System.Serializable]
public class InventoryData
{
    public List<GameItemData> dataList = new List<GameItemData>();
}

[System.Serializable]
public class FishingData
{
    public List<GameItemData> dataList = new List<GameItemData>();
}

[System.Serializable]
public class MiniGameData
{
    public Dictionary<string, int> gameRecords = new Dictionary<string, int>();
}

[System.Serializable]
public class SettingsData
{
    public float bgmVolume;
    public float effectVolume;
    public float timeScale;
}