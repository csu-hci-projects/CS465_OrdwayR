using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputRouter : MonoBehaviour
{

    public bool inTestMode = false;
    private int InputToExit = 8;
    private int GlyphToExit = 8;
    private int SpellToExit = 16;
    private int lessonTestModifier = 8;

    public Lessons currentLesson = Lessons.Intro;

    public UIManager UIManager;
    private List<ButtonType> buttonsPressedList = new List<ButtonType>();

    public EnvironmentManager EnvironmentManager;

    public ControlType controlType = ControlType.ControllerOneHand;

    private bool isBoardUpdating = false;

    void Start()
    {
        if (inTestMode)
        {
            InputToExit = 8 / lessonTestModifier;
            GlyphToExit = 8 / lessonTestModifier;
            SpellToExit = 16 / lessonTestModifier;
        }
        controlType = GameSettings.Instance.controlType;
    }


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
        Debug.Log("Current Lesson: " + currentLesson);
        if (currentLesson == Lessons.SpellToExit)
        {
            currentLesson = Lessons.Intro;
            SceneManager.LoadScene(0);
        }
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


        Debug.Log("Switching Lesson To: " + currentLesson);

        switch (currentLesson)
        {
            case Lessons.SpellIntro:
                Debug.Log("Changing to Spell Intro");
                EnvironmentManager.PlayTableLift();
                EnvironmentManager.PlayChairMove();
                EnvironmentManager.PlayBoardGlyphToSpellIntro();
                break;

            case Lessons.Spell:
                Debug.Log("Changing to Spell Lesson");
                EnvironmentManager.PlayBoardSpellIntroToSpell();
                break;

            case Lessons.SpellToExit:
                Debug.Log("Changing to Exit");
                EnvironmentManager.PlayBoardSpellToExit();
                break;
        }

        UIManager.SetTopText("Lesson Progress: 0/16");


    }


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
            UIManager.SetTopText("Lesson Progress: " + correctValue + "/" + InputToExit);
        }
        UIManager.studentInputUpdate(mappedButton, ButtonMapping.MapButtonTypeToGlyphType(ButtonMapping.MapRawToButtonType(button)));
        StartCoroutine(CheckInputBoardsCoroutine(UIManager.isCorrectInput(mappedButton)));
    }

    public IEnumerator CheckInputBoardsCoroutine(bool isValidSpell)
    {
        if (correctValue >= InputToExit)
        {
            correctValue = 0;
            UIManager.CheckGlyphBoards(false);
            NextLesson();
        }
        isBoardUpdating = true;
        UIManager.CheckInputBoards(isValidSpell);
        yield return new WaitForSeconds(2f);
        isBoardUpdating = false;

    }

    private void TestLesson(string button)
    {

    }

    private void GlyphLesson(string button)
    {

        Debug.Log("Glyph Lesson: " + button);
        if (isBoardUpdating)
        {
            Debug.Log("Board is updating, ignoring button press.");
            return;
        }

        bool isConnector = buttonsPressedList.Count == 1;
        ButtonType mappedButton = ButtonMapping.MapRawToButtonType(button, isConnector);

        buttonsPressedList.Add(mappedButton);

        UIManager.studentGlyphUpdate(buttonsPressedList);

        if (buttonsPressedList.Count >= 3)
        {
            Debug.Log("Glyph Lesson: Checking spell: " + button);
            bool isValidSpell = UIManager.isCorrectGlyphSpell(
                ButtonMapping.MapButtonTypeToGlyphType(buttonsPressedList[0]),
                ButtonMapping.MapButtonToConnectorType(buttonsPressedList[1]),
                ButtonMapping.MapButtonTypeToGlyphType(buttonsPressedList[2]));

            Debug.Log("Glyph Lesson: isValidSpell: " + isValidSpell);

            if (isValidSpell)
            {
                correctValue++;
                UIManager.SetTopText("Lesson Progress: " + correctValue + "/" + GlyphToExit);
            }

            UIManager.checkSpellList(buttonsPressedList);
            StartCoroutine(CheckGlyphBoardsCoroutine(isValidSpell));

            Debug.Log(ArrayHandler.arrayListToString(buttonsPressedList));
            buttonsPressedList.Clear();
        }
    }

    public IEnumerator CheckGlyphBoardsCoroutine(bool isValidSpell)
    {
        if (correctValue >= GlyphToExit)
        {
            correctValue = 0;

            NextLesson();
        }
        isBoardUpdating = true;
        UIManager.CheckGlyphBoards(isValidSpell);
        yield return new WaitForSeconds(2f);
        isBoardUpdating = false;

    }

    private void SpellLesson(string button)
    {
        if (isBoardUpdating)
        {
            Debug.Log("Board is updating, ignoring button press.");
            return;
        }

        bool isConnector = buttonsPressedList.Count == 1;
        ButtonType mappedButton = ButtonMapping.MapRawToButtonType(button, isConnector);

        buttonsPressedList.Add(mappedButton);
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
                UIManager.SetTopText("Lesson Progress: " + correctValue + "/" + SpellToExit);
            }

            UIManager.checkSpellList(buttonsPressedList);
            StartCoroutine(CheckSpellsBoardsCoroutine(isValidSpell));

            Debug.Log(ArrayHandler.arrayListToString(buttonsPressedList));
            buttonsPressedList.Clear();
        }
    }

    public IEnumerator CheckSpellsBoardsCoroutine(bool isValidSpell)
    {
        isBoardUpdating = true;
        if (correctValue >= SpellToExit)
        {
            correctValue = 0;
            NextLesson();
        }
        UIManager.CheckGlyphBoards(isValidSpell);
        if (isValidSpell)
        {
            EnvironmentManager.PlayRandomMagicEffect();
        }
        yield return new WaitForSeconds(2f);
        isBoardUpdating = false;

    }



    public void changeLesson(Lessons lesson)
    {
        currentLesson = lesson;
    }
}

