using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Source")]
    [Space]
    public AudioSource audioSource;

    [Header("SFX")]
    [Space]
    public SoundAssets[] sounds;

    public enum Sound
    {
        BUTTONCLICK,
        BUTTONCLICKPOS,
        BUTTONCLICKNEG,
        COUNTDOWN,
        LASTCOUNTDOWN
    }

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    public void Play(Sound soundType)
    {
        audioSource.PlayOneShot(sounds[(int)soundType].sfx);
    }
}

/// <summary>
/// Used as a container to hold
/// the sfx and the sound type
/// </summary>
[System.Serializable]
public class SoundAssets 
{
    public SoundManager.Sound sfxType;
    public AudioClip sfx;
}
