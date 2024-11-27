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
            Debug.LogWarning("�����Ϸ��� �������� �����ϴ�.");
            return false;
        }

        equippedItem = item;
        SetLayerRecursively(equippedItem.gameObject, LayerMask.NameToLayer("EquipmentItem"));

        Debug.Log($"{item.itemName}��(��) {slotType} ���Կ� �����Ǿ����ϴ�.");
        return true;
    }


    public void Unequip()
    {
        if (equippedItem == null)
        {
            Debug.LogWarning("������ ��� �ֽ��ϴ�.");
            return;
        }

        Debug.Log($"{equippedItem.itemName}��(��) {slotType} ���Կ��� �����Ǿ����ϴ�.");
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
