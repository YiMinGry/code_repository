using UnityEngine;

public class PlayerAniEvent : MonoBehaviour
{
    private bool _isEventAniPlay = false;
    public bool IsEventAniPlay
    {
        get
        {
            return _isEventAniPlay;
        }
    }

    private bool _isActionAniPlay = false;
    public bool IsActionAniPlay
    {
        get
        {
            return _isActionAniPlay;
        }
    }

    public void OnEventAniPlay()
    {
        _isEventAniPlay = true;
    }
    public void OnEventAniEnd()
    {
        _isEventAniPlay = false;
    }
    public void OnActionAniPlay()
    {
        _isActionAniPlay = true;
    }
    public void OnActionAniEnd()
    {
        _isActionAniPlay = false;
    }
    public void OnFootStep()
    {
        SoundManager.Instance.Play(SoundManager.footstep + Random.Range(0, 10), SoundType.Effect);
    }

    public void OnWater()
    {
        SoundManager.Instance.Play(SoundManager.footstepWater + Random.Range(0, 4), SoundType.Effect);
        GameObject fx = Instantiate(ItemDatabase.Instance.LoadFxByName("CFXR Water Ripples (Arcs, Loop)"));

        fx.transform.position = transform.position + new Vector3(0, 0.6f, 0);
    }

    public void OnLand()
    {
        SoundManager.Instance.Play(SoundManager.jumpLand, SoundType.Effect);
    }

    public void OnFishing()
    {
        Main.Instance.player.toolHandler.EnableTool(ToolType.fishingRod);
        FishingManager.Instance.FishingStart();
    }
    public void OnFishRodPlay()
    {
        SoundManager.Instance.Play("fishing_rod", SoundType.Effect);
    }

    public void OnCastingFish()
    {
        Main.Instance.player.toolHandler.PlayTool(ToolType.fishingRod);
    }

    public void OnCatchFish()
    {
        FishingManager.Instance.CatchFish();
    }

    public void OnEndRooting()
    {
        if (EquipmentManager.Instance.FindSlot(EquipmentType.HandR).equippedItem != null)
        {
            EquipmentManager.Instance.FindSlot(EquipmentType.HandR).equippedItem.Show();
        }

        if (EquipmentManager.Instance.FindSlot(EquipmentType.HandL).equippedItem != null)
        {
            EquipmentManager.Instance.FindSlot(EquipmentType.HandL).equippedItem.Show();
        }

        if (EquipmentManager.Instance.FindSlot(EquipmentType.objectRooting).equippedItem != null)
        {
            EquipmentManager.Instance.FindSlot(EquipmentType.objectRooting).equippedItem.Hide();
            EquipmentManager.Instance.FindSlot(EquipmentType.objectRooting).Unequip();
        }
    }


    public void OnTitleCam()
    {
        Main.Instance.camManager.SetCamera(Cams.Title);
    }

    public void OnPlayerFollowCam()
    {
        Main.Instance.camManager.SetCamera(Cams.PlayerFollow);
        Main.Instance.player.SwitchActionMap(ActionMap.Player);
    }

    public void OnFrontCam()
    {
        Main.Instance.camManager.SetCamera(Cams.Front);
    }

    public void OnBackCam()
    {
        Main.Instance.camManager.SetCamera(Cams.Back);
    }
}
