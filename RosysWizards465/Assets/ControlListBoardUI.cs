using System.Collections.Generic;
using UnityEngine;

public class ControlListBoardUI : MonoBehaviour
{
    public List<GameObject> controlListUI = new List<GameObject>();
    void Start()
    {
        foreach (var control in controlListUI)
        {
            control.SetActive(false);
        }
        int controlTypeIndex = (int)GameSettings.Instance.controlType;
        if (controlTypeIndex >= 0 && controlTypeIndex < controlListUI.Count)
        {
            controlListUI[controlTypeIndex].SetActive(true);
        }
    }
}
