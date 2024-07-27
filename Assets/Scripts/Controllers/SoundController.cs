using MiniIT.ARKANOID.Enums;
using MiniIT.ARKANOID.Save;
using UnityEngine;
using Zenject;
using AudioSettings = MiniIT.ARKANOID.Settings.AudioSettings;

namespace MiniIT.ARKANOID.Controllers
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource            audioSource;
        [SerializeField] private AudioSource            musicSource;

        private ISaveManager                            saveManager;
    
        [Inject]
        public void Construct(ISaveManager saveManager)
        {
            this.saveManager = saveManager;
        }

        public async void PlaySoundEffect(SoundType soundType)
        {
            audioSource.mute = !await saveManager.GetSoundState();
            audioSource.PlayOneShot(SettingsProvider.Get<AudioSettings>().GetSoundEffect(soundType));
        }

        public async void PlayMusic(MusicType musicType)
        {
            musicSource.Stop();
            musicSource.clip = SettingsProvider.Get<AudioSettings>().GetMusic(musicType);
            musicSource.mute = !await saveManager.GetSoundState();
            musicSource.Play();
        }

        public async void SoundStateChange()
        {
            bool currentSoundState = await saveManager.GetSoundState();

            await saveManager.SaveSoundState(!currentSoundState);
            
            audioSource.mute = !await saveManager.GetSoundState();
            musicSource.mute = !await saveManager.GetSoundState();
        }
    }
}