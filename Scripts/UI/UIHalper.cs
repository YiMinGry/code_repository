using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    UITitle,
    UIAlert1,
    UIAlert2,
    UIInventory,
    UISettings,
    UIFishingDex,
    UIShop,
    UIMenu,
    UIQuickSlot,
    UIGuitar,
    UISkin,
    UIVideoPlayer
}
public class UIHelper : Singleton<UIHelper>
{
    private List<UIBase> activeUIPopups = new List<UIBase>();
    private Dictionary<UIType, UIBase> activeUIPopupDict = new Dictionary<UIType, UIBase>();

    Canvas mainCanvas;

    public UILog uILog;
    public UIAction uIAction;
    public UIAutoSave uIAutoSave;

    public void InitFrontUI()
    {
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();

        if (uILog == null)
        {
            GameObject uILogPrefab = Resources.Load<GameObject>($"UI/UILog");
            uILog = Instantiate(uILogPrefab, mainCanvas.transform).GetComponent<UILog>();
        }

        if (uIAction == null)
        {
            GameObject uIActionPrefab = Resources.Load<GameObject>($"UI/UIAction");
            uIAction = Instantiate(uIActionPrefab, mainCanvas.transform).GetComponent<UIAction>();
        }

        if (uIAutoSave == null)
        {
            GameObject uIAutoSavePrefab = Resources.Load<GameObject>($"UI/UIAutoSave");
            uIAutoSave = Instantiate(uIAutoSavePrefab, mainCanvas.transform).GetComponent<UIAutoSave>();
        }
    }

    public UIBase OpenUI(UIType uiType)
    {
        if (activeUIPopupDict.ContainsKey(uiType))
        {
            Debug.LogWarning($"{uiType} UI is already active.");
            return activeUIPopupDict[uiType];
        }

        GameObject uiPrefab = Resources.Load<GameObject>($"UI/{uiType}");
        if (uiPrefab == null)
        {
            Debug.LogError($"UI prefab for {uiType} not found in Resources/UI/");
            return null;
        }

        Main.Instance.player.GetComponent<PlayerInputManager>().CursorLock(false);

        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();

        UIBase uiInstance = Instantiate(uiPrefab, mainCanvas.transform).GetComponent<UIBase>();
        activeUIPopups.Add(uiInstance);
        activeUIPopupDict[uiType] = uiInstance;

        uiInstance.Open();

        if (uiType != UIType.UITitle)
        {
            SoundManager.Instance.Play(SoundManager.click, SoundType.Effect);
        }


        Main.Instance.player.SwitchActionMap(ActionMap.UI);

        Main.Instance.player.controller.input.look = Vector2.zero;

        return uiInstance;
    }

    public void CloseUI(UIType uiType)
    {
        if (activeUIPopupDict.TryGetValue(uiType, out UIBase uiInstance))
        {
            if (uiType != UIType.UITitle)
            {
                SoundManager.Instance.Play(SoundManager.click, SoundType.Effect);
            }

            uiInstance.Close();
            activeUIPopups.Remove(uiInstance);
            activeUIPopupDict.Remove(uiType);
        }
        else
        {
            Debug.LogWarning($"{uiType} UI is not currently active.");
        }

        if (IsUIActiveCount <= 0)
        {
            Main.Instance.player.SwitchActionMap(ActionMap.Player);
            Main.Instance.player.GetComponent<PlayerInputManager>().CursorLock(true);
        }
    }

    public void CloseMostRecentUI()
    {
        if (activeUIPopups.Count > 0)
        {
            UIBase uiInstance = activeUIPopups[activeUIPopups.Count - 1];
            uiInstance.Close();
            activeUIPopups.RemoveAt(activeUIPopups.Count - 1);

            foreach (var keyValue in activeUIPopupDict)
            {
                if (keyValue.Value == uiInstance)
                {
                    activeUIPopupDict.Remove(keyValue.Key);
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("No UI is currently active.");
        }

        if (IsUIActiveCount <= 0)
        {
            Main.Instance.player.SwitchActionMap(ActionMap.Player);
            Main.Instance.player.GetComponent<PlayerInputManager>().CursorLock(true);
        }
    }

    public GameObject GetMostRecentUI()
    {
        if (activeUIPopups.Count > 0)
        {
            UIBase uiInstance = activeUIPopups[activeUIPopups.Count - 1];
            return uiInstance.gameObject;
        }
        else
        {
            Debug.LogWarning("No UI is currently active.");
            return null;
        }
    }

    public void ConfirmMostRecentUI()
    {
        if (activeUIPopups.Count > 0)
        {
            UIBase uiInstance = activeUIPopups[activeUIPopups.Count - 1];
            uiInstance.InvokeConfirm();
        }
        else
        {
            Debug.LogWarning("No UI is currently active.");
        }

        if (IsUIActiveCount <= 0)
        {
            Main.Instance.player.SwitchActionMap(ActionMap.Player);
            Main.Instance.player.GetComponent<PlayerInputManager>().CursorLock(true);
        }
    }

    public void CancelMostRecentUI()
    {
        if (activeUIPopups.Count > 0)
        {
            UIBase uiInstance = activeUIPopups[activeUIPopups.Count - 1];

            uiInstance.InvokeCancel();
        }
        else
        {
            Debug.LogWarning("No UI is currently active.");
        }

        if (IsUIActiveCount <= 0)
        {
            Main.Instance.player.SwitchActionMap(ActionMap.Player);
            Main.Instance.player.GetComponent<PlayerInputManager>().CursorLock(true);
        }
    }

    public bool IsUIActive(UIType uiType)
    {
        return activeUIPopupDict.ContainsKey(uiType);
    }

    public int IsUIActiveCount
    {
        get
        {
            return activeUIPopupDict.Count;
        }
    }

    public Sprite GetSprite(string path)
    {
        if (!path.StartsWith("Sprites/"))
        {
            path = $"Sprites/{path}";
        }

        Sprite sprite = Resources.Load<Sprite>(path);

        if (sprite == null)
        {
            Debug.Log(path + " NotFound");
            sprite = Resources.Load<Sprite>("Sprites/PNG/NotFound");
        }

        return sprite;
    }

    public Sprite GetActionSprite(ActionType actionType)
    {
        string actionName = "Hand";

        switch (actionType)
        {
            case ActionType.Chop:
                actionName = "Axe";
                break;
            case ActionType.PickUp:
                actionName = "Arrow Right (Curved)";
                break;
            case ActionType.Mining:
                actionName = "Scythe Big";
                break;
            case ActionType.Enter:
                actionName = "Enter";
                break;
            case ActionType.Trade:
                actionName = "Coins Bag";
                break;
            case ActionType.Rest:
                actionName = "Heart - Health";
                break;
            case ActionType.Brew:
                actionName = "Potion - Half";
                break;
            case ActionType.AcceptQuest:
                actionName = "Chat x2";
                break;
            case ActionType.Fishing:
                actionName = "Fishing";
                break;
            case ActionType.Guitar:
                actionName = "Music";
                break;
            case ActionType.Drink:
                actionName = "Cutlery";
                break;
        }

        return GetSprite($"Icons/PNG/{actionName}");
    }
    public Sprite GetIcon(string itemName)
    {
        return GetSprite($"Icons/PNG/{itemName}");
    }

    public Sprite GetItemIcon(string itemName)
    {
        return GetSprite($"Icons/Item/{itemName}");
    }
}