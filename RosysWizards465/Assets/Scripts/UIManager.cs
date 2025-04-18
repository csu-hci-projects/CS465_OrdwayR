using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;
using System;
public class UIManager : MonoBehaviour
{
    [SerializeField] private BoardUI studentBoardUI;
    [SerializeField] private BoardUI teacherBoardUI;
    [SerializeField] private GameObject leftHandUI;

    public Text topText;

    public void SetTopText(string text)
    {
        topText.text = text;
    }

    public void CheckGlyphBoards(bool isValidSpell)
    {
        teacherBoardUI.CheckAndRandomizeGlyph(isValidSpell);
        studentBoardUI.ColorAndClear(isValidSpell);
    }

    public void UpdateInputBoards(bool isValidSpell)
    {
        teacherBoardUI.ChangeColorAndNext(isValidSpell);
        studentBoardUI.ColorAndClear(isValidSpell);
    }





    public bool isCorrectGlyphSpell(GlyphType primaryGlyph, ConnectorType connector, GlyphType secondaryGlyph)
    {

        return teacherBoardUI.isCorrectGlyphSpell(
                    primaryGlyph,
                    connector,
                    secondaryGlyph);

    }

    public bool isCorrectSpell(GlyphType primaryGlyph, ConnectorType connector, GlyphType secondaryGlyph)
    {

        return teacherBoardUI.isCorrectSpell(
                    primaryGlyph,
                    connector,
                    secondaryGlyph);

    }

    public bool isCorrectInput(ControlSet button)
    {
        return teacherBoardUI.isCorrectInput(button);
    }





    public void studentGlyphUpdate(List<ButtonType> buttonsPressedList)
    {
        GlyphType glyph1 = buttonsPressedList.Count > 0 ? ButtonMapping.MapButtonTypeToGlyphType(buttonsPressedList[0]) : GlyphType.None;
        ConnectorType connector = buttonsPressedList.Count > 1 ? ButtonMapping.MapButtonToConnectorType(buttonsPressedList[1]) : ConnectorType.None;
        GlyphType glyph2 = buttonsPressedList.Count > 2 ? ButtonMapping.MapButtonTypeToGlyphType(buttonsPressedList[2]) : GlyphType.None;

        Debug.Log("Glyph1: " + glyph1 + ", Connector: " + connector + ", Glyph2: " + glyph2);
        studentBoardUI.UpdateGlyphUI(glyph1, connector, glyph2);
    }


    public void studentInputUpdate(ControlSet button, GlyphType glyphType)
    {
        studentBoardUI.UpdateInputUI(button, glyphType);
    }

    public void checkSpellList(List<ButtonType> buttonsPressedList)
    {
        foreach (var spell in Spell.spellList)
        {
            if (spell.MatchesButtonSequence(buttonsPressedList))
            {
                break;
            }
        }
    }

    public void SetUILayout(string layout)
    {
        studentBoardUI.SetLayout(layout);
        teacherBoardUI.SetLayout(layout);

        if (layout is "Intro" or "InputToGlyph" or "GlyphToSpell" or "SpellIntro" or "SpellToExit")
        {
            leftHandUI.SetActive(true);
        }
        else if (layout is "InputLesson" or "GlyphLesson" or "SpellLesson")
        {
            leftHandUI.SetActive(false);
        }

    }



}
