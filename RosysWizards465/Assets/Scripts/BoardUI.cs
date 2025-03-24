
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class BoardUI : MonoBehaviour
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

