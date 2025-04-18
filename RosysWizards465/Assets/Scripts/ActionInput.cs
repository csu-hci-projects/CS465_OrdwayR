using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using NUnit.Framework;
using OVR.OpenVR;

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
        isTwoHanded = (GameSettings.Instance.controlType == ControlType.ControllerTwoHand) ? true : false;
        A.action.started += OnAPressed;
        B.action.started += OnBPressed;
        X.action.started += OnXPressed;
        Y.action.started += OnYPressed;
        LT.action.started += onLTPressed;
        LG.action.started += onLGPressed;
        RT.action.started += onRTPressed;
        RG.action.started += onRGPressed;
    }

    private void OnButtonPressed(string button, InputActionReference pairedButton = null)
    {
        if (!isTwoHanded || (pairedButton != null && pairedButton.action != null && pairedButton.action.phase == InputActionPhase.Performed))
        {
            inputRouter.ButtonUpdate(button);
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
        OnButtonPressed("A", X);
    }
    void LeftGun()
    {
        Debug.Log("Executing LeftGun action");
        OnButtonPressed("X", A);
    }

    void RightPeace()
    {
        Debug.Log("Executing RightPeace action");
        OnButtonPressed("B", Y);
    }
    void LeftPeace()
    {
        Debug.Log("Executing LeftPeace action");
        OnButtonPressed("Y", B);
    }

    void RightRocker()
    {
        Debug.Log("Executing RightRocker action");
        OnButtonPressed("RG", LG);
    }

    void LeftRocker()
    {
        Debug.Log("Executing LeftRocker action");
        OnButtonPressed("LG", RG);
    }

    void RightShakka()
    {
        Debug.Log("Executing RightShakka action");
        OnButtonPressed("RT", LT);
    }
    void LeftShakka()
    {
        Debug.Log("Executing LeftShakka action");
        OnButtonPressed("LT", RT);
    }

    void RightFist()
    {
        Debug.Log("Executing RightFist action");
    }
    void LeftFist()
    {
        Debug.Log("Executing LeftFist action");
    }
}
