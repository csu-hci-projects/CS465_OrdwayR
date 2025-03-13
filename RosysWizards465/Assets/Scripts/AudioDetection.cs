using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDetection : MonoBehaviour
{

    AudioClip recording;
    public int sampleWindow = 64;
    void Start()
    {
        MicrophoneToAudioClip();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MicrophoneToAudioClip()
    {
        string microphoneName = Microphone.devices[0];
        recording = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoudnessFromMicrophone()
    {
        return getLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), recording);
    }

    public float getLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {


        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
        {
            return 0;
        }


        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);
        float totalLoudness = 0;

        foreach (float sample in waveData)
        {
            totalLoudness += Mathf.Abs(sample);
        }


        return totalLoudness / sampleWindow;
    }
}
