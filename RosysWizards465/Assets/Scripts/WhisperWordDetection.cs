using System;
using System.Collections;
using System.Collections.Generic;
using Samples.Whisper;
using UnityEngine;

public class WhisperWordDetection : MonoBehaviour

{
    [SerializeField] public Whisper whisper;

    public GameObject menu;


    void Start()
    {
        ShowObjects();
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(whisper.outputText);
        if (whisper.outputText != null)
        {
            string[] words = whisper.outputText.Split(' ');
            foreach (string word in words)
            {
                string parsed = word.ToLower();
                if (parsed == "weave" || parsed == "weaving" || parsed == "weaved"
                || parsed == "weave." || parsed == "weaving." || parsed == "weaved."
                || parsed == "weave!" || parsed == "weaving!" || parsed == "weaved!"
                || parsed == "weave?" || parsed == "weaving?" || parsed == "weaved?"
                || parsed == "weave," || parsed == "weaving," || parsed == "weaved,"
                || parsed == "we've" || parsed == "we've." || parsed == "we've!"
                || parsed == "we've?" || parsed == "we've," || parsed == "we've,")

                {
                    Debug.Log("Weave detected");
                    menu.SetActive(true);
                }

            }
        }
    }

    void ShowObjects()
    {
        TMPro.TextMeshProUGUI[] textObjects = FindObjectsByType<TMPro.TextMeshProUGUI>(FindObjectsSortMode.None);
        foreach (TMPro.TextMeshProUGUI text in textObjects)
        {
            text.enabled = true;
            Color color = text.color;
            color.a = 1f;
            text.color = color;
        }

        Renderer[] renderers = FindObjectsByType<Renderer>(FindObjectsSortMode.None);
        foreach (Renderer renderer in renderers)
        {
            if (renderer.material.HasProperty("_Color"))
            {
                renderer.enabled = true;
                Color color = renderer.material.color;
                color.a = 0.796875f;
                renderer.material.color = color;
            }
            else
            {
                Debug.Log("No color property found");
            }
        }
    }
}
