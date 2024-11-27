using UnityEngine;

public class UIMenu : UIBase
{
    public void OnClickInven()
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

    public void OnClickFishDex()
    {
        if (UIHelper.Instance.IsUIActive(UIType.UIFishingDex))
        {
            UIHelper.Instance.CloseUI(UIType.UIFishingDex);
        }
        else
        {
            UIHelper.Instance.OpenUI(UIType.UIFishingDex);
        }
    }

    public void OnClickSetting()
    {
        if (UIHelper.Instance.IsUIActive(UIType.UISettings))
        {
            UIHelper.Instance.CloseUI(UIType.UISettings);
        }
        else
        {
            UIHelper.Instance.OpenUI(UIType.UISettings);
        }
    }

    public void OnClickQuit()
    {
        SoundManager.Instance.Play(SoundManager.click, SoundType.Effect);

        Main.Instance.SaveGame();

        Application.Quit();
    }
}
