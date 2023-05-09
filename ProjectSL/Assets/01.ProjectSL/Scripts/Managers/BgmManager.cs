using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BgmManager : Singleton<BgmManager>
{
   
    public List<AudioClip> bgmList;
    public AudioSource audioSource;

    public void TitleBgmPlay()
    {
        audioSource.clip = bgmList[0];
        BgmPlay();
    }

    public void EnvironMentPlay()
    {
        audioSource.clip = bgmList[1];
        BgmPlay();
    }

    public void RamapgeBgmPlay()
    {
        audioSource.clip = bgmList[2];
        BgmPlay();
    }
    
    public void SevarogBgmPlay()
    {
        audioSource.clip = bgmList[3];
        BgmPlay();
    }

    public void BgmPlay()
    {
        audioSource.Play();
    }
}
