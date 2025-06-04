using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource musicSource;
    private AudioSource sfxSource;
    private void Awake()
    {
        instance = this;
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;

        sfxSource = gameObject.AddComponent<AudioSource>();
    }
    private void Start()
    {
        
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.sfxSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
    public static void PlaySoundLoop(SoundType sound, float volume = 1)
    {
        instance.musicSource.clip = instance.soundList[(int)sound];
        instance.musicSource.volume = volume;
        instance.musicSource.Play();
    }
    public static void StopLoopingSound()
    {
        instance.musicSource.Stop();
        instance.musicSource.loop = false;
    }
}

public enum SoundType
{
    GAME_BACKGROUND,
    SELECT,
    HIT,
    WIN,
    LOSE,
    MENU_BACKGROUND,
    LEVEL_UP,
    XP
      
}
