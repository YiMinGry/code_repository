using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    public EquipmentType slotType;
    public GameItem equippedItem;

    public bool Equip(GameItem item)
    {
        if (item == null)
        {
            Debug.LogWarning("장착하려는 아이템이 없습니다.");
            return false;
        }

        equippedItem = item;
        SetLayerRecursively(equippedItem.gameObject, LayerMask.NameToLayer("EquipmentItem"));

        Debug.Log($"{item.itemName}이(가) {slotType} 슬롯에 장착되었습니다.");
        return true;
    }


    public void Unequip()
    {
        if (equippedItem == null)
        {
            Debug.LogWarning("슬롯이 비어 있습니다.");
            return;
        }

        Debug.Log($"{equippedItem.itemName}이(가) {slotType} 슬롯에서 해제되었습니다.");
        Destroy(equippedItem.gameObject);
        equippedItem = null;
    }

    public void ChangeItem(GameItem item)
    {
        Unequip();
        Equip(item);
    }


    void SetLayerRecursively(GameObject obj, int layerIndex)
    {
        obj.layer = layerIndex;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layerIndex);
        }
    }
}
