using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MiniIT.ARKANOID.Settings
{
    [CreateAssetMenu(menuName = "Arkanoid/AudioSettings", fileName = "AudioSettings", order = 0)]
    public class AudioSettings : ScriptableObject
    {
        [SerializeField] private List<AudioClipSetting>             soundEffects;
        [SerializeField] private List<MusicSetting>                 music;

        public AudioClip GetSoundEffect(SoundType soundType)
        {
           return soundEffects.First(x => x.SoundType == soundType).AudioClip;
        }

        public AudioClip GetMusic(MusicType musicType)
        {
            return music.First(x => x.SoundType == musicType).AudioClip;
        }
    }

    [Serializable]
    public struct AudioClipSetting
    {
        public SoundType            SoundType;
        public AudioClip            AudioClip;
    }

    public enum SoundType
    {
        BallStart,
        BrickHit,
        ButtonClick,
        MenuSwipe,
    }

    [Serializable]
    public struct MusicSetting
    {
        public MusicType            SoundType;
        public AudioClip            AudioClip;
    }

    public enum MusicType
    {
        Menu,
        InGame
    }
}