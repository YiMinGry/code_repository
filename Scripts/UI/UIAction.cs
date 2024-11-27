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
        string actionName = "Ȯ���ϱ�";

        switch (actionType)
        {
            case ActionType.Chop:
                actionName = "����";
                break;
            case ActionType.PickUp:
                actionName = "�ݱ�";
                break;
            case ActionType.Mining:
                actionName = "ä���ϱ�";
                break;
            case ActionType.Enter:
                actionName = "�����ϱ�";
                break;
            case ActionType.Trade:
                actionName = "�ŷ��ϱ�";
                break;
            case ActionType.Rest:
                actionName = "�޽��ϱ�";
                break;
            case ActionType.Brew:
                actionName = "�����ϱ�";
                break;
            case ActionType.AcceptQuest:
                actionName = "�����ϱ�";
                break;
            case ActionType.Fishing:
                actionName = "�����ϱ�";
                break;
            case ActionType.Guitar:
                actionName = "�����ϱ�";
                break;
            case ActionType.Drink:
                actionName = "���ñ�";
                break;
        }

        return actionName;
    }
}
