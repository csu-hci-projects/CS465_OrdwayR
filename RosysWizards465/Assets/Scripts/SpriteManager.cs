
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private Sprite noneImage;

    public List<Sprite> glyphSprites;
    public List<Sprite> connectorSprites;
    public List<Sprite> controlSprites;
    public List<Sprite> gestureSprites;


    private Sprite attackImage => glyphSprites[0];
    private Sprite defenseImage => glyphSprites[1];
    private Sprite healthImage => glyphSprites[2];
    private Sprite buffImage => glyphSprites[3];
    private Sprite linkImage => connectorSprites[0];
    private Sprite weaveImage => connectorSprites[1];
    private Sprite equalImage => connectorSprites[2];
    private Sprite buttonAImage => controlSprites[0];
    private Sprite buttonAPressedImage => controlSprites[1];
    private Sprite buttonBImage => controlSprites[2];
    private Sprite buttonBPressedImage => controlSprites[3];
    private Sprite buttonXImage => controlSprites[4];
    private Sprite buttonXPressedImage => controlSprites[5];
    private Sprite buttonYImage => controlSprites[6];
    private Sprite buttonYPressedImage => controlSprites[7];
    private Sprite gripLeftImage => controlSprites[8];
    private Sprite gripLeftPressedImage => controlSprites[9];
    private Sprite gripRightImage => controlSprites[10];
    private Sprite gripRightPressedImage => controlSprites[11];
    private Sprite triggerLeftImage => controlSprites[12];
    private Sprite triggerLeftPressedImage => controlSprites[13];
    private Sprite triggerRightImage => controlSprites[14];
    private Sprite triggerRightPressedImage => controlSprites[15];
    private Sprite fingerGun => gestureSprites[0];
    private Sprite peaceSign => gestureSprites[1];
    private Sprite rocker => gestureSprites[2];
    private Sprite shakka => gestureSprites[3];

    public Sprite GetGlyphSprite(GlyphType glyphType)
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

    public Sprite GetConnectorSprite(ConnectorType connectorType)
    {
        return connectorType switch
        {
            ConnectorType.Link => linkImage,
            ConnectorType.Weave => weaveImage,
            ConnectorType.None => noneImage,
            _ => noneImage
        };
    }

    public Sprite GetEqualSprite()
    {
        return equalImage;
    }



    public Sprite GetControlSetSprite(ControlSet controlSet)
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
            ControlSet.FingerGunRight => fingerGun,
            ControlSet.FingerGunLeft => fingerGun,
            ControlSet.PeaceSignRight => peaceSign,
            ControlSet.PeaceSignLeft => peaceSign,
            ControlSet.RockerRight => rocker,
            ControlSet.RockerLeft => rocker,
            ControlSet.ShakkaRight => shakka,
            ControlSet.ShakkaLeft => shakka,
            ControlSet.HeartHands => noneImage,
            ControlSet.Triangle => noneImage,
            ControlSet.Cutesy => noneImage,
            ControlSet.Clap => noneImage,

            _ => noneImage
        };
    }
   

}



