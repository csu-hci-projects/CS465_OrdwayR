
using UnityEngine;
using Image = UnityEngine.UI.Image;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private Sprite noneImage;

    public List<Sprite> glyphs;
    public List<Sprite> connectors;
    public List<Sprite> controls;


    private Sprite attackImage => glyphs[0];
    private Sprite defenseImage => glyphs[1];
    private Sprite healthImage => glyphs[2];
    private Sprite buffImage => glyphs[3];
    private Sprite linkImage => connectors[0];
    private Sprite weaveImage => connectors[1];
    private Sprite buttonAImage => controls[0];
    private Sprite buttonAPressedImage => controls[1];
    private Sprite buttonBImage => controls[2];
    private Sprite buttonBPressedImage => controls[3];
    private Sprite buttonXImage => controls[4];
    private Sprite buttonXPressedImage => controls[5];
    private Sprite buttonYImage => controls[6];
    private Sprite buttonYPressedImage => controls[7];
    private Sprite gripLeftImage => controls[8];
    private Sprite gripLeftPressedImage => controls[9];
    private Sprite gripRightImage => controls[10];
    private Sprite gripRightPressedImage => controls[11];
    private Sprite triggerLeftImage => controls[12];
    private Sprite triggerLeftPressedImage => controls[13];
    private Sprite triggerRightImage => controls[14];
    private Sprite triggerRightPressedImage => controls[15];
    public Sprite GetSprite(GlyphType glyphType)
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

    public Sprite GetSprite(ConnectorType connectorType)
    {
        return connectorType switch
        {
            ConnectorType.Link => linkImage,
            ConnectorType.Weave => weaveImage,
            ConnectorType.None => noneImage,
            _ => noneImage
        };
    }



    public Sprite GetControlSprite(ControlSet controlSet)
    {
        return controlSet switch
        {
            ControlSet.ButtonA => buttonAImage,
            ControlSet.ButtonAPressed => buttonAPressedImage,
            ControlSet.ButtonB => buttonBImage,
            ControlSet.ButtonBPressed => buttonBPressedImage,
            ControlSet.ButtonX => buttonXImage,
            ControlSet.ButtonXPressed => buttonXPressedImage,
            ControlSet.ButtonY => buttonYImage,
            ControlSet.ButtonYPressed => buttonYPressedImage,
            ControlSet.GripLeft => gripLeftImage,
            ControlSet.GripLeftPressed => gripLeftPressedImage,
            ControlSet.GripRight => gripRightImage,
            ControlSet.GripRightPressed => gripRightPressedImage,
            ControlSet.TriggerLeft => triggerLeftImage,
            ControlSet.TriggerLeftPressed => triggerLeftPressedImage,
            ControlSet.TriggerRight => triggerRightImage,
            ControlSet.TriggerRightPressed => triggerRightPressedImage,
            _ => noneImage
        };
    }


}



