
using UnityEngine;
using Image = UnityEngine.UI.Image;
using System.Collections;

public class
BoardUI : MonoBehaviour
{
    public GlyphType primaryGlyph;
    public ConnectorType connector;
    public GlyphType secondaryGlyph;
    [SerializeField] private Image primaryGlyphImage;
    [SerializeField] private Image connectorImage;
    [SerializeField] private Image secondaryGlyphImage;
    [SerializeField] private Sprite defenseImage;
    [SerializeField] private Sprite attackImage;
    [SerializeField] private Sprite healthImage;
    [SerializeField] private Sprite buffImage;
    [SerializeField] private Sprite linkImage;
    [SerializeField] private Sprite weaveImage;
    [SerializeField] private Sprite noneImage;



    void Start()
    {
        primaryGlyphImage.sprite = GetSprite(primaryGlyph);
        connectorImage.sprite = GetSprite(connector);
        secondaryGlyphImage.sprite = GetSprite(secondaryGlyph);
    }

    void Update()
    {
        primaryGlyphImage.sprite = GetSprite(primaryGlyph);
        connectorImage.sprite = GetSprite(connector);
        secondaryGlyphImage.sprite = GetSprite(secondaryGlyph);
    }


    private Sprite GetSprite(GlyphType glyphType)
    {
        return glyphType switch
        {
            GlyphType.Defense => defenseImage,
            GlyphType.Health => healthImage,
            GlyphType.Attack => attackImage,
            GlyphType.Buff => buffImage,
            GlyphType.None => noneImage,
            _ => noneImage
        };
    }

    private Sprite GetSprite(ConnectorType connectorType)
    {
        return connectorType switch
        {
            ConnectorType.Link => linkImage,
            ConnectorType.Weave => weaveImage,
            ConnectorType.None => noneImage,
            _ => noneImage
        };
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
        primaryGlyphImage.color = color;
        connectorImage.color = color;
        secondaryGlyphImage.color = color;
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

