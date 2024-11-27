using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFishingDex : UIBase
{
    [SerializeField] GameObject fishingDexItem;
    [SerializeField] Transform content;

    private void OnEnable()
    {
        foreach (GameItemData data in FishingManager.Instance.GetFishList()) 
        {
            UIItemBase item = Instantiate(fishingDexItem, content).GetComponent<UIItemBase>();
            item.Set(data);
        }
    }
}
