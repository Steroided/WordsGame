using UnityEngine;
using System.Collections;
using Zenject;

public class SoundManagerButtonsProvider
{
    [Inject]
    private SoundManager _soundManager;
    public void PlaySound(string name)
    {
        _soundManager.PlaySound(name);
    }

    public void PlaySoundNotPausable(string name)
    {
        _soundManager.PlaySoundUI(name);
    }

    public void ChangeSoundVolume(float volume)
    {
        _soundManager.SetSoundVolume(volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        _soundManager.SetMusicVolume(volume);
    }

    public void ToggleMusicMuted()
    {
        _soundManager.SetMusicMuted(!_soundManager.GetMusicMuted());
    }

    public void ToggleSoundMuted()
    {
        _soundManager.SetSoundMuted(!_soundManager.GetSoundMuted());
    }
}
