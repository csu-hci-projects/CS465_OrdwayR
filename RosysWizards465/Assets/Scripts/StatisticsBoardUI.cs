
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsBoardUI : MonoBehaviour
{

    [Header("Input Lesson Stats")]
    public List<float> inputLessonInputTimes = new List<float>();
    public int numberOfInputMistakes = 0;

    [Header("Glyph Lesson Stats")]
    public List<float> glyphLessonInputTimes = new List<float>();
    public float glyphLessonTime = 0f;
    public int numberOfGlyphMistakes = 0;

    [Header("Spell Lesson Stats")]
    public List<float> spellLessonInputTimes = new List<float>();
    public float spellLessonTime = 0f;
    public int numberOfSpellMistakes = 0;

    [Header("Text UI Elements")]
    public Text InputAverageTimeText;
    public Text InputMistakesText;

    public Text GlyphAverageTimeText;
    public Text GlyphMistakesText;
    public Text GlyphLessonTimeText;

    public Text SpellAverageTimeText;
    public Text SpellMistakesText;
    public Text SpellLessonTimeText;



    void Start()
    {
        ResetAllStats();
    }

    public void AddInputLessonMistake()
    {
        numberOfInputMistakes++;
    }
    public void AddInputLessonTime(float time)
    {
        inputLessonInputTimes.Add(time);
    }

    public void SetGlyphLessonCompletionTime(float time)
    {
        glyphLessonTime = time;
    }
    public void AddGlyphLessonMistake()
    {
        numberOfGlyphMistakes++;
    }

    public void AddGlyphLessonTime(float time)
    {
        glyphLessonInputTimes.Add(time);
    }

    public void SetSpellLessonCompletionTime(float time)
    {
        spellLessonTime = time;
    }
    public void AddSpellLessonMistake()
    {
        numberOfSpellMistakes++;
    }
    public void AddSpellLessonTime(float time)
    {
        spellLessonInputTimes.Add(time);
    }

    public void ResetAllStats()
    {
        inputLessonInputTimes.Clear();
        glyphLessonInputTimes.Clear();
        spellLessonInputTimes.Clear();
        numberOfInputMistakes = 0;
        numberOfGlyphMistakes = 0;
        numberOfSpellMistakes = 0;
        glyphLessonTime = 0f;
        spellLessonTime = 0f;
    }

    public float GetInputLessonTimeAverage()
    {
        if (inputLessonInputTimes.Count == 0) return 0f;
        float total = 0f;
        foreach (float time in inputLessonInputTimes)
        {
            total += time;
        }
        return total / inputLessonInputTimes.Count;
    }
    public float GetGlyphLessonTimeAverage()
    {
        if (glyphLessonInputTimes.Count == 0) return 0f;
        float total = 0f;
        foreach (float time in glyphLessonInputTimes)
        {
            total += time;
        }
        return total / glyphLessonInputTimes.Count;
    }
    public float GetSpellLessonTimeAverage()
    {
        if (spellLessonInputTimes.Count == 0) return 0f;
        float total = 0f;
        foreach (float time in spellLessonInputTimes)
        {
            total += time;
        }
        return total / spellLessonInputTimes.Count;
    }
    public void DisplayLessonStats()
    {
        Debug.Log("Input Lesson Stats: ");
        Debug.Log("Average Input Time: " + GetInputLessonTimeAverage());
        Debug.Log("Number of Mistakes: " + numberOfInputMistakes);

        Debug.Log("Glyph Lesson Stats: ");
        Debug.Log("Average Glyph Time: " + GetGlyphLessonTimeAverage());
        Debug.Log("Number of Mistakes: " + numberOfGlyphMistakes);
        Debug.Log("Glyph Lesson Time: " + glyphLessonTime);

        Debug.Log("Spell Lesson Stats: ");
        Debug.Log("Average Spell Time: " + GetSpellLessonTimeAverage());
        Debug.Log("Number of Mistakes: " + numberOfSpellMistakes);
        Debug.Log("Spell Lesson Time: " + spellLessonTime);


        InputAverageTimeText.text = "Average Input Time: " + GetInputLessonTimeAverage().ToString("F2") + " seconds";
        InputMistakesText.text = "Number of Mistakes: " + numberOfInputMistakes.ToString();

        GlyphAverageTimeText.text = "Average Input Time: " + GetGlyphLessonTimeAverage().ToString("F2") + " seconds";
        GlyphMistakesText.text = "Number of Mistakes: " + numberOfGlyphMistakes.ToString();
        GlyphLessonTimeText.text = "Completion Time: " + glyphLessonTime.ToString("F2") + " seconds";

        SpellAverageTimeText.text = "Average Input Time: " + GetSpellLessonTimeAverage().ToString("F2") + " seconds";
        SpellMistakesText.text = "Number of Mistakes: " + numberOfSpellMistakes.ToString();
        SpellLessonTimeText.text = "Completion Time: " + spellLessonTime.ToString("F2") + " seconds";
    }

}