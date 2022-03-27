using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicManager : Singleton<BGMusicManager>
{
    [Header("Audio Source")]
    public AudioSource mainmenuSource;
    public AudioSource gameplaySource;

    [Header("Audio Clip")]
    public AudioClip mainmenuClip;
    public AudioClip gameplayClip;
    public AudioClip gameplayHurryClip;

    public void PlaySound(AudioSource source, AudioClip clip)
    {
        if (source.clip != clip)
        {
            if (clip != null)
            {
                source.clip = clip;
                source.Play();
            }
        }
    }
    public void PlaySound(AudioSource source, AudioClip[] clip)
    {
        int random = Random.Range(0, clip.Length);

        source.clip = clip[random];
        source.Play();
    }
    public void PlaySoundOneShot(AudioSource source, AudioClip clip)
    {
        if (source.clip != clip)
        {
            if (clip != null)
            {
                source.PlayOneShot(clip);
            }
        }
    }
    public void PlaySoundOneShot(AudioSource source, AudioClip[] clip)
    {
        int random = Random.Range(0, clip.Length);

        source.PlayOneShot(clip[random]);
    }


    public void RemoveSound(AudioSource source)
    {
        source.clip = null;
    }

    public void RemoveAllSound()
    {
        mainmenuSource.clip = null;
        gameplaySource.clip = null;
    }
}
