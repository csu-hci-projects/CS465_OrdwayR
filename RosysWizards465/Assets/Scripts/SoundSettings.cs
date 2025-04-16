using System.Collections.Generic;
using UnityEngine;
public class SoundSettings : MonoBehaviour
{
    public List<AudioSource> dialogueAudioSources;
    public List<AudioSource> musicAudioSources;

    void Start()
    {
        float dialogueVolume = GameSettings.Instance.dialogueVolume;
        float musicVolume = GameSettings.Instance.musicVolume;

        foreach (var source in dialogueAudioSources)
        {
            source.volume = dialogueVolume;
        }

        foreach (var source in musicAudioSources)
        {
            source.volume = musicVolume;
        }
    }
}
