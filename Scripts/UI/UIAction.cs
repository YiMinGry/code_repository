using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAction : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI contentText;

    [SerializeField] private GameObject keyicon;
    [SerializeField] private GameObject keyText;

    public void Set(ActionType actionType)
    {
        icon.sprite = UIHelper.Instance.GetActionSprite(actionType);
        contentText.text = GetActionString(actionType);

        if (Main.Instance.player.controller.input.isGamePadConnected)
        {
            keyicon.gameObject.SetActive(true);
            keyText.gameObject.SetActive(false);
        }
        else 
        {
            keyicon.gameObject.SetActive(false);
            keyText.gameObject.SetActive(true);
        }

        _canvasGroup.alpha = 1;
    }

    public void DeActive() 
    {
        _canvasGroup.alpha = 0;
    }

    public string GetActionString(ActionType actionType)
    {
        string actionName = "확인하기";

        switch (actionType)
        {
            case ActionType.Chop:
                actionName = "베기";
                break;
            case ActionType.PickUp:
                actionName = "줍기";
                break;
            case ActionType.Mining:
                actionName = "채광하기";
                break;
            case ActionType.Enter:
                actionName = "입장하기";
                break;
            case ActionType.Trade:
                actionName = "거래하기";
                break;
            case ActionType.Rest:
                actionName = "휴식하기";
                break;
            case ActionType.Brew:
                actionName = "제조하기";
                break;
            case ActionType.AcceptQuest:
                actionName = "수락하기";
                break;
            case ActionType.Fishing:
                actionName = "낚시하기";
                break;
            case ActionType.Guitar:
                actionName = "연주하기";
                break;
            case ActionType.Drink:
                actionName = "마시기";
                break;
        }

        return actionName;
    }
}
