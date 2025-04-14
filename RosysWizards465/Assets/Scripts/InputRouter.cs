using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void NextLesson()
    {
        currentLesson = currentLesson switch
        {
            Lessons.Intro => Lessons.Input,
            Lessons.Input => Lessons.InputToGlyph,
            Lessons.InputToGlyph => Lessons.Glyph,
            Lessons.Glyph => Lessons.GlyphToSpell,
            Lessons.GlyphToSpell => Lessons.SpellIntro,
            Lessons.SpellIntro => Lessons.Spell,
            Lessons.Spell => Lessons.SpellToExit,
            _ => currentLesson
        };

        if (currentLesson == Lessons.SpellToExit)
        {
            currentLesson = Lessons.Intro;
            SceneManager.LoadScene(0);
        }

    }


    public Lessons currentLesson = Lessons.Intro;

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

    int correctValue = 0;
    private void InputLesson(string button)
    {
        if (isBoardUpdating)
        {
            Debug.Log("Board is updating, ignoring button press.");
            return;
        }

        ControlSet mappedButton = ButtonMapping.MapRawToControlSet(button);

        // UIManager.setMessage(button);
        if (UIManager.isCorrectInput(mappedButton))
        {
            correctValue++;
            UIManager.SetTopText("Lesson Progress: " + correctValue + "/8");
        }
        UIManager.studentInputUpdate(mappedButton, ButtonMapping.MapButtonTypeToGlyphType(ButtonMapping.MapRawToButtonType(button)));
        StartCoroutine(CheckInputBoardsCoroutine(UIManager.isCorrectInput(mappedButton)));
    }

    public IEnumerator CheckInputBoardsCoroutine(bool isValidSpell)
    {
        isBoardUpdating = true;
        UIManager.CheckInputBoards(isValidSpell);
        yield return new WaitForSeconds(2f);
        isBoardUpdating = false;
        if (correctValue >= 8)
        {
            correctValue = 0;
            UIManager.CheckGlyphBoards(false);
            NextLesson();
        }
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
        // UIManager.setMessage(button + " Pressed\r\n" + ArrayHandler.arrayListToString(buttonsPressedList));
        // UIManager.setSpellName("");
        UIManager.studentGlyphUpdate(buttonsPressedList);

        if (buttonsPressedList.Count >= 3)
        {
            bool isValidSpell = UIManager.isCorrectSpell(
                ButtonMapping.MapButtonTypeToGlyphType(buttonsPressedList[0]),
                ButtonMapping.MapButtonToConnectorType(buttonsPressedList[1]),
                ButtonMapping.MapButtonTypeToGlyphType(buttonsPressedList[2]));

            if (isValidSpell)
            {
                correctValue++;
                UIManager.SetTopText("Lesson Progress: " + correctValue + "/12");
            }

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
        if (correctValue >= 3)
        {
            correctValue = 0;
            NextLesson();

        }
    }

    public void changeLesson(Lessons lesson)
    {
        currentLesson = lesson;
    }
}

