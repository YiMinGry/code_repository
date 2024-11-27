using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.Examples.ObjectSpin;

public enum InteractionType
{
    Tree,
    River,
    Plant,
    Berry,
    Stone,
    Boulder,
    Animal,
    Campfire,
    Portal,

    WeaponShop,    // ���� ����
    ArmorShop,     // �� ����
    Blacksmith,    // ���尣
    Inn,           // ����
    Alchemist,     // ���ݼ���
    Tavern,        // ������
    QuestBoard,    // ����Ʈ �Խ���
    Item,
    Guitar,
}

public enum ActionType
{
    Chop,         // ���� ����
    PickUp,       // �ݱ�
    Mining,       // ä��
    Enter,        // ����
    Trade,        // �ŷ�
    Rest,         // �޽�
    Brew,         // ������ ����
    Drink,        // ���ñ�
    AcceptQuest,  // ����Ʈ �Խ��ǿ��� ����Ʈ ����
    Fishing,      // ����
    Guitar,
    None// ��Ÿ �Ǵ� �ൿ ����
}

public class InteractionObject : MonoBehaviour
{
    public InteractionType interactionType;
    private ActionType actionType;

    private void Awake()
    {
        actionType = GetActionType();
    }

    public ActionType GetActionType()
    {
        switch (interactionType)
        {
            case InteractionType.Tree:
                return ActionType.Chop; // ���� ����

            case InteractionType.Item:
            case InteractionType.Plant:
            case InteractionType.Berry:
            case InteractionType.Stone:
                return ActionType.PickUp; // �ݱ�

            case InteractionType.Boulder:
                return ActionType.Mining; // ä��

            case InteractionType.Portal:
                return ActionType.Enter; // ����

            case InteractionType.WeaponShop:
            case InteractionType.ArmorShop:
            case InteractionType.Blacksmith:
                return ActionType.Trade; //�ŷ�

            case InteractionType.Campfire:
            case InteractionType.Inn:
                return ActionType.Rest; // �޽�

            case InteractionType.Alchemist:
                return ActionType.Brew; // ������ ����

            case InteractionType.Tavern:
                return ActionType.Drink; // ���ñ�

            case InteractionType.QuestBoard:
                return ActionType.AcceptQuest; // ����Ʈ ����

            case InteractionType.River:
                return ActionType.Fishing; // ����
            case InteractionType.Guitar:
                return ActionType.Guitar;
            default:
                return ActionType.None; // ��Ÿ �ൿ ����
        }
    }
}
