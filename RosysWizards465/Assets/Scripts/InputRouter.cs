using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputRouter : MonoBehaviour
{

    public enum Lessons
    {
        Intro,
        Input,
        InputToGlyph,
        Glyph,
        GlyphToSpell,
        SpellIntro,
        Spell,
        SpellToExit,
        Test
    }

    void Update()
    {
        string layout = currentLesson switch
        {
            Lessons.Intro => "Intro",
            Lessons.Input => "InputLesson",
            Lessons.InputToGlyph => "InputToGlyph", 
            Lessons.Glyph => "GlyphLesson",
            Lessons.GlyphToSpell => "GlyphToSpell",
            Lessons.SpellIntro => "SpellIntro",
            Lessons.Spell => "SpellLesson",
            Lessons.SpellToExit => "SpellToExit",
            Lessons.Test => "Test",
            _ => ""
        };

        if (!string.IsNullOrEmpty(layout))
        {
            UIManager.SetUILayout(layout);
        }
    }



    public Lessons currentLesson = Lessons.Glyph;

    public UIManager UIManager;
    private List<ButtonType> buttonsPressedList = new List<ButtonType>();

    private bool isBoardUpdating = false;
    public void ButtonUpdate(string button)
    {
        switch (currentLesson)
        {
            case Lessons.Input:
                InputLesson(button);
                break;
            case Lessons.Glyph:
                GlyphLesson(button);
                break;
            case Lessons.Spell:
                SpellLesson(button);
                break;
            case Lessons.Test:
                TestLesson(button);
                break;
        }


    }

    private void InputLesson(string button)
    {
        if (isBoardUpdating)
        {
            Debug.Log("Board is updating, ignoring button press.");
            return;
        }

        ControlSet mappedButton = ButtonMapping.MapRawToControlSet(button);

        UIManager.setMessage(button);

        UIManager.studentInputUpdate(mappedButton, ButtonMapping.MapButtonTypeToGlyphType(ButtonMapping.MapRawToButtonType(button)));
        StartCoroutine(CheckInputBoardsCoroutine(UIManager.isCorrectInput(mappedButton)));
    }

    public IEnumerator CheckInputBoardsCoroutine(bool isValidSpell)
    {
        isBoardUpdating = true;
        UIManager.CheckInputBoards(isValidSpell);
        yield return new WaitForSeconds(2f);
        isBoardUpdating = false;
    }



    private void SpellLesson(string button)
    {

    }

    private void TestLesson(string button)
    {

    }

    private void GlyphLesson(string button)
    {
        if (isBoardUpdating)
        {
            Debug.Log("Board is updating, ignoring button press.");
            return;
        }

        bool isConnector = buttonsPressedList.Count == 1;
        ButtonType mappedButton = ButtonMapping.MapRawToButtonType(button, isConnector);

        buttonsPressedList.Add(mappedButton);
        UIManager.setMessage(button + " Pressed\r\n" + ArrayHandler.arrayListToString(buttonsPressedList));
        UIManager.setSpellName("");
        UIManager.studentGlyphUpdate(buttonsPressedList);

        if (buttonsPressedList.Count >= 3)
        {
            bool isValidSpell = UIManager.isCorrectSpell(
                ButtonMapping.MapButtonTypeToGlyphType(buttonsPressedList[0]),
                ButtonMapping.MapButtonToConnectorType(buttonsPressedList[1]),
                ButtonMapping.MapButtonTypeToGlyphType(buttonsPressedList[2]));

            UIManager.checkSpellList(buttonsPressedList);
            StartCoroutine(CheckGlyphBoardsCoroutine(isValidSpell));

            Debug.Log(ArrayHandler.arrayListToString(buttonsPressedList));
            buttonsPressedList.Clear();
        }
    }

    public IEnumerator CheckGlyphBoardsCoroutine(bool isValidSpell)
    {
        isBoardUpdating = true;
        UIManager.CheckGlyphBoards(isValidSpell);
        yield return new WaitForSeconds(3f);
        isBoardUpdating = false;
    }

    public void changeLesson(Lessons lesson)
    {
        currentLesson = lesson;
    }
}

