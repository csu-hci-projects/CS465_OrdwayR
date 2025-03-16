using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomInputAction : MonoBehaviour
{
    public InputActionReference rightHold;
    public InputActionReference leftHold;
    public MeshRenderer mesh;
    public GameObject magicCircle;
    // Start is called before the first frame update
    void Start()
    {
        rightHold.action.performed += onRightHold;
        leftHold.action.performed += onLeftHold;
        rightHold.action.canceled += onRelease;
        leftHold.action.canceled += onRelease;
    }

    void onRightHold(InputAction.CallbackContext context)
    {
        if (leftHold.action.triggered)
        {
            mesh.material.color = Color.red;
            magicCircle.SetActive(true);
        }
    }

    void onLeftHold(InputAction.CallbackContext context)
    {
        if (rightHold.action.triggered)
        {
            mesh.material.color = Color.red;
            magicCircle.SetActive(true);
        }
    }

    void onRelease(InputAction.CallbackContext context)
    {
        if (!rightHold.action.triggered && !leftHold.action.triggered)
        {
            mesh.material.color = Color.gray;
            magicCircle.SetActive(false);
        }
    }


    void Update()
    {

    }
}
