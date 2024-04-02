using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SoundConfig<T> : ScriptableObject where T : Enum
{
    public Dictionary<T, SoundEffect[]> SoundEffects;
}
[Serializable]
public class SoundEffect
{
    public SoundEffectType Type;
    public bool ClipVariations;
    public bool VolumeVariations;
    public bool HighPriority;
    [HideIf(nameof(ClipVariations))]public AudioClip Clip;
    [ShowIf(nameof(ClipVariations))]public AudioClip[] Clips;
    [ShowIf(nameof(VolumeVariations)),MinMaxSlider(0,1)]public Vector2 VolumeValues;
    private SoundEffectCategory _category;

    public AudioClip ProvideRandomClip()
    {
        if (!ClipVariations) return null;
        //RandomDeck<AudioClip> randomDeck = new RandomDeck<AudioClip>(Clips);
        //return randomDeck.Provide();
        return null; //temp
    }
}
public enum SoundEffectCategory
{
    
}

public enum SoundEffectType
{
    
}
