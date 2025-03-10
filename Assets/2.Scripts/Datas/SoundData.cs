using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "SoundData", menuName = "Scriptable Object Asset/SoundData")]
public class SoundData : ScriptableObject
{
    [Header("BGM 사운드 종류")]
    [SerializedDictionary("BGM_SoundType", "Clip")]
    public SerializedDictionary<BGM_SoundType, AudioClip> BGM_SoundClip;


    [Header("BGM 사운드 종류")]
    [SerializedDictionary("SFX_SoundType", "Clip")]
    public SerializedDictionary<SFX_SoundType, AudioClip> SFX_SoundClip;

}