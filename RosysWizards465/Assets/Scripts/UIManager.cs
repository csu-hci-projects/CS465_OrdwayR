using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;
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

    }

    public void studentGlyphUpdate(List<ButtonType> buttonsPressedList)
    {

    }

    public void studentInputUpdate(ButtonType button)
    {

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
