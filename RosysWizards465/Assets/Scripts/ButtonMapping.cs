using System;
using NUnit.Framework;

public class ButtonMapping
{

    public static GlyphType MapButtonTypeToGlyphType(ButtonType mappedButton)
    {
        return mappedButton switch
        {
            ButtonType.Primary => GlyphType.Attack,
            ButtonType.Secondary => GlyphType.Defense,
            ButtonType.Trigger => GlyphType.Health,
            ButtonType.Grab => GlyphType.Buff,
            _ => GlyphType.None
        };
    }

    public static ConnectorType MapButtonToConnectorType(ButtonType mappedButton)
    {
        return mappedButton switch
        {
            ButtonType.Primary => ConnectorType.Link,
            ButtonType.Secondary => ConnectorType.Weave,
            _ => ConnectorType.None
        };
    }

    public static ControlSet MapRawToControlSet(string button)
    {
        return button switch
        {
            "A" => ControlSet.ButtonA,
            "B" => ControlSet.ButtonB,
            "X" => ControlSet.ButtonX,
            "Y" => ControlSet.ButtonY,
            "LG" => ControlSet.GripLeft,
            "RG" => ControlSet.GripRight,
            "LT" => ControlSet.TriggerLeft,
            "RT" => ControlSet.TriggerRight,
            _ => ControlSet.None
        };
    }

    public static ButtonType MapRawToButtonType(string button, bool isConnector = false)
    {
        if (isConnector)
        {
            return button switch
            {
                "LT" or "RT" or "A" or "X" => ButtonType.Primary,
                "LG" or "RG" or "B" or "Y" => ButtonType.Secondary,
                _ => ButtonType.Unknown
            };
        }

        return button switch
        {
            "A" or "X" => ButtonType.Primary,
            "B" or "Y" => ButtonType.Secondary,
            "LT" or "RT" => ButtonType.Trigger,
            "LG" or "RG" => ButtonType.Grab,
            _ => ButtonType.Unknown
        };
    }

    internal static ControlSet MapControllerToGesture(ControlSet button)
    {
        if (GameSettings.Instance.controlType == ControlType.GestureTwoHand)
        {
            return button switch
            {
                ControlSet.ButtonA => ControlSet.FingerGunRight,
                ControlSet.ButtonB => ControlSet.PeaceSignRight,
                ControlSet.ButtonX => ControlSet.FingerGunRight,
                ControlSet.ButtonY => ControlSet.PeaceSignRight,
                ControlSet.GripRight => ControlSet.RockerRight,
                ControlSet.GripLeft => ControlSet.RockerRight,
                ControlSet.TriggerRight => ControlSet.ShakkaRight,
                ControlSet.TriggerLeft => ControlSet.ShakkaRight,
                _ => button
            };
        }
        return button switch
        {
            ControlSet.ButtonA => ControlSet.FingerGunRight,
            ControlSet.ButtonB => ControlSet.PeaceSignRight,
            ControlSet.ButtonX => ControlSet.FingerGunLeft,
            ControlSet.ButtonY => ControlSet.PeaceSignLeft,
            ControlSet.GripRight => ControlSet.RockerRight,
            ControlSet.GripLeft => ControlSet.RockerLeft,
            ControlSet.TriggerRight => ControlSet.ShakkaRight,
            ControlSet.TriggerLeft => ControlSet.ShakkaLeft,
            _ => button
        };
    }
}