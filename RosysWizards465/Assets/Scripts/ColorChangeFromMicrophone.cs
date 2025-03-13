using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeFromMicrophone : MonoBehaviour
{
    public Color minColor;
    public Color maxColor;
    public AudioDetection audioDetection;

    public float loudnessSensibility = 100;
    public float threshold = 1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    
    public float increaseRate = 0.5f; 
   
    public float decreaseDuration = 3f;

    
    private float currentLerp = 0f;
    void Update()
    {
        float loudness = audioDetection.GetLoudnessFromMicrophone() * loudnessSensibility;

        if (loudness < threshold)
        {
            loudness = 0;
        }

        if (loudness > threshold)
        {
            
            currentLerp += increaseRate * Time.deltaTime;
            currentLerp = Mathf.Clamp01(currentLerp);
        }
        else
        {
            
            currentLerp -= Time.deltaTime / decreaseDuration;
            currentLerp = Mathf.Clamp01(currentLerp);
        }

       
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.Lerp(minColor, maxColor, currentLerp);

    }
}
