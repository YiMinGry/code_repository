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
}
