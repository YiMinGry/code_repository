using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemBase : MonoBehaviour
{
    public ItemType ItemType;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private TextMeshProUGUI countText;

    public void Set(GameItemData gameItemData)
    {
        icon.sprite = ItemDatabase.Instance.LoadItemByData(gameItemData.GetResourcesPath()).icon;
        contentText.text = ItemDatabase.Instance.LoadItemByData(gameItemData.GetResourcesPath()).itemName;
        countText.text = "x" + gameItemData.itemCount;
        gameObject.SetActive(true);
    }
}
