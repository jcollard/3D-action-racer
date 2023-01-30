using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyDriver.Audio
{
    public static class SFXController
    {
        private static float _volume = 0.25f;
        public static float Volume 
        { 
            get => _volume;
            set => _volume = Mathf.Clamp(value, 0, 1);
        }
        
        public static void Play(AudioClip clip)
        {
            GameObject go = new ($"Audio Clip: {clip.name}");
            SelfDestructable selfDestructable = go.AddComponent<SelfDestructable>();
            selfDestructable.SelfDestructIn(clip.length + 0.1f);
            AudioSource audioSource = go.AddComponent<AudioSource>();
            audioSource.volume = _volume;
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}