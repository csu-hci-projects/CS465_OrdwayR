using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Oculus.Interaction.Input;
using System.IO.MemoryMappedFiles;

public class ControllerActionInput : MonoBehaviour
{
    [SerializeField] private InputActionReference A;
    [SerializeField] private InputActionReference B;
    [SerializeField] private InputActionReference X;
    [SerializeField] private InputActionReference Y;

    [SerializeField] private InputActionReference LT;
    [SerializeField] private InputActionReference LG;
    [SerializeField] private InputActionReference RT;
    [SerializeField] private InputActionReference RG;

    [SerializeField] private BoardUI studentBoardUI;




    public Text message;
    public Text spellName;
    public bool isTwoHanded = false;

    private enum ButtonType
    {
        Primary,
        Secondary,
        Trigger,
        Grab,
        Unknown
    }

    class Spell
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

        public Boolean MatchesButtonSequence(List<ButtonType> buttonsPressedList)
        {
            if (buttonsPressedList.Count < 3)
            {
                return false;
            }
            return buttonsPressedList[0] == button1 && buttonsPressedList[1] == button2 && buttonsPressedList[2] == button3;
        }
    }

    private List<Spell> spellList = new List<Spell>()
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

    private List<ButtonType> buttonsPressedList = new List<ButtonType>();

    void Start()
    {
        A.action.started += OnAPressed;
        B.action.started += OnBPressed;
        X.action.started += OnXPressed;
        Y.action.started += OnYPressed;
        LT.action.started += onLTPressed;
        LG.action.started += onLGPressed;
        RT.action.started += onRTPressed;
        RG.action.started += onRGPressed;
    }

    private void OnButtonPressed(string button, InputActionReference pairedButton = null)
    {
        if (!isTwoHanded || (pairedButton != null && pairedButton.action != null && pairedButton.action.phase == InputActionPhase.Performed))
        {
            ButtonUpdate(button);
        }
    }

    private void OnAPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("A", X);
    }

    private void OnBPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("B", Y);
    }

    private void OnXPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("X", A);
    }

    private void OnYPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("Y", B);
    }

    private void onLTPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("LT", RT);
    }

    private void onLGPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("LG", RG);
    }

    private void onRTPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("RT", LT);
    }

    private void onRGPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("RG", LG);
    }

    private void ButtonUpdate(string button)
    {
        ButtonType mappedButton = MapButtonToType(button);

        buttonsPressedList.Add(mappedButton);
        message.text = button + " Pressed\r\n" + arrayListToString(buttonsPressedList);
        spellName.text = "";
        UpdateStudentBoardUI();

        if (buttonsPressedList.Count >= 3)
        {
            checkSpellList();
            Debug.Log(arrayListToString(buttonsPressedList));
            buttonsPressedList.Clear(); // Reuse the list instead of creating a new one
        }
    }

    private GlyphType MapButtonToGlyphType(ButtonType mappedButton)
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

    private ConnectorType MapButtonToConnectorType(ButtonType mappedButton)
    {
        return mappedButton switch
        {
            ButtonType.Primary => ConnectorType.Link,
            ButtonType.Secondary => ConnectorType.Weave,
            _ => ConnectorType.None
        };
    }

    private void UpdateStudentBoardUI()
    {
        GlyphType glyph1 = buttonsPressedList.Count > 0 ? MapButtonToGlyphType(buttonsPressedList[0]) : GlyphType.None;
        ConnectorType connector = buttonsPressedList.Count > 1 ? MapButtonToConnectorType(buttonsPressedList[1]) : ConnectorType.None;
        GlyphType glyph2 = buttonsPressedList.Count > 2 ? MapButtonToGlyphType(buttonsPressedList[2]) : GlyphType.None;

        Debug.Log("Glyph1: " + glyph1 + ", Connector: " + connector + ", Glyph2: " + glyph2);

        studentBoardUI.UpdateBoardUI(glyph1, connector, glyph2);
    }

    private void checkSpellList()
    {
        bool spellFound = false;
        foreach (var spell in spellList)
        {
            if (spell.MatchesButtonSequence(buttonsPressedList))
            {
                message.text += "\r\nSpell Cast: " + spell.getSpellName();
                spellName.text = "Spell Cast: " + spell.getSpellName();
                spellFound = true;
                break;
            }
        }
        if (!spellFound)
        {
            message.text += "\r\nInvalid Spell: " + arrayListToString(buttonsPressedList);
        }
    }



    private ButtonType MapButtonToType(string button)
    {
        if (buttonsPressedList.Count == 1)
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


    private string arrayListToString(List<ButtonType> list)
    {
        string result = "[";
        for (int i = 0; i < list.Count; i++)
        {
            result += list[i].ToString();
            if (i < list.Count - 1)
            {
                result += ", ";
            }
        }
        result += "]";
        return result;
    }


}
