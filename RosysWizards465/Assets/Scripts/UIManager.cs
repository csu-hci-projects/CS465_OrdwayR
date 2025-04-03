using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class UIManager : MonoBehaviour
{
    [SerializeField] private BoardUI studentBoardUI;

    [SerializeField] private BoardUI teacherBoardUI;

    public Text message;
    public Text spellName;

    public void setMessage(string message)
    {
        this.message.text = message;
    }
    public void setSpellName(string spellName)
    {
        this.spellName.text = spellName;
    }


    public void CheckBoards(bool isValidSpell)
    {
        teacherBoardUI.CheckAndRandomize(isValidSpell);
        studentBoardUI.CheckAndClear(isValidSpell);
    }



    public bool isCorrectSpell(GlyphType primaryGlyph, ConnectorType connector, GlyphType secondaryGlyph)
    {

        return teacherBoardUI.isCorrectSpell(
                    primaryGlyph,
                    connector,
                    secondaryGlyph);

    }

    public void UpdateStudentBoardUI(List<ButtonType> buttonsPressedList)
    {
        GlyphType glyph1 = buttonsPressedList.Count > 0 ? ButtonMapping.MapButtonToGlyphType(buttonsPressedList[0]) : GlyphType.None;
        ConnectorType connector = buttonsPressedList.Count > 1 ? ButtonMapping.MapButtonToConnectorType(buttonsPressedList[1]) : ConnectorType.None;
        GlyphType glyph2 = buttonsPressedList.Count > 2 ? ButtonMapping.MapButtonToGlyphType(buttonsPressedList[2]) : GlyphType.None;

        Debug.Log("Glyph1: " + glyph1 + ", Connector: " + connector + ", Glyph2: " + glyph2);

        studentBoardUI.UpdateBoardUI(glyph1, connector, glyph2);
    }


    public void checkSpellList(List<ButtonType> buttonsPressedList)
    {
        bool spellFound = false;
        foreach (var spell in Spell.spellList)
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
            message.text += "\r\nInvalid Spell: " + ArrayHandler.arrayListToString(buttonsPressedList);
        }
    }




}
