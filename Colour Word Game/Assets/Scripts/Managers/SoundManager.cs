using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The SoundManager contains all sound
/// files and plays the specified sound
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public enum Sound
    {
        BUTTONCLICK,
        BUTTONCLICKPOS,
        BUTTONCLICKNEG,
        COUNTDOWN,
        LASTCOUNTDOWN
    }

    [Header("Source")]
    [Space]
    public AudioSource audioSource;

    [Header("SFX")]
    [Space]
    public SoundAssets[] sounds;

    private Dictionary<Sound, AudioClip> soundClips;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        soundClips = new Dictionary<Sound, AudioClip>();

        // Fill the dictionary with the array 
        for(int i = 0; i < sounds.Length; i++)
        {
            soundClips.Add(sounds[i].sfxType, sounds[i].sfx);
        }
    }

    public void Play(Sound soundType)
    {
        audioSource.PlayOneShot(soundClips[soundType]);
    }
}

/// <summary>
/// A class that acts a container by
/// exposing the members in the inspector.
/// </summary>
[System.Serializable]
public class SoundAssets 
{
    public SoundManager.Sound sfxType;
    public AudioClip sfx;
}