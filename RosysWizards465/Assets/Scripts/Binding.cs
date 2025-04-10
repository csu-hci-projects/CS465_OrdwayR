using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Binding
{

    public ControlSet button;
    public GlyphType glyph;
    public Binding(ControlSet button, GlyphType glyph)
    {
        this.button = button;
        this.glyph = glyph;
    }


    public static List<Binding> inputList = new List<Binding>()
    {
        new Binding(ControlSet.ButtonA,GlyphType.Attack),
        new Binding(ControlSet.ButtonX,GlyphType.Attack),
        new Binding(ControlSet.ButtonB,GlyphType.Defense),
         new Binding(ControlSet.ButtonY,GlyphType.Defense),
          new Binding(ControlSet.GripLeft,GlyphType.Buff),
           new Binding(ControlSet.GripRight,GlyphType.Buff),
            new Binding(ControlSet.TriggerLeft,GlyphType.Health),
             new Binding(ControlSet.TriggerRight,GlyphType.Health)


    };

}