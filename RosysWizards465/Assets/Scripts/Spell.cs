using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spell
{

    public ButtonType button1;
    public ButtonType button2;
    public ButtonType button3;
    public string spellName;
    public Spell(ButtonType button1, ButtonType button2, ButtonType button3, string spellName)
    {
        this.button1 = button1;
        this.button2 = button2;
        this.button3 = button3;
        this.spellName = spellName;
    }

    public string getSpellName()
    {
        return spellName;
    }

    public bool MatchesButtonSequence(List<ButtonType> buttonsPressedList)
    {
        if (buttonsPressedList.Count < 3)
        {
            return false;
        }
        return buttonsPressedList[0] == button1 && buttonsPressedList[1] == button2 && buttonsPressedList[2] == button3;
    }

    public static List<Spell> spellList = new List<Spell>()
    {
        new Spell(ButtonType.Grab, ButtonType.Primary, ButtonType.Grab, "Spell 1"),
        new Spell(ButtonType.Grab, ButtonType.Primary, ButtonType.Trigger, "Spell 2"),
        new Spell(ButtonType.Grab, ButtonType.Primary, ButtonType.Primary, "Spell 3"),
        new Spell(ButtonType.Grab, ButtonType.Primary, ButtonType.Secondary, "Spell 4"),
        new Spell(ButtonType.Grab, ButtonType.Secondary, ButtonType.Grab, "Spell 5"),
        new Spell(ButtonType.Grab, ButtonType.Secondary, ButtonType.Trigger, "Spell 6"),
        new Spell(ButtonType.Grab, ButtonType.Secondary, ButtonType.Primary, "Spell 7"),
        new Spell(ButtonType.Grab, ButtonType.Secondary, ButtonType.Secondary, "Spell 8"),
        new Spell(ButtonType.Trigger, ButtonType.Primary, ButtonType.Grab, "Spell 9"),
        new Spell(ButtonType.Trigger, ButtonType.Primary, ButtonType.Trigger, "Spell 10"),
        new Spell(ButtonType.Trigger, ButtonType.Primary, ButtonType.Primary, "Spell 11"),
        new Spell(ButtonType.Trigger, ButtonType.Primary, ButtonType.Secondary, "Spell 12"),
        new Spell(ButtonType.Trigger, ButtonType.Secondary, ButtonType.Grab, "Spell 13"),
        new Spell(ButtonType.Trigger, ButtonType.Secondary, ButtonType.Trigger, "Spell 14"),
        new Spell(ButtonType.Trigger, ButtonType.Secondary, ButtonType.Primary, "Spell 15"),
        new Spell(ButtonType.Trigger, ButtonType.Secondary, ButtonType.Secondary, "Spell 16"),
        new Spell(ButtonType.Primary, ButtonType.Primary, ButtonType.Grab, "Spell 17"),
        new Spell(ButtonType.Primary, ButtonType.Primary, ButtonType.Trigger, "Spell 18"),
        new Spell(ButtonType.Primary, ButtonType.Primary, ButtonType.Primary, "Spell 19"),
        new Spell(ButtonType.Primary, ButtonType.Primary, ButtonType.Secondary, "Spell 20"),
        new Spell(ButtonType.Primary, ButtonType.Secondary, ButtonType.Grab, "Spell 21"),
        new Spell(ButtonType.Primary, ButtonType.Secondary, ButtonType.Trigger, "Spell 22"),
        new Spell(ButtonType.Primary, ButtonType.Secondary, ButtonType.Primary, "Spell 23"),
        new Spell(ButtonType.Primary, ButtonType.Secondary, ButtonType.Secondary, "Spell 24"),
        new Spell(ButtonType.Secondary, ButtonType.Primary, ButtonType.Grab, "Spell 25"),
        new Spell(ButtonType.Secondary, ButtonType.Primary, ButtonType.Trigger, "Spell 26"),
        new Spell(ButtonType.Secondary, ButtonType.Primary, ButtonType.Primary, "Spell 27"),
        new Spell(ButtonType.Secondary, ButtonType.Primary, ButtonType.Secondary, "Spell 28"),
        new Spell(ButtonType.Secondary, ButtonType.Secondary, ButtonType.Grab, "Spell 29"),
        new Spell(ButtonType.Secondary, ButtonType.Secondary, ButtonType.Trigger, "Spell 30"),
        new Spell(ButtonType.Secondary, ButtonType.Secondary, ButtonType.Primary, "Spell 31"),
        new Spell(ButtonType.Secondary, ButtonType.Secondary, ButtonType.Secondary, "Spell 32")
    };

}