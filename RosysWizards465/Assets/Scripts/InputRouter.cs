using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputRouter : MonoBehaviour
{

    public enum Lessons
    {
        Input,
        Glyph,
        Spell,
        Test
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

        ControlSet mappedButton = ButtonMapping.MapButtonToSet(button);

        UIManager.setMessage(button);

        UIManager.studentInputUpdate(mappedButton, ButtonMapping.MapButtonToGlyphType(ButtonMapping.MapButtonToType(button)));


        // UIManager.setMessage(button + " Pressed\r\n" + ArrayHandler.arrayListToString(buttonsPressedList));
        // UIManager.setSpellName("");
        // UIManager.UpdateStudentBoardUI(buttonsPressedList);

        // if (buttonsPressedList.Count >= 3)
        // {
        //     bool isValidSpell = UIManager.isCorrectSpell(
        //         ButtonMapping.MapButtonToGlyphType(buttonsPressedList[0]),
        //         ButtonMapping.MapButtonToConnectorType(buttonsPressedList[1]),
        //        ButtonMapping.MapButtonToGlyphType(buttonsPressedList[2]));

        //     UIManager.checkSpellList(buttonsPressedList);
        //     StartCoroutine(CheckBoardsCoroutine(isValidSpell));

        //     Debug.Log(ArrayHandler.arrayListToString(buttonsPressedList));
        //     buttonsPressedList.Clear();
        // }
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
        ButtonType mappedButton = ButtonMapping.MapButtonToType(button, isConnector);

        buttonsPressedList.Add(mappedButton);
        UIManager.setMessage(button + " Pressed\r\n" + ArrayHandler.arrayListToString(buttonsPressedList));
        UIManager.setSpellName("");
        UIManager.studentGlyphUpdate(buttonsPressedList);

        if (buttonsPressedList.Count >= 3)
        {
            bool isValidSpell = UIManager.isCorrectSpell(
                ButtonMapping.MapButtonToGlyphType(buttonsPressedList[0]),
                ButtonMapping.MapButtonToConnectorType(buttonsPressedList[1]),
                ButtonMapping.MapButtonToGlyphType(buttonsPressedList[2]));

            UIManager.checkSpellList(buttonsPressedList);
            StartCoroutine(CheckBoardsCoroutine(isValidSpell));

            Debug.Log(ArrayHandler.arrayListToString(buttonsPressedList));
            buttonsPressedList.Clear();
        }
    }

    public IEnumerator CheckBoardsCoroutine(bool isValidSpell)
    {
        isBoardUpdating = true;
        UIManager.CheckBoards(isValidSpell);
        yield return new WaitForSeconds(3f);
        isBoardUpdating = false;
    }

    public void changeLesson(Lessons lesson)
    {
        currentLesson = lesson;
    }
}

