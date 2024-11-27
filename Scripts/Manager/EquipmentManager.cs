using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class EquipmentManager : Singleton<EquipmentManager>
{
    public List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();

    private void Start()
    {
        EquipmentSlot[] dataList = Main.Instance.player.GetComponentsInChildren<EquipmentSlot>();

        foreach (var slot in dataList)
        {
            equipmentSlots.Add(slot);
        }
    }

    public EquipmentSlot FindSlot(EquipmentType type)
    {
        foreach (var slot in equipmentSlots)
        {
            if (slot.slotType == type)
            {
                return slot;
            }
        }

        Debug.LogWarning($"타입이 {type}인 슬롯을 찾을 수 없습니다.");
        return null;
    }

    public EquipmentSlot EquipItem(EquipmentType type, string itemName)
    {
        var slot = FindSlot(type);
        if (slot != null)
        {

            GameItem item = ItemDatabase.Instance.InstantiateItem(itemName, slot.transform);
            slot.Equip(item);
        }

        return slot;
    }

    public void UnequipItem(EquipmentType type)
    {
        var slot = FindSlot(type);
        if (slot != null)
        {
            slot.Unequip();
        }
    }

    public EquipmentData SaveData()
    {
        EquipmentData saveData = new EquipmentData
        {
            dataList = new List<EquipmentSlotData>()
        };

        foreach (var slot in equipmentSlots)
        {
            if (slot.slotType == EquipmentType.objectRooting)
            {
                continue;
            }

            saveData.dataList.Add(new EquipmentSlotData
            {
                slotType = slot.slotType,
                item = slot.equippedItem != null
                    ? new GameItemData
                    {
                        itemName = slot.equippedItem.itemName,
                        itemType = slot.equippedItem.itemType,
                    }
                    : null
            });
        }

        return saveData;
    }

    public void LoadFromFile(EquipmentData saveData)
    {
        foreach (var data in saveData.dataList)
        {


            if (data.item != null)
            {
                if (string.IsNullOrEmpty(data.item.itemName))
                {
                    continue;
                }

                if (data.slotType == EquipmentType.Skin)
                {
                    Material material = ItemDatabase.Instance.GetMaterial(data.item.itemName);
                    Main.Instance.player.SetMaterialSkin(material);
                }
                else if (data.slotType == EquipmentType.Emot)
                {
                    Material material = ItemDatabase.Instance.GetMaterial(data.item.itemName);
                    Main.Instance.player.SetMaterialEmot(material);
                }

                EquipItem(data.slotType, "Skin/" + data.item.itemName);
            }
            else
            {
                UnequipItem(data.slotType);
            }

        }
    }
}
