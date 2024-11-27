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

public class GameItem : MonoBehaviour
{
    public ItemType itemType;

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
