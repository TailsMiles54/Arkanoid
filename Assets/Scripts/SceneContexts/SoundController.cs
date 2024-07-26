using MiniIT.ARKANOID;
using MiniIT.ARKANOID.Settings;
using UnityEngine;
using Zenject;
using AudioSettings = MiniIT.ARKANOID.Settings.AudioSettings;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource            audioSource;
    [SerializeField] private AudioSource            musicSource;

    private GameController                          gameController;
    
    [Inject]
    public void Construct(GameController gameController)
    {
        this.gameController = gameController;
        gameController.SoundStateChanged += SoundStateChanged;
    }

    public void PlaySoundEffect(SoundType soundType)
    {
        audioSource.mute = gameController.SoundEnabled;
        audioSource.PlayOneShot(SettingsProvider.Get<AudioSettings>().GetSoundEffect(soundType));
    }

    public void PlayMusic(MusicType musicType)
    {
        musicSource.Stop();
        musicSource.clip = SettingsProvider.Get<AudioSettings>().GetMusic(musicType);
        musicSource.mute = gameController.SoundEnabled;
        musicSource.Play();
    }

    private void SoundStateChanged()
    {
        audioSource.mute = gameController.SoundEnabled;
        musicSource.mute = gameController.SoundEnabled;
    }

    ~SoundController()
    {
        gameController.SoundStateChanged -= SoundStateChanged;
    }
}