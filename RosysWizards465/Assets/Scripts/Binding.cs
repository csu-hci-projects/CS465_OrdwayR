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


    public static List<Binding> OneHandControllerBindingList = new List<Binding>()
    {
        new Binding(ControlSet.ButtonA,GlyphType.Attack),
        new Binding(ControlSet.ButtonX,GlyphType.Attack),
        new Binding(ControlSet.ButtonB,GlyphType.Defense),
         new Binding(ControlSet.ButtonY,GlyphType.Defense),
             new Binding(ControlSet.GripRight,GlyphType.Buff),
          new Binding(ControlSet.GripLeft,GlyphType.Buff),
        new Binding(ControlSet.TriggerRight,GlyphType.Health),
            new Binding(ControlSet.TriggerLeft,GlyphType.Health),



    };

    public static List<Binding> TwoHandControllerBindingList = new List<Binding>()
    {
        new Binding(ControlSet.ButtonA,GlyphType.Attack),
        new Binding(ControlSet.ButtonB,GlyphType.Defense),
             new Binding(ControlSet.GripRight,GlyphType.Buff),
        new Binding(ControlSet.TriggerRight,GlyphType.Health),

    };
    public static List<Binding> OneHandGestureBindingList = new List<Binding>()
    {
          new Binding(ControlSet.FingerGunRight,GlyphType.Attack),
        new Binding(ControlSet.FingerGunLeft,GlyphType.Attack),
        new Binding(ControlSet.PeaceSignRight,GlyphType.Defense),
         new Binding(ControlSet.PeaceSignLeft,GlyphType.Defense),
             new Binding(ControlSet.RockerRight,GlyphType.Buff),
          new Binding(ControlSet.RockerLeft,GlyphType.Buff),
        new Binding(ControlSet.ShakkaRight,GlyphType.Health),
            new Binding(ControlSet.ShakkaLeft,GlyphType.Health),

    };

    public static List<Binding> TwoHandGestureBindingList = new List<Binding>()
    {
          new Binding(ControlSet.FingerGunRight,GlyphType.Attack),
        new Binding(ControlSet.PeaceSignRight,GlyphType.Defense),
             new Binding(ControlSet.RockerRight,GlyphType.Buff),
        new Binding(ControlSet.ShakkaRight,GlyphType.Health),

    };

    public static List<Binding> CombinedGestureBindingList = new List<Binding>()
    {
          new Binding(ControlSet.Clap,GlyphType.Attack),
        new Binding(ControlSet.Triangle,GlyphType.Defense),
             new Binding(ControlSet.Cutesy,GlyphType.Buff),
        new Binding(ControlSet.HeartHands,GlyphType.Health),

    };





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
    FingerGunRight,
    FingerGunLeft,
    PeaceSignRight,
    PeaceSignLeft,
    RockerRight,
    RockerLeft,
    ShakkaRight,
    ShakkaLeft,
    HeartHands,
    Triangle,
    Cutesy,
    Clap,

    None
}
