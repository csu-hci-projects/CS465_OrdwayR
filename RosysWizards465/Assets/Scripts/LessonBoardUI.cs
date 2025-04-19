using UnityEngine;
using Image = UnityEngine.UI.Image;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using Oculus.Interaction.PoseDetection;




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

    [System.Serializable]
    public class Spell
    {
        public Image primaryGlyphImage;
        public Image connectorImage;
        public Image secondaryGlyphImage;
    }



    [Header("Input Lesson")]
    public ControlSet inputControlSet;
    public GlyphType inputGlyph;
    public int currentInput = 0;

    [SerializeField] private InputLessonImages inputLessonImages;



    [Header("Glyph Lesson")]
    public GlyphType primaryGlyph;
    public ConnectorType connector;
    public GlyphType secondaryGlyph;

    [SerializeField] private GlyphLessonImages glyphLessonImages;


    [Header("Spell Lesson")]
    public GlyphType spellPrimaryGlyph;
    public ConnectorType spellConnector;
    public GlyphType spellSecondaryGlyph;

    [SerializeField] private GlyphLessonImages spellLessonImages;



    [Header("Managers")]
    [SerializeField] private SpriteManager spriteManager;

    public static List<Binding> bindingList = new List<Binding>();
    public void setBindingList()
    {
        switch (GameSettings.Instance.controlType)
        {
            case ControlType.ControllerOneHand:
                bindingList = Binding.OneHandControllerBindingList;
                break;
            case ControlType.ControllerTwoHand:
                bindingList = Binding.TwoHandControllerBindingList;
                break;
            case ControlType.GestureOneHand:
                bindingList = Binding.OneHandGestureBindingList;
                break;
            case ControlType.GestureTwoHand:
                bindingList = Binding.TwoHandGestureBindingList;
                break;
            case ControlType.GestureCombined:
                bindingList = Binding.CombinedGestureBindingList;
                break;
            default:
                Debug.LogWarning("Unknown control type");
                break;
        }
    }


    void Start()
    {
        setBindingList();

        glyphLessonImages.primaryGlyphImage.sprite = spriteManager.GetGlyphSprite(primaryGlyph);
        glyphLessonImages.connectorImage.sprite = spriteManager.GetConnectorSprite(connector);
        glyphLessonImages.secondaryGlyphImage.sprite = spriteManager.GetGlyphSprite(secondaryGlyph);

        inputControlSet = bindingList[currentInput].button;
        inputGlyph = bindingList[currentInput].glyph;


        spellLessonImages.primaryGlyphImage.sprite = spriteManager.GetGlyphSprite(spellPrimaryGlyph);
        spellLessonImages.connectorImage.sprite = spriteManager.GetConnectorSprite(spellConnector);
        spellLessonImages.secondaryGlyphImage.sprite = spriteManager.GetGlyphSprite(spellSecondaryGlyph);
    }

    void Update()
    {
        glyphLessonImages.primaryGlyphImage.sprite = spriteManager.GetGlyphSprite(primaryGlyph);
        glyphLessonImages.connectorImage.sprite = spriteManager.GetConnectorSprite(connector);
        glyphLessonImages.secondaryGlyphImage.sprite = spriteManager.GetGlyphSprite(secondaryGlyph);

        inputLessonImages.controllerImage.sprite = spriteManager.GetControlSetSprite(inputControlSet);
        float rotationY = (GameSettings.Instance.controlType is ControlType.GestureOneHand) && currentInput % 2 == 1 ? 180 : 0;
        inputLessonImages.controllerImage.transform.localRotation = Quaternion.Euler(
            inputLessonImages.controllerImage.transform.localRotation.eulerAngles.x,
            rotationY,
            inputLessonImages.controllerImage.transform.localRotation.eulerAngles.z
        );
        inputLessonImages.equalImage.sprite = spriteManager.GetEqualSprite();
        inputLessonImages.glyphImage.sprite = spriteManager.GetGlyphSprite(inputGlyph);

        spellLessonImages.primaryGlyphImage.sprite = spriteManager.GetGlyphSprite(spellPrimaryGlyph);
        spellLessonImages.connectorImage.sprite = spriteManager.GetConnectorSprite(spellConnector);
        spellLessonImages.secondaryGlyphImage.sprite = spriteManager.GetGlyphSprite(spellSecondaryGlyph);

    }

    public void UpdateGlyphUI(GlyphType primaryGlyph, ConnectorType connector, GlyphType secondaryGlyph)
    {
        this.primaryGlyph = primaryGlyph;
        this.connector = connector;
        this.secondaryGlyph = secondaryGlyph;
        this.spellPrimaryGlyph = primaryGlyph;
        this.spellConnector = connector;
        this.spellSecondaryGlyph = secondaryGlyph;
    }

    public void UpdateInputUI(ControlSet button, GlyphType inputGlyph)
    {
        if (GameSettings.Instance.controlType == ControlType.GestureOneHand || 
            GameSettings.Instance.controlType == ControlType.GestureTwoHand)
        {
            this.inputControlSet = ButtonMapping.MapControllerToGesture(button);
        }
        else if (GameSettings.Instance.controlType == ControlType.GestureCombined)
        {
            this.inputControlSet = ButtonMapping.MapControllerToCombinedGesture(button);
        }
        else
        {
            this.inputControlSet = button;
        }
        this.inputGlyph = inputGlyph;

    }

    public void ColorAndClear(bool status)
    {
        StartCoroutine(ChangeColorAndClear(status));
    }

    public void ChangeColorAndNext(bool status)
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
        inputControlSet = ControlSet.None;
        inputGlyph = GlyphType.None;
        spellPrimaryGlyph = GlyphType.None;
        spellConnector = ConnectorType.None;
        spellSecondaryGlyph = GlyphType.None;
    }



    public void nextInput()
    {
        if (currentInput < bindingList.Count - 1)
        {
            currentInput++;
        }
        else
        {
            currentInput = 0;
        }
        inputControlSet = bindingList[currentInput].button;
        inputGlyph = bindingList[currentInput].glyph;
    }


    public bool isCorrectGlyphSpell(GlyphType primaryGlyph, ConnectorType connector, GlyphType secondaryGlyph)
    {
        Debug.Log("Glyph: " + primaryGlyph + ", Connector: " + connector + ", Glyph2: " + secondaryGlyph);
        Debug.Log("Glyph: " + this.primaryGlyph + ", Connector: " + this.connector + ", Glyph2: " + this.secondaryGlyph);
        return this.primaryGlyph == primaryGlyph && this.connector == connector && this.secondaryGlyph == secondaryGlyph;
    }

    public bool isCorrectSpell(GlyphType primaryGlyph, ConnectorType connector, GlyphType secondaryGlyph)
    {
        Debug.Log("Spell: " + primaryGlyph + ", Connector: " + connector + ", Glyph2: " + secondaryGlyph);
        Debug.Log("Spell: " + this.spellPrimaryGlyph + ", Connector: " + this.spellConnector + ", Glyph2: " + this.spellSecondaryGlyph);
        return this.spellPrimaryGlyph == primaryGlyph && this.spellConnector == connector && this.spellSecondaryGlyph == secondaryGlyph;
    }


    public bool isCorrectInput(ControlSet button)
    {
        if (GameSettings.Instance.controlType == ControlType.GestureOneHand ||
            GameSettings.Instance.controlType == ControlType.GestureTwoHand)
        {
            return inputControlSet == ButtonMapping.MapControllerToGesture(button);
        }
        else if (GameSettings.Instance.controlType == ControlType.GestureCombined)
        {
            return inputControlSet == ButtonMapping.MapControllerToCombinedGesture(button);
        }
        else
        {
            return inputControlSet == button;
        }


    }

    public bool isCorrectInput(GlyphType glyph)
    {
        return inputGlyph == glyph;
    }


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




    public void changeUIColors(Color color)
    {
        inputLessonImages.controllerImage.color = color;
        inputLessonImages.equalImage.color = color;
        inputLessonImages.glyphImage.color = color;
        glyphLessonImages.primaryGlyphImage.color = color;
        glyphLessonImages.connectorImage.color = color;
        glyphLessonImages.secondaryGlyphImage.color = color;
        spellLessonImages.primaryGlyphImage.color = color;
        spellLessonImages.connectorImage.color = color;
        spellLessonImages.secondaryGlyphImage.color = color;
    }

    public void randomizeBoardUI()
    {
        primaryGlyph = (GlyphType)Random.Range(0, 4);
        connector = (ConnectorType)Random.Range(0, 2);
        secondaryGlyph = (GlyphType)Random.Range(0, 4);
        spellPrimaryGlyph = (GlyphType)Random.Range(0, 4);
        spellConnector = (ConnectorType)Random.Range(0, 2);
        spellSecondaryGlyph = (GlyphType)Random.Range(0, 4);
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





