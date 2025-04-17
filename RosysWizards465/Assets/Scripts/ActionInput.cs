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
        OnButtonPressed("A", X);
    }

    private void OnBPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("B", Y);
    }

    private void OnXPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("X", A);
    }

    private void OnYPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("Y", B);
    }

    private void onLTPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("LT", RT);
    }

    private void onLGPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("LG", RG);
    }

    private void onRTPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("RT", LT);
    }

    private void onRGPressed(InputAction.CallbackContext context)
    {
        OnButtonPressed("RG", LG);
    }

    void RightRocker()
    {
        Debug.Log("Right Rocker gesture received!");
        OnButtonPressed("A", X);
    }

    void LeftRocker()
    {
        Debug.Log("Left Rocker gesture received!");
    }
    void RightPeach()
    {
        Debug.Log("Right Peach gesture received!");
    }
    void LeftPeach()
    {
        Debug.Log("Left Peach gesture received!");
    }
    void RightFist()
    {
        Debug.Log("Right Fist gesture received!");
    }
    void LeftFist()
    {
        Debug.Log("Left Fist gesture received!");
    }
    void RightShake()
    {
        Debug.Log("Right Shake gesture received!");
    }
    void LeftShake()
    {
        Debug.Log("Left Shake gesture received!");
    }

    void RightOK()
    {
        Debug.Log("Right OK gesture received!");
    }
    void LeftOK()
    {
        Debug.Log("Left OK gesture received!");
    }



















}
