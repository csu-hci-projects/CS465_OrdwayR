using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRouter : MonoBehaviour
{

    public UIManager UIManager;
    private List<ButtonType> buttonsPressedList = new List<ButtonType>();

    private bool isBoardUpdating = false;
    public void ButtonUpdate(string button)
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
        UIManager.UpdateStudentBoardUI(buttonsPressedList);

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
}

