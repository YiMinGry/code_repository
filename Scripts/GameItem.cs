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

    public string itemName; // ������ �̸�
    public Sprite icon;     // ������ ������
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
