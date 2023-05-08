using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    Animator _animator;

    AudioSource _audioSource;

    public AudioClip _footStep_R;
    public AudioClip _footStep_L;

    public List<AudioClip> _swingOHSList;
    public List<AudioClip> _swingTHSList;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    public void OnRightFootStep()
    {
        PlaySFX(_footStep_R);
        //_audioSource.PlayClipAtPoint(_footStep_R, Camera.main.transform.position);
    }
    public void OnLeftFootStep()
    {
        PlaySFX(_footStep_L);
        // _audioSource.PlayClipAtPoint(_footStep_L, Camera.main.transform.position);
    }
    public void OnSwingOHS()
    {
        AudioClip randClip = _swingOHSList[Random.Range(0, _swingOHSList.Count)];
        PlaySFX(randClip);
        //_audioSource.PlayClipAtPoint(randClip, Camera.main.transform.position);
    }
    public void OnSwingTHS()
    {
        AudioClip randClip = _swingTHSList[Random.Range(0, _swingTHSList.Count)];
        PlaySFX(randClip);
        //AudioSource.PlayClipAtPoint(randClip, Camera.main.transform.position);
    }
    public void PlaySFX(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
