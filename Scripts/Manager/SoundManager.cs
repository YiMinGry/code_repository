using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Bgm,
    Effect
}

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource[] _audioSources;
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public const string mainBGM = "Black_Moon_Princess";
    public const string subBGM = "DADDY -Coldplay";
    public const string click = "click";
    public const string enter = "enter";
    public const string footstep = "Fantasy_Game_Footstep_Dirt_Medium_";
    public const string footstepWater = "Fantasy_Game_Footstep_Water_";
    public const string jumpLand = "JumpLand";


    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject("@Sound");
            Object.DontDestroyOnLoad(root);

            int soundCount = System.Enum.GetValues(typeof(SoundType)).Length;
            _audioSources = new AudioSource[soundCount];
            string[] soundNames = System.Enum.GetNames(typeof(SoundType));

            for (int i = 0; i < soundCount; i++)
            {
                GameObject go = new GameObject(soundNames[i]);
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)SoundType.Bgm].loop = true; // BGM loop
        }
    }

    public void Clear()
    {
        if (_audioSources == null) 
        {
            return;
        }

        foreach (AudioSource audioSource in _audioSources)
        {
            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.clip = null;
            }
        }
        _audioClips.Clear();
    }

    public void Play(AudioClip audioClip, SoundType type = SoundType.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
        {
            Debug.LogWarning("AudioClip is null!");
            return;
        }

        AudioSource audioSource = _audioSources[(int)type];
        audioSource.pitch = pitch;

        if (type == SoundType.Bgm)
        {
            if (audioSource.isPlaying) 
            {
                audioSource.Stop();
            }

            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Play(string path, SoundType type = SoundType.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    private AudioClip GetOrAddAudioClip(string path, SoundType type = SoundType.Effect)
    {
        if (!path.StartsWith("Sounds/")) 
        {
            path = $"Sounds/{path}";
        }

        if (_audioClips.TryGetValue(path, out AudioClip audioClip))
        {
            return audioClip;
        }

        audioClip = Resources.Load<AudioClip>(path);
        if (audioClip == null)
        {
            Debug.LogWarning($"AudioClip not found at path: {path}");
        }
        else
        {
            _audioClips[path] = audioClip;
        }

        return audioClip;
    }

    public AudioSource GetAudioSource(SoundType type) 
    {
        return _audioSources[(int)type];
    }
}