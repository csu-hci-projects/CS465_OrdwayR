using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using NUnit.Framework;
using OVR.OpenVR;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;
using System;

public class ActionInput : MonoBehaviour
{
    [SerializeField] private InputActionReference A;
    [SerializeField] private InputActionReference B;
    [SerializeField] private InputActionReference X;
    [SerializeField] private InputActionReference Y;
    [SerializeField] private InputActionReference LT;
    [SerializeField] private InputActionReference LG;
    [SerializeField] private InputActionReference RT;
    [SerializeField] private InputActionReference RG;

    public InputRouter inputRouter;
    public bool isTwoHanded = false;

    void Start()
    {
        isTwoHanded = (GameSettings.Instance.controlType is ControlType.ControllerTwoHand or ControlType.GestureTwoHand) ? true : false;
        A.action.started += OnAPressed;
        B.action.started += OnBPressed;
        X.action.started += OnXPressed;
        Y.action.started += OnYPressed;
        LT.action.started += onLTPressed;
        LG.action.started += onLGPressed;
        RT.action.started += onRTPressed;
        RG.action.started += onRGPressed;
    }

    String currentRightHandAction = "";
    String currentLeftHandAction = "";

    private void OnButtonPressed(string button, InputActionReference pairedButton = null)
    {
        if (GameSettings.Instance.controlType is ControlType.GestureTwoHand /*or ControlType.GestureCombined /*USE THIS FOR MORE ACCURACY (HARDER)*/)
        {
            Debug.Log("Right Hand: " + currentRightHandAction + ", Left Hand: " + currentLeftHandAction);
            if (currentRightHandAction == currentLeftHandAction)
            {
                Debug.Log("Executing action for both hands: " + currentRightHandAction);
                inputRouter.ButtonUpdate(button);
            }

        }
        else
        {
            if (!isTwoHanded || (pairedButton != null && pairedButton.action != null && pairedButton.action.phase == InputActionPhase.Performed))
            {
                string correctedForTwoHanded = !isTwoHanded ? button : ButtonMapping.MapRawToRaw(button).ToString();
                inputRouter.ButtonUpdate(correctedForTwoHanded);
            }
        }
    }

    private void OnAPressed(InputAction.CallbackContext context)
    {
        Debug.Log("A button pressed");
        OnButtonPressed("A", X);
    }

    private void OnBPressed(InputAction.CallbackContext context)
    {
        Debug.Log("B button pressed");
        OnButtonPressed("B", Y);
    }

    private void OnXPressed(InputAction.CallbackContext context)
    {
        Debug.Log("X button pressed");
        OnButtonPressed("X", A);
    }

    private void OnYPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Y button pressed");
        OnButtonPressed("Y", B);
    }

    private void onLTPressed(InputAction.CallbackContext context)
    {
        Debug.Log("LT button pressed");
        OnButtonPressed("LT", RT);
    }

    private void onLGPressed(InputAction.CallbackContext context)
    {
        Debug.Log("LG button pressed");
        OnButtonPressed("LG", RG);
    }

    private void onRTPressed(InputAction.CallbackContext context)
    {
        Debug.Log("RT button pressed");
        OnButtonPressed("RT", LT);
    }

    private void onRGPressed(InputAction.CallbackContext context)
    {
        Debug.Log("RG button pressed");
        OnButtonPressed("RG", LG);
    }

    void RightGun()
    {
        Debug.Log("Executing RightGun action");
        currentRightHandAction = "Gun";
        OnButtonPressed("A", X);
    }
    void LeftGun()
    {
        Debug.Log("Executing LeftGun action");
        currentLeftHandAction = "Gun";
        OnButtonPressed("X", A);
    }

    void RightPeace()
    {
        Debug.Log("Executing RightPeace action");
        currentRightHandAction = "Peace";
        OnButtonPressed("B", Y);
    }
    void LeftPeace()
    {
        Debug.Log("Executing LeftPeace action");
        currentLeftHandAction = "Peace";
        OnButtonPressed("Y", B);
    }

    void RightRocker()
    {
        Debug.Log("Executing RightRocker action");
        currentRightHandAction = "Rocker";
        OnButtonPressed("RG", LG);
    }

    void LeftRocker()
    {
        Debug.Log("Executing LeftRocker action");
        currentLeftHandAction = "Rocker";
        OnButtonPressed("LG", RG);
    }

    void RightShakka()
    {
        Debug.Log("Executing RightShakka action");
        currentRightHandAction = "Shakka";
        OnButtonPressed("RT", LT);
    }
    void LeftShakka()
    {
        Debug.Log("Executing LeftShakka action");
        currentLeftHandAction = "Shakka";
        OnButtonPressed("LT", RT);
    }
    void RightGunEnd()
    {
        Debug.Log("Executing RightGunEnd action");
        currentRightHandAction = "";
    }
    void LeftGunEnd()
    {
        Debug.Log("Executing LeftGunEnd action");
        currentLeftHandAction = "";
    }
    void RightPeaceEnd()
    {
        Debug.Log("Executing RightPeaceEnd action");
        currentRightHandAction = "";
    }
    void LeftPeaceEnd()
    {
        Debug.Log("Executing LeftPeaceEnd action");
        currentLeftHandAction = "";
    }
    void RightRockerEnd()
    {
        Debug.Log("Executing RightRockerEnd action");
        currentRightHandAction = "";
    }
    void LeftRockerEnd()
    {
        Debug.Log("Executing LeftRockerEnd action");
        currentLeftHandAction = "";
    }
    void RightShakkaEnd()
    {
        Debug.Log("Executing RightShakkaEnd action");
        currentRightHandAction = "";
    }
    void LeftShakkaEnd()
    {
        Debug.Log("Executing LeftShakkaEnd action");
        currentLeftHandAction = "";
    }

    void RightClap()
    {
        Debug.Log("Executing RightClap action");
        currentRightHandAction = "Clap";
        OnButtonPressed("A", X);
    }
    // void LeftClap()
    // {
    //     Debug.Log("Executing LeftClap action");
    //     currentLeftHandAction = "Clap";
    //     OnButtonPressed("X", A);
    // }
    void RightClapEnd()
    {
        Debug.Log("Executing RightClapEnd action");
        currentRightHandAction = "";
    }
    void LeftClapEnd()
    {
        Debug.Log("Executing LeftClapEnd action");
        currentLeftHandAction = "";
    }
    void RightTriangle()
    {
        Debug.Log("Executing RightTriangle action");
        currentRightHandAction = "Triangle";
        OnButtonPressed("B", Y);
    }
    // void LeftTriangle()
    // {
    //     Debug.Log("Executing LeftTriangle action");
    //     currentLeftHandAction = "Triangle";
    //     OnButtonPressed("Y", B);
    // }
    void RightTriangleEnd()
    {
        Debug.Log("Executing RightTriangleEnd action");
        currentRightHandAction = "";
    }
    void LeftTriangleEnd()
    {
        Debug.Log("Executing LeftTriangleEnd action");
        currentLeftHandAction = "";
    }

    void RightCutesy()
    {
        Debug.Log("Executing RightCutey action");
        currentRightHandAction = "Cutey";
        OnButtonPressed("RG", LG);
    }
    // void LeftCutesy()
    // {
    //     Debug.Log("Executing LeftCutey action");
    //     currentLeftHandAction = "Cutey";
    //     OnButtonPressed("LG", RG);
    // }
    void RightCuteyEnd()
    {
        Debug.Log("Executing RightCuteyEnd action");
        currentRightHandAction = "";
    }
    void LeftCuteyEnd()
    {
        Debug.Log("Executing LeftCuteyEnd action");
        currentLeftHandAction = "";
    }

    void RightHeart()
    {
        Debug.Log("Executing RightHeart action");
        currentRightHandAction = "Heart";
        OnButtonPressed("RT", LT);
    }

    // void LeftHeart()
    // {
    //     Debug.Log("Executing LeftHeart action");
    //     currentLeftHandAction = "Heart";
    //     OnButtonPressed("LT", RT);
    // }
    void RightHeartEnd()
    {
        Debug.Log("Executing RightHeartEnd action");
        currentRightHandAction = "";
    }
    void LeftHeartEnd()
    {
        Debug.Log("Executing LeftHeartEnd action");
        currentLeftHandAction = "";
    }


}
