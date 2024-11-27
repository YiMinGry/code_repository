using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class UIQuickSlot : UIBase
{
    public void OnClickGuitar()
    {
        Main.Instance.player.ToolFunction(ActionType.Guitar);
    }

    public void OnClickSkin()
    {
        if (UIHelper.Instance.IsUIActive(UIType.UIQuickSlot))
        {
            UIHelper.Instance.CloseUI(UIType.UIQuickSlot);
        }

        UIHelper.Instance.OpenUI(UIType.UISkin);
    }
}
