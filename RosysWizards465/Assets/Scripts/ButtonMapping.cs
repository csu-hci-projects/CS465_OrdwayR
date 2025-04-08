public class ButtonMapping
{

    public static GlyphType MapButtonToGlyphType(ButtonType mappedButton)
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

    public static ButtonType MapButtonToType(string button, bool isConnector = false)
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




}