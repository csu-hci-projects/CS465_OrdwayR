using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Oculus.Interaction.Input;

public class ControllerActionInput : MonoBehaviour
{
    [SerializeField] public InputActionReference A;
    [SerializeField] public InputActionReference B;
    [SerializeField] public InputActionReference X;
    [SerializeField] public InputActionReference Y;

    [SerializeField] public InputActionReference LT;
    [SerializeField] public InputActionReference LG;
    [SerializeField] public InputActionReference RT;
    [SerializeField] public InputActionReference RG;

    [SerializeField] public Text message;

    [SerializeField] public Boolean twoHanded = false;

    private List<List<string>> spellList = new List<List<string>>(){
        new List<string>(){"Primary", "Trigger", "Trigger", "Spell 1"},
        new List<string>(){"Primary", "Trigger", "Grab", "Spell 2"},
        new List<string>(){"Primary", "Grab", "Trigger", "Spell 3"},
        new List<string>(){"Primary", "Grab", "Grab", "Spell 4"},
        new List<string>(){"Secondary", "Trigger", "Trigger", "Spell 5"},
        new List<string>(){"Secondary", "Trigger", "Grab", "Spell 6"},
        new List<string>(){"Secondary", "Grab", "Trigger", "Spell 7"},
        new List<string>(){"Secondary", "Grab", "Grab", "Spell 8"},
        new List<string>(){"Trigger", "Trigger", "Trigger", "Spell 9"},
        new List<string>(){"Trigger", "Trigger", "Grab", "Spell 10"},
        new List<string>(){"Trigger", "Grab", "Trigger", "Spell 11"},
        new List<string>(){"Trigger", "Grab", "Grab", "Spell 12"},
        new List<string>(){"Grab", "Trigger", "Trigger", "Spell 13"},
        new List<string>(){"Grab", "Trigger", "Grab", "Spell 14"},
        new List<string>(){"Grab", "Grab", "Trigger", "Spell 15"},
        new List<string>(){"Grab", "Grab", "Grab", "Spell 16"},
    };

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

        if (!twoHanded || (pairedButton != null && pairedButton.action != null && pairedButton.action.phase == InputActionPhase.Performed))
        {
            buttonUpdate(button);
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

    void onLTPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("LT", RT);
    }

    void onLGPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("LG", RG);
    }

    void onRTPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("RT", LT);
    }

    void onRGPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("RG", LG);
    }

    private List<string> buttonsPressedList = new List<string>();

    void buttonUpdate(string button)
    {
        button = getButtonType(button);

        buttonsPressedList.Add(button);
        message.text = button + " Pressed\r\n" + arrayListToString(buttonsPressedList);
        if (buttonsPressedList.Count >= 3)
        {
            checkSpellList();
            Debug.Log(arrayListToString(buttonsPressedList));
            buttonsPressedList.Clear();
        }
    }



    private void checkSpellList()
    {
        bool spellFound = false;
        foreach (var spell in spellList)
        {
            if (spell[0] == buttonsPressedList[0] &&
                spell[1] == buttonsPressedList[1] &&
                spell[2] == buttonsPressedList[2])
            {
                message.text += "\r\nSpell Cast: " + spell[3];
                spellFound = true;
                break;
            }
        }
        if (!spellFound)
        {
            message.text += "\r\nInvalid Spell: " + arrayListToString(buttonsPressedList);
        }
    }

    private static string getButtonType(string button)
    {
        switch (button)
        {
            case "A":
            case "X":
                return "Primary";
            case "B":
            case "Y":
                return "Secondary";
            case "LT":
            case "RT":
                return "Trigger";
            case "LG":
            case "RG":
                return "Grab";
            default:
                return button;
        }
    }

    private string arrayListToString(List<string> list)
    {
        string result = "[";
        for (int i = 0; i < list.Count; i++)
        {
            result += list[i];
            if (i < list.Count - 1)
            {
                result += ", ";
            }
        }
        result += "]";
        return result;
    }

    void Update()
    {

    }
}
