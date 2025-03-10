using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _bgm_Audio;
    [SerializeField] private List<AudioSource> _sfx_Audio;

    [SerializeField] private SoundData _soundData;
    [SerializeField] private bool _isBGMVolume = true;
    [SerializeField] private bool _isSFXVolume = true;

    protected override void Awake()
    {
        base.Awake();

        _soundData = Resources.Load<SoundData>("SoundData");
    }
    
    //--BGM--
    public void Play_BGM(BGM_SoundType soundType, float volume)
    {
        if(!_isBGMVolume)
            return;

        _bgm_Audio.clip = _soundData.BGM_SoundClip[soundType];
        _bgm_Audio.volume = volume;
        _bgm_Audio.Play();
    }

    public void Stop_BGM()
    {
        _bgm_Audio.Stop();
    }

    public void Pause_BGM()
    {
        _bgm_Audio.Pause();
    }
    
    //--SFX--
    public void Play_SFX(SFX_SoundType soundType, float volume)
    {
        if(!_isSFXVolume)
            return;
        AudioSource emptySource = _sfx_Audio.Find(source => !source.isPlaying);

        if(emptySource != null)
        {
            emptySource.volume = volume;
            emptySource.PlayOneShot(_soundData.SFX_SoundClip[soundType]);
        }
        else
        {
            _sfx_Audio[0].PlayOneShot(_soundData.SFX_SoundClip[soundType]);
        }
    }

    public void DisableBGM_Volume()
    {
        _bgm_Audio.volume = 0;
        _isBGMVolume = false;
    }
    public void EnableBGM_Volume()
    {
        _bgm_Audio.volume = 0.55f;
        _isBGMVolume = true;
    }
    public void DisableSFX_Volume()
    {
        _isSFXVolume = false;
    }
    public void EnableSFX_Volume()
    {
        _isSFXVolume = true;
    }


    public bool GetIsBGMVolume()
    {
        return _isBGMVolume;
    }
    public bool GetIsSFXVolume()
    {
        return _isSFXVolume;
    }
}