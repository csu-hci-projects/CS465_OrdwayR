
using UnityEngine;
using Image = UnityEngine.UI.Image;
using System.Collections;
using System.Collections.Generic;




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
    

    public GlyphType primaryGlyph;
    public ConnectorType connector;
    public GlyphType secondaryGlyph;

    [Header("Lesson Image Connectors")]
    [SerializeField] private InputLessonImages inputLessonImages;


    [SerializeField] private GlyphLessonImages glyphLessonImages;



    [Header("Managers")]
    [SerializeField] private SpriteManager spriteManager;




    void Start()
    {
        glyphLessonImages.primaryGlyphImage.sprite = spriteManager.GetSprite(primaryGlyph);
        glyphLessonImages.connectorImage.sprite = spriteManager.GetSprite(connector);
        glyphLessonImages.secondaryGlyphImage.sprite = spriteManager.GetSprite(secondaryGlyph);
    }

    void Update()
    {
        glyphLessonImages.primaryGlyphImage.sprite = spriteManager.GetSprite(primaryGlyph);
        glyphLessonImages.connectorImage.sprite = spriteManager.GetSprite(connector);
        glyphLessonImages.secondaryGlyphImage.sprite = spriteManager.GetSprite(secondaryGlyph);
    }

    public void UpdateBoardUI(GlyphType primaryGlyph, ConnectorType connector, GlyphType secondaryGlyph)
    {
        this.primaryGlyph = primaryGlyph;
        this.connector = connector;
        this.secondaryGlyph = secondaryGlyph;
    }

    public void CheckAndClear(bool status)
    {
        StartCoroutine(ChangeColorAndClear(status));
    }

    public void CheckAndRandomize(bool status)
    {
        StartCoroutine(ChangeColorAndRandomize(status));
    }

    private IEnumerator ChangeColorAndClear(bool status)
    {
        Color targetColor = status ? Color.green : Color.red;
        changeGlyphColors(targetColor);
        yield return new WaitForSeconds(3);
        clearBoardUI();
        changeGlyphColors(Color.white);
    }

    private IEnumerator ChangeColorAndRandomize(bool status)
    {
        Color targetColor = status ? Color.green : Color.red;
        changeGlyphColors(targetColor);
        yield return new WaitForSeconds(3);
        randomizeBoardUI();
        changeGlyphColors(Color.white);
    }

    public void clearBoardUI()
    {
        primaryGlyph = GlyphType.None;
        connector = ConnectorType.None;
        secondaryGlyph = GlyphType.None;
    }

    public void changeGlyphColors(Color color)
    {
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

    public bool isCorrectSpell(GlyphType primaryGlyph, ConnectorType connector, GlyphType secondaryGlyph)
    {
        return this.primaryGlyph == primaryGlyph && this.connector == connector && this.secondaryGlyph == secondaryGlyph;
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

