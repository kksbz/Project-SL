using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ISFX_Object : GData.IInitialize
{
    void SetAudioClip(AudioClip audioClip);
    void SFX_Play();
    void SFX_Play(AudioClip audioClip);
    void SFX_Play(AudioClip audioClip, bool isOneShot);
    void SFX_Play_Loop(AudioClip audioClip);
    void SFX_Stop();
    AudioClip FindAudioClip(string audioClipName);
}

public class SFX_Object : MonoBehaviour, ISFX_Object
{
    [SerializeField]
    private AudioSource _audioSource;
    public AudioSource AudioSource { get { return _audioSource; } private set { _audioSource = value; } }

    [Header("SFX목록")]
    public List<SFX> sfxList;

    [Serializable]
    public class SFX
    {
        public string SFX_Name;
        public AudioClip SFX_AudioClip;
    }

    public void Init()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void SetAudioClip(AudioClip audioClip)
    {
        AudioSource.clip = audioClip;
    }

    public void SFX_Play()
    {
        AudioSource.Play();
    }

    public void SFX_Play(AudioClip audioClip)
    {
        AudioSource.clip = audioClip;
        AudioSource.Play();
    }

    public void SFX_Play(AudioClip audioClip, bool isOneShot)
    {
        if (isOneShot)
        {
            AudioSource.loop = false;
            AudioSource.PlayOneShot(audioClip);
        }
        else
        {
            AudioSource.clip = audioClip;
            AudioSource.Play();
        }
    }

    public void SFX_Play_Loop(AudioClip audioClip)
    {
        AudioSource.loop = true;
        AudioSource.clip = audioClip;
        AudioSource.Play();
    }

    public void SFX_Stop()
    {
        AudioSource.loop = false;
        AudioSource.Stop();
    }

    public AudioClip FindAudioClip(string audioClipName)
    {
        return sfxList.Find(element => element.SFX_Name == audioClipName).SFX_AudioClip;
    }
}
