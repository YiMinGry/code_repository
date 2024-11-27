using System;
using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public enum EquipmentType
{
    Skin,
    Emot,
    Head,
    Body,
    HandL,
    HandR,
    objectRooting,
    None
}

public enum ActionMap
{
    Player,
    Guitar,
    Fishing,
    UI,
    Water
}

public class Player : MonoBehaviour
{
    private PlayerController _controller;
    public PlayerController controller
    {
        get
        {
            return _controller;
        }
    }
    private ObjectDetector _objectDetector;
    public ObjectDetector objectDetector
    {
        get
        {
            return _objectDetector;
        }
    }

    private PlayerAniEvent _aniEvent;
    public PlayerAniEvent aniEvent
    {
        get
        {
            return _aniEvent;
        }
    }

    [Header("Player Tools")]
    private ToolHandler _toolHandler;
    public ToolHandler toolHandler
    {
        get
        {
            return _toolHandler; ;
        }
    }

    GameObject detectFx;

    bool isActionAinPlay = false;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _objectDetector = GetComponent<ObjectDetector>();
        _aniEvent = GetComponent<PlayerAniEvent>();
        _toolHandler = GetComponent<ToolHandler>();

        EventManager.Regist("InputZoom", InputZoom);
        EventManager.Regist("InputFunction", InputFunction);
        EventManager.Regist("InputInven", InputInven);
        EventManager.Regist("InputMenu", InputMenu);
        EventManager.Regist("InputTab", InputTab);
        EventManager.Regist("Action_R", Action_R);
        EventManager.Regist("Action_L", Action_L);
    }

    private void Update()
    {
        if (_objectDetector.GetNearInteraction() == null || UIHelper.Instance.IsUIActiveCount > 0 || _aniEvent.IsEventAniPlay || _toolHandler.isPlaying || Main.Instance.camManager.camState != Cams.PlayerFollow)
        {
            UIHelper.Instance.uIAction.DeActive();

            if (detectFx != null)
            {
                detectFx.SetActive(false);
            }
        }
        else
        {
            ActionType actionType = _objectDetector.GetNearInteraction().GetActionType();
            switch (actionType)
            {
                case ActionType.Fishing:

                    if (_controller.IsUnderwater)
                    {
                        UIHelper.Instance.uIAction.DeActive();

                        if (detectFx != null)
                        {
                            detectFx.SetActive(false);
                        }
                        return;
                    }

                    break;
            }

            UIHelper.Instance.uIAction.Set(_objectDetector.GetNearInteraction().GetActionType());

            if (detectFx == null)
            {
                detectFx = Instantiate(ItemDatabase.Instance.LoadFxByName("CFXR _CLICK_ ITEM"));
            }

            detectFx.transform.position = _objectDetector.GetNearInteraction().transform.position + new Vector3(0, 0.6f, 0);
            detectFx.SetActive(true);
        }
    }

    public void SetPosition(Vector3 _pos, Vector3 _rot)
    {
        _controller.enabled = false;
        transform.position = _pos;
        transform.rotation = Quaternion.Euler(_rot);
        _controller.enabled = true;
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    private void InputZoom()
    {
        if (Vector2.zero == _controller.input.move)
        {
            if (_controller.input.zoom > 0)
            {
                Main.Instance.camManager.ZoomIn();
            }
            else if (_controller.input.zoom < 0)
            {
                Main.Instance.camManager.ZoomOut();
            }
        }
    }

    private void InputFunction()
    {
        if (UIHelper.Instance.IsUIActiveCount > 0)
        {

        }
        else
        {
            if (_objectDetector.IsDetect)
            {
                if (_objectDetector.GetNearInteraction() != null)
                {
                    InteractionObject interactionObject = _objectDetector.GetNearInteraction();
                    ToolFunction(interactionObject.GetActionType(), interactionObject);
                }
            }
        }
    }


    public void ToolFunction(ActionType actionType, InteractionObject interactionObject = null)
    {
        if (_aniEvent.IsEventAniPlay)
        {
            return;
        }

        if (UIHelper.Instance.IsUIActive(UIType.UIQuickSlot))
        {
            UIHelper.Instance.CloseUI(UIType.UIQuickSlot);
        }

        switch (actionType)
        {
            case ActionType.Chop:
                break;
            case ActionType.PickUp:
                InventoryManager.Instance.GetItem(interactionObject.GetComponent<GameItem>());
                Destroy(interactionObject.gameObject);
                break;
            case ActionType.Mining:
                break;
            case ActionType.Enter:
                break;
            case ActionType.Trade:
                break;
            case ActionType.Rest:
                break;
            case ActionType.Brew:
                break;
            case ActionType.AcceptQuest:
                break;
            case ActionType.Fishing:
                if (!_controller.IsUnderwater)
                {
                    SwitchActionMap(ActionMap.Fishing);
                    _controller.animator.SetTrigger("Fishing");
                }
                break;
            case ActionType.Guitar:
                UIHelper.Instance.OpenUI(UIType.UIGuitar);

                SoundManager.Instance.Play(SoundManager.click, SoundType.Effect);
                Main.Instance.player.toolHandler.EnableTool(ToolType.guitar);
                Main.Instance.player.controller.animator.SetBool("isGuitar", true);
                Main.Instance.player.SwitchActionMap(ActionMap.Guitar);
                Main.Instance.player.GetComponent<PlayerInputManager>().CursorLock(true);
                break;
            case ActionType.Drink:
                break;
            default:
                break;
        }
    }

    private void InputInven()
    {
        if (UIHelper.Instance.IsUIActive(UIType.UIInventory))
        {
            UIHelper.Instance.CloseUI(UIType.UIInventory);
        }
        else
        {
            UIHelper.Instance.OpenUI(UIType.UIInventory);
        }
    }

    private void InputMenu()
    {
        if (UIHelper.Instance.IsUIActiveCount > 0)
        {
            UIHelper.Instance.CloseMostRecentUI();
        }
        else
        {
            UIHelper.Instance.OpenUI(UIType.UIMenu);
        }
    }

    private void InputTab()
    {
        if (UIHelper.Instance.IsUIActive(UIType.UIQuickSlot))
        {
            UIHelper.Instance.CloseUI(UIType.UIQuickSlot);
        }
        else
        {
            UIHelper.Instance.OpenUI(UIType.UIQuickSlot);
        }
    }

    private void Action_L()
    {
        if (aniEvent.IsActionAniPlay || aniEvent.IsEventAniPlay)
        {
            return;
        }

        int actionIdx = 0;

        if (EquipmentManager.Instance.FindSlot(EquipmentType.HandL).equippedItem != null)
        {
            actionIdx = (int)EquipmentManager.Instance.FindSlot(EquipmentType.HandL).equippedItem.actionType;
        }

        _controller.animator.SetInteger("Action_L", actionIdx);
        _controller.animator.SetTrigger("InputClickL");
    }

    private void Action_R()
    {
        if (aniEvent.IsActionAniPlay || aniEvent.IsEventAniPlay)
        {
            return;
        }

        int actionIdx = 0;

        if (EquipmentManager.Instance.FindSlot(EquipmentType.HandR).equippedItem != null)
        {
            actionIdx = (int)EquipmentManager.Instance.FindSlot(EquipmentType.HandR).equippedItem.actionType;
        }

        _controller.animator.SetInteger("Action_R", actionIdx);
        _controller.animator.SetTrigger("InputClickR");
    }

    public void SwitchActionMap(ActionMap actionMap)
    {
        Main.Instance.player.controller.playerInput.SwitchCurrentActionMap(actionMap.ToString());
    }

    public void SetMaterialSkin(Material mat)
    {
        var rend = transform.Find("Chibi_Cat").GetComponent<SkinnedMeshRenderer>();

        Material[] mats = rend.materials;
        mats[0] = mat;
        rend.materials = mats;
    }

    public void SetMaterialEmot(Material mat)
    {
        var rend = transform.Find("Chibi_Cat").GetComponent<SkinnedMeshRenderer>();
        Material[] mats = rend.materials;
        mats[1] = mat;
        rend.materials = mats;
    }
}
