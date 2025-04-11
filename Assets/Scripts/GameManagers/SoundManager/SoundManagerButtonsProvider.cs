using UnityEngine;
using System.Collections;

public static class SoundManagerButtonsProvider
{
    public static void PlaySound(string name)
    {
        SoundManager.PlaySound(name);
    }

    public static void PlaySoundNotPausable(string name)
    {
        SoundManager.PlaySoundUI(name);
    }

    public static void ChangeSoundVolume(float volume)
    {
        SoundManager.SetSoundVolume(volume);
    }

    public static void ChangeMusicVolume(float volume)
    {
        SoundManager.SetMusicVolume(volume);
    }

    public static void ToggleMusicMuted()
    {
        SoundManager.SetMusicMuted(!SoundManager.GetMusicMuted());
    }

    public static void ToggleSoundMuted()
    {
        SoundManager.SetSoundMuted(!SoundManager.GetSoundMuted());
    }
}
