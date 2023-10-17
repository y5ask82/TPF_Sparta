using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSounds;
    [SerializeField] private AudioSource sfxSounds;
    [SerializeField] private AudioSource sfxSoundsVariable1;
    [SerializeField] private AudioSource sfxSoundsVariable2;
    [SerializeField] private AudioSource sfxSoundsVariable3;
    [SerializeField] private AudioSource sfxSoundsVariable4;

    private float bgmVolume;
    private float sfxVolume;

    private const string strBGMVolume = "BGMVolume";
    private const string strSFXVolume = "SFXVolume";

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (PlayerPrefs.HasKey(strBGMVolume))
        {
            bgmVolume = PlayerPrefs.GetFloat(strBGMVolume);
        }
        else
        {
            PlayerPrefs.SetFloat(strBGMVolume, 1);
        }

        if (PlayerPrefs.HasKey(strSFXVolume))
        {
            sfxVolume = PlayerPrefs.GetFloat(strSFXVolume);
        }
        else
        {
            PlayerPrefs.SetFloat(strSFXVolume, 1);
        }
        PlayerPrefs.Save();
    }

    public void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat(strBGMVolume, volume);
        bgmVolume = volume;
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(strSFXVolume, volume);
        sfxVolume = volume;
        PlayerPrefs.Save();
    }

    public float GetBGMVolume()
    {
        return bgmVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSounds.Stop();
        bgmSounds.clip = clip;
        bgmSounds.volume = bgmVolume;
        bgmSounds.Play();
    }

    public void StopBGM()
    {
        bgmSounds.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSounds.clip = clip;
        sfxSounds.volume = sfxVolume;
        sfxSounds.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSounds.clip = clip;
        sfxSounds.volume = sfxVolume * volume;
        sfxSounds.PlayOneShot(clip);
    }

    public void PlaySFXVariable1(AudioClip clip, float volume)
    {
        sfxSoundsVariable1.clip = clip;
        sfxSoundsVariable1.volume = sfxVolume * volume;
        sfxSoundsVariable1.PlayOneShot(clip);
    }

    public void PlaySFXVariable2(AudioClip clip, float volume)
    {
        sfxSoundsVariable2.clip = clip;
        sfxSoundsVariable2.volume = sfxVolume * volume;
        sfxSoundsVariable2.PlayOneShot(clip);
    }

    public void PlaySFXVariable3(AudioClip clip, float volume)
    {
        sfxSoundsVariable3.clip = clip;
        sfxSoundsVariable3.volume = sfxVolume * volume;
        sfxSoundsVariable3.PlayOneShot(clip);
    }

    public void PlaySFXVariable4(AudioClip clip, float volume)
    {
        sfxSoundsVariable4.clip = clip;
        sfxSoundsVariable4.volume = sfxVolume * volume;
        sfxSoundsVariable4.PlayOneShot(clip);
    }
}
