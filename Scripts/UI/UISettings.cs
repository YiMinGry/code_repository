using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class UISettings : UIBase
{
    [SerializeField] private Slider bgm;
    [SerializeField] private Slider effect;
    [SerializeField] private Slider timeScale;


    private void OnEnable()
    {
        bgm.value = SoundManager.Instance.GetAudioSource(SoundType.Bgm).volume;
        effect.value = SoundManager.Instance.GetAudioSource(SoundType.Effect).volume;
        timeScale.value = Main.Instance.dayNightTimer.timeScale / 100;
    }

    private void OnDisable()
    {
        Main.Instance.SaveGame(false);
    }

    public void OnBgmValueChanged()
    {
        SoundManager.Instance.GetAudioSource(SoundType.Bgm).volume = bgm.value;
    }

    public void OnEffectValueChanged()
    {
        SoundManager.Instance.GetAudioSource(SoundType.Effect).volume = effect.value;
    }

    public void OnTimeScaleValueChanged()
    {
        Main.Instance.dayNightTimer.SetTimeScale(timeScale.value * 100);
    }
}
