using UnityEngine;
using Image = UnityEngine.UI.Image;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;




public class BoardUI : MonoBehaviour
{
    [System.Serializable]
    public class InputLessonImages
    {
        public Image controllerImage;
        public Image equalImage;
        public Image glyphImage;
    }

    [System.Serializable]
    public class GlyphLessonImages
    {
        public Image primaryGlyphImage;
        public Image connectorImage;
        public Image secondaryGlyphImage;
    }



    [Header("Input Lesson")]
    public ControlSet inputButton;
    public GlyphType inputGlyph;
    public int currentInput = 0;

    [SerializeField] private InputLessonImages inputLessonImages;



    [Header("Glyph Lesson")]
    public GlyphType primaryGlyph;
    public ConnectorType connector;
    public GlyphType secondaryGlyph;

    [SerializeField] private GlyphLessonImages glyphLessonImages;



    [Header("Managers")]
    [SerializeField] private SpriteManager spriteManager;

    public void SetLayout(string layoutName)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(
                child.name == layoutName ||
            (layoutName is "Intro" or "InputToGlyph"
            or "GlyphToSpell" or "SpellIntro" or "SpellToExit"
            && child.name == "Start")
            || (layoutName is "InputLesson" or "GlyphLesson" or "SpellLesson"
            && child.name == "Input Progress"));
        }
    }




    void Start()
    {
        glyphLessonImages.primaryGlyphImage.sprite = spriteManager.GetSprite(primaryGlyph);
        glyphLessonImages.connectorImage.sprite = spriteManager.GetSprite(connector);
        glyphLessonImages.secondaryGlyphImage.sprite = spriteManager.GetSprite(secondaryGlyph);
        inputButton = Binding.inputList[currentInput].button;
        inputGlyph = Binding.inputList[currentInput].glyph;
    }

    void Update()
    {
        glyphLessonImages.primaryGlyphImage.sprite = spriteManager.GetSprite(primaryGlyph);
        glyphLessonImages.connectorImage.sprite = spriteManager.GetSprite(connector);
        glyphLessonImages.secondaryGlyphImage.sprite = spriteManager.GetSprite(secondaryGlyph);
        inputLessonImages.controllerImage.sprite = spriteManager.GetSprite(inputButton);
        inputLessonImages.equalImage.sprite = spriteManager.GetEqualSprite();
        inputLessonImages.glyphImage.sprite = spriteManager.GetSprite(inputGlyph);

    }

    public void UpdateGlyphUI(GlyphType primaryGlyph, ConnectorType connector, GlyphType secondaryGlyph)
    {
        this.primaryGlyph = primaryGlyph;
        this.connector = connector;
        this.secondaryGlyph = secondaryGlyph;
    }

    public void UpdateInputUI(ControlSet button, GlyphType inputGlyph)
    {
        this.inputButton = button;
        this.inputGlyph = inputGlyph;

    }

    public void CheckAndClear(bool status)
    {
        StartCoroutine(ChangeColorAndClear(status));
    }

    public void CheckAndNextInput(bool status)
    {
        StartCoroutine(ChangeColorAndNextInput(status));
    }

    private IEnumerator ChangeColorAndNextInput(bool status)
    {
        Color targetColor = status ? Color.green : Color.red;
        changeUIColors(targetColor);
        yield return new WaitForSeconds(2);
        if (status) nextInput();
        changeUIColors(Color.white);
    }

    public void CheckAndRandomizeGlyph(bool status)
    {
        StartCoroutine(ChangeColorAndRandomize(status));
    }

    private IEnumerator ChangeColorAndClear(bool status)
    {
        Color targetColor = status ? Color.green : Color.red;
        changeUIColors(targetColor);
        yield return new WaitForSeconds(2);
        clearBoardUI();
        changeUIColors(Color.white);
    }

    private IEnumerator ChangeColorAndRandomize(bool status)
    {
        Color targetColor = status ? Color.green : Color.red;
        changeUIColors(targetColor);
        yield return new WaitForSeconds(2);
        randomizeBoardUI();
        changeUIColors(Color.white);
    }

    public void clearBoardUI()
    {
        primaryGlyph = GlyphType.None;
        connector = ConnectorType.None;
        secondaryGlyph = GlyphType.None;
    }


    public void changeUIColors(Color color)
    {
        inputLessonImages.controllerImage.color = color;
        inputLessonImages.equalImage.color = color;
        inputLessonImages.glyphImage.color = color;
        glyphLessonImages.primaryGlyphImage.color = color;
        glyphLessonImages.connectorImage.color = color;
        glyphLessonImages.secondaryGlyphImage.color = color;
    }

    public void randomizeBoardUI()
    {
        primaryGlyph = (GlyphType)Random.Range(0, 4);
        connector = (ConnectorType)Random.Range(0, 2);
        secondaryGlyph = (GlyphType)Random.Range(0, 4);
    }


    public void nextInput()
    {
        if (currentInput < 7)
        {
            currentInput++;
        }
        else
        {
            currentInput = 0;
        }
        inputButton = Binding.inputList[currentInput].button;
        inputGlyph = Binding.inputList[currentInput].glyph;
    }

    public bool isCorrectSpell(GlyphType primaryGlyph, ConnectorType connector, GlyphType secondaryGlyph)
    {
        return this.primaryGlyph == primaryGlyph && this.connector == connector && this.secondaryGlyph == secondaryGlyph;
    }

    public bool isCorrectInput(ControlSet button)
    {
        return inputButton == button;
    }
}

public enum GlyphType
{
    Defense,
    Health,
    Attack,
    Buff,
    None
}
public enum ConnectorType
{
    Link,
    Weave,
    None
}

public enum ControlSet
{
    ButtonA,
    ButtonAPressed,
    ButtonB,
    ButtonBPressed,
    ButtonX,
    ButtonXPressed,
    ButtonY,
    ButtonYPressed,
    GripLeft,
    GripLeftPressed,
    GripRight,
    GripRightPressed,
    TriggerLeft,
    TriggerLeftPressed,
    TriggerRight,
    TriggerRightPressed,
    None
}

