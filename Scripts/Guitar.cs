using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem.XR;
using Unity.VisualScripting;

public class Guitar : ToolEventBase
{
    public AudioClip[] guitarClips;
    public AudioClip[] noiseClips;

    private AudioClip curClips;

    private void OnEnable()
    {
        if (guitarClips.Length > 0)
        {
            curClips = guitarClips[0];
        }

        isPlaying = true;
    }
    private void Awake()
    {
        EventManager.Regist("OnGuitarC", () => { PlayNote(0); });
        EventManager.Regist("OnGuitarD", () => { PlayNote(1); });
        EventManager.Regist("OnGuitarE", () => { PlayNote(2); });
        EventManager.Regist("OnGuitarF", () => { PlayNote(3); });
        EventManager.Regist("OnGuitarG", () => { PlayNote(4); });
        EventManager.Regist("OnGuitarA", () => { PlayNote(5); });
        EventManager.Regist("OnGuitarB", () => { PlayNote(6); });
        EventManager.Regist("OnGuitarStop", Stop);
    }

    private void PlayNote(int noteIndex)
    {
        if (noteIndex < 0 || noteIndex >= guitarClips.Length)
        {
            Debug.LogWarning("Invalid note index.");
            return;
        }

        if (curClips != guitarClips[noteIndex])
        {
            SoundManager.Instance.Play(noiseClips[Random.Range(0, noiseClips.Length)], SoundType.Effect);
        }

        EventManager.Invoke("UIGuitarPlay" + noteIndex);

        curClips = guitarClips[noteIndex];
        SoundManager.Instance.Play(curClips, SoundType.Effect);
        Main.Instance.player.controller.animator.SetTrigger("Guitar_Playing");
    }

    public void Stop()
    {
        SoundManager.Instance.Play(SoundManager.click, SoundType.Effect);
        Main.Instance.player.controller.animator.SetBool("isGuitar", false);

        UIHelper.Instance.CloseUI(UIType.UIGuitar);

        isPlaying = false;
    }
}
