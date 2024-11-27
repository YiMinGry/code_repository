using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILog : MonoBehaviour
{
    [SerializeField] private GameObject logItem;
    [SerializeField] private Transform content;

    public void Add(ActionType actionType, string msg, bool isAlert = false)
    {
        UILogItem item = Instantiate(logItem, content).GetComponent<UILogItem>();
        Sprite sprite = UIHelper.Instance.GetActionSprite(actionType);
        item.Set(sprite, msg, isAlert);
    }

    public void Add(Sprite sprite, string msg, bool isAlert = false)
    {
        UILogItem item = Instantiate(logItem, content).GetComponent<UILogItem>();
        item.Set(sprite, msg, isAlert);
    }

    public void Add(string itemName, string msg, bool isAlert = false)
    {
        UILogItem item = Instantiate(logItem, content).GetComponent<UILogItem>();
        Sprite sprite = UIHelper.Instance.GetSprite(itemName);
        item.Set(sprite, msg, isAlert);
    }
}
