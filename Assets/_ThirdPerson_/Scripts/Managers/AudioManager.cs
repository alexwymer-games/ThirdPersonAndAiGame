using System;
using UnityEngine;

/// //////////////////////////
/// Audio Manager Instructions
/// 1) Add new enum to SoundType for new sound 
/// 2) In Editor, Find ManagerParent / AudioManager Prefab 
/// 3) Under Music Sounds / SFX Sounds, add new Element
/// 4) Select the corresponding enum you created and Select Corresponing audioClip
/// 5) Save and Apply Overriding Changes to Prefab 
/// 6) In code reference audioManager.audioManagerInstance and choose play/stop music or playSFX
/// </summary>


public enum SoundType
{
    GAME_TRACK,

    RIFLE_SHOT,
    RIFLE_LOAD,
    RIFLE_SLIDEBACK,
    RIFLE_UNLOAD
}



public class AudioManager : MonoBehaviour
{
    //Singleton 
    public static AudioManager audioManagerInstance;

    //Music 
    [SerializeField] private Sound[] musicSounds;
    public AudioSource musicSource;

    //SFX
    [SerializeField] private Sound[] sfxSounds;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (audioManagerInstance == null)
        {
            audioManagerInstance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayMusic(SoundType name)
    {
        Sound s = Array.Find(musicSounds, x => x.soundType == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.audioClip;
            musicSource.Play();
        }
    }

    public void StopPlayingMusic(SoundType name)
    {
        Sound s = Array.Find(musicSounds, x => x.soundType == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.audioClip;
            musicSource.Stop();
        }
    }

    public void PlaySFX(SoundType name)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundType == name);

        if (s == null)
        {
            //Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.pitch = s.pitch;
            sfxSource.volume = s.volume;

            sfxSource.PlayOneShot(s.audioClip);
        }
    }

    public void StopPlayingSFX(SoundType name)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundType == name);

        if (s == null)
        {
            //Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.Stop();
        }
    }

    public bool ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        return musicSource.mute;
    }

    public bool ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        return sfxSource.mute;
    }

    public void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void ChangeSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    //Getters 
    public float GetMusicVolume()
    {
        return musicSource.volume;
    }

    public float GetSFXVolume()
    {
        return sfxSource.volume;
    }

}

[Serializable]
public class Sound
{
    public SoundType soundType;
    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0f, 1f)]
    public float pitch;

    public bool loop;
}

