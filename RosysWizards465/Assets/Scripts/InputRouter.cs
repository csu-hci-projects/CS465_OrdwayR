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

    bool isTwoHandedController;
    private bool isBoardUpdating = false;

    void Start()
    {
        isTwoHandedController = GameSettings.Instance.controlType == ControlType.ControllerTwoHand;
        if (inTestMode)
        {
            InputToExit = 8 / lessonTestModifier;
            GlyphToExit = 8 / lessonTestModifier;
            SpellToExit = 16 / lessonTestModifier;
        }

        if (isTwoHandedController)
        {
            InputToExit = 4;
        }


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

        }


    }



    int correctCount = 0;
    private void InputLesson(string button)
    {
        UIManager.SetTopText("Lesson Progress: " + correctCount + "/" + InputToExit);
        if (isBoardUpdating)
        {
            Debug.Log("Board is updating, ignoring button press.");
            return;
        }

        //Mapping the button to ControlSet
        ControlSet mappedButton = ButtonMapping.MapRawToControlSet(button); //Falls apart for gesutures

        //Display the button on the UI
        UIManager.studentInputUpdate(mappedButton, ButtonMapping.MapButtonTypeToGlyphType(ButtonMapping.MapRawToButtonType(button)));

        //Check if the button is correct
        StartCoroutine(CheckInputBoardsCoroutine(UIManager.isCorrectInput(mappedButton)));
    }

    public IEnumerator CheckInputBoardsCoroutine(bool isValidSpell)
    {
        //Check if the button is correct
        if (isValidSpell)
        {
            correctCount++;
            UIManager.SetTopText("Lesson Progress: " + correctCount + "/" + InputToExit);
        }

        //Update the boards and showing color
        isBoardUpdating = true;
        UIManager.UpdateInputBoards(isValidSpell);
        yield return new WaitForSeconds(2f);
        isBoardUpdating = false;

        //Check if the lesson is complete
        if (correctCount >= InputToExit)
        {
            correctCount = 0;
            UIManager.CheckGlyphBoards(false);
            NextLesson();
        }

    }



    private void GlyphLesson(string button)
    {
        UIManager.SetTopText("Lesson Progress: " + correctCount + "/" + GlyphToExit);
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



            UIManager.checkSpellList(buttonsPressedList);
            StartCoroutine(CheckGlyphBoardsCoroutine(isValidSpell));

            Debug.Log(ArrayHandler.arrayListToString(buttonsPressedList));
            buttonsPressedList.Clear();
        }
    }

    public IEnumerator CheckGlyphBoardsCoroutine(bool isValidSpell)
    {

        if (isValidSpell)
        {
            correctCount++;
            UIManager.SetTopText("Lesson Progress: " + correctCount + "/" + GlyphToExit);
        }
        isBoardUpdating = true;
        UIManager.CheckGlyphBoards(isValidSpell);
        yield return new WaitForSeconds(2f);
        isBoardUpdating = false;
        if (correctCount >= GlyphToExit)
        {
            correctCount = 0;
            NextLesson();
        }
    }

    private void SpellLesson(string button)
    {
        UIManager.SetTopText("Lesson Progress: " + correctCount + "/" + SpellToExit);
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

            UIManager.checkSpellList(buttonsPressedList);
            StartCoroutine(CheckSpellsBoardsCoroutine(isValidSpell));

            Debug.Log(ArrayHandler.arrayListToString(buttonsPressedList));
            buttonsPressedList.Clear();
        }
    }

    public IEnumerator CheckSpellsBoardsCoroutine(bool isValidSpell)
    {
        isBoardUpdating = true;

        UIManager.CheckGlyphBoards(isValidSpell);
        if (isValidSpell)
        {
            UIManager.SetTopText("Lesson Progress: " + correctCount + "/" + SpellToExit);
            correctCount++;
            EnvironmentManager.PlayRandomMagicEffect();
        }
        yield return new WaitForSeconds(2f);
        isBoardUpdating = false;
        if (correctCount >= SpellToExit)
        {
            correctCount = 0;
            NextLesson();
        }
    }



    public void changeLesson(Lessons lesson)
    {
        currentLesson = lesson;
    }
}

