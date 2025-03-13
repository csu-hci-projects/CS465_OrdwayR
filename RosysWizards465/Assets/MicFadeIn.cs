using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicFadeIn : MonoBehaviour
{

    public AudioDetection audioDetection;

    public float loudnessSensibility = 100;
    public float threshold = 1f;




    public float increaseRate = 1f;

    public float decreaseDuration = 15f;


    private float currentLerp = 0f;
    void Update()
    {
        float loudness = audioDetection.GetLoudnessFromMicrophone() * loudnessSensibility;
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
        TMPro.TextMeshProUGUI[] textObjects = gameObject.GetComponentsInChildren<TMPro.TextMeshProUGUI>();

        calculateLerp(loudness);
        setObjectAlpha(renderers);
        setTextAlpha(textObjects);
    }

    void setTextAlpha(TMPro.TextMeshProUGUI[] textObjects)
    {
        foreach (TMPro.TextMeshProUGUI text in textObjects)
        {
            Color color = text.color;
            color.a = Mathf.Lerp(0f, 1f, currentLerp);
            text.color = color;

            if (currentLerp <= 0.01f)
            {
                text.enabled = false;
            }
            else
            {
                text.enabled = true;
            }
        }
    }

    void setObjectAlpha(Renderer[] renderers)
    {
        foreach (Renderer renderer in renderers)
        {

            if (renderer.material.HasProperty("_Color"))
            {
                Color color = renderer.material.color;
                color.a = Mathf.Lerp(0f, 0.796875f, currentLerp);
                renderer.material.color = color;

                if (currentLerp <= 0.01f)
                {
                    renderer.enabled = false;
                }
                else
                {
                    renderer.enabled = true;
                }
            }
            else
            {
                Debug.Log("No color property found");
            }
        }
    }

    void calculateLerp(float loudness)
    {
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
        Debug.Log("Current Lerp: " + currentLerp);
    }


}
