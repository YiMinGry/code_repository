using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Fish,
    Foods,
    Nature,
    Props,
    Tools,
    Weapons
}

public enum ItemActionType 
{ 
    None = 0,

    Action_R_2Hand = 1,
    Action_R_1Hand = 2,

    Action_L_1HandBlock = 101,
    Action_L_Bow = 102,
    Action_L_Throw = 103,
    Action_L_CastSpell = 104,
    Action_L_Drinking = 105
}


public class GameItem : MonoBehaviour
{
    public ItemType itemType;
    public EquipmentType equipmentType = EquipmentType.None;
    public ItemActionType actionType = ItemActionType.None;
    public string itemName; // 아이템 이름
    public Sprite icon;     // 아이템 아이콘
    public float probability;

    public string GetResourcesPath()
    {
        return itemType + "/" + itemName;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
