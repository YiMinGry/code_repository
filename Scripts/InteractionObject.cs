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

    WeaponShop,    // 무기 상점
    ArmorShop,     // 방어구 상점
    Blacksmith,    // 대장간
    Inn,           // 여관
    Alchemist,     // 연금술사
    Tavern,        // 선술집
    QuestBoard,    // 퀘스트 게시판
    Item,
    Guitar,
}

public enum ActionType
{
    Chop,         // 나무 베기
    PickUp,       // 줍기
    Mining,       // 채광
    Enter,        // 입장
    Trade,        // 거래
    Rest,         // 휴식
    Brew,         // 아이템 제조
    Drink,        // 마시기
    AcceptQuest,  // 퀘스트 게시판에서 퀘스트 수락
    Fishing,      // 낚시
    Guitar,
    None// 기타 또는 행동 없음
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
                return ActionType.Chop; // 나무 베기

            case InteractionType.Item:
            case InteractionType.Plant:
            case InteractionType.Berry:
            case InteractionType.Stone:
                return ActionType.PickUp; // 줍기

            case InteractionType.Boulder:
                return ActionType.Mining; // 채광

            case InteractionType.Portal:
                return ActionType.Enter; // 입장

            case InteractionType.WeaponShop:
            case InteractionType.ArmorShop:
            case InteractionType.Blacksmith:
                return ActionType.Trade; //거래

            case InteractionType.Campfire:
            case InteractionType.Inn:
                return ActionType.Rest; // 휴식

            case InteractionType.Alchemist:
                return ActionType.Brew; // 아이템 제조

            case InteractionType.Tavern:
                return ActionType.Drink; // 마시기

            case InteractionType.QuestBoard:
                return ActionType.AcceptQuest; // 퀘스트 수락

            case InteractionType.River:
                return ActionType.Fishing; // 낚시
            case InteractionType.Guitar:
                return ActionType.Guitar;
            default:
                return ActionType.None; // 기타 행동 없음
        }
    }
}
