using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    private float volume = 0.5f;
    private float tmpVolume;

    bool isPlaying = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isPlaying == false && other.gameObject.tag.Equals("Player"))
        {
            tmpVolume = SoundManager.Instance.GetAudioSource(SoundType.Bgm).volume;

            SoundManager.Instance.Clear();

            SoundManager.Instance.GetAudioSource(SoundType.Bgm).volume = volume;
            SoundManager.Instance.Play(SoundManager.subBGM);
            Main.Instance.camManager.SetCamera(Cams.Piano);

            isPlaying = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (isPlaying == true && other.gameObject.tag.Equals("Player"))
        {
            SoundManager.Instance.Clear();

            SoundManager.Instance.GetAudioSource(SoundType.Bgm).volume = tmpVolume;
            SoundManager.Instance.Play(SoundManager.mainBGM);
            Main.Instance.camManager.SetCamera(Cams.PlayerFollow);

            isPlaying = false;
        }
    }
}
