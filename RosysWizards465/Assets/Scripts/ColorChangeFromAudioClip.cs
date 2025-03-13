using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeFromAudioClip : MonoBehaviour
{

    public AudioSource source;
    public Color minColor;
    public Color maxColor;
    public AudioDetection audioDetection;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float loudness = audioDetection.getLoudnessFromAudioClip(source.timeSamples, source.clip);

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.Lerp(minColor, maxColor, loudness);
    }
}
