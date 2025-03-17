using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomInputAction : MonoBehaviour
{
    [SerializeField] public InputActionReference rightHold;
    [SerializeField] public InputActionReference leftHold;
    [SerializeField] public MeshRenderer mesh;
    [SerializeField] public GameObject magicCircle;
    [SerializeField] private Button recordButton;
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
            Debug.Log("Right and Left are pressed");
            recordButton.onClick.Invoke();
        }
    }

    void onLeftHold(InputAction.CallbackContext context)
    {
        if (rightHold.action.triggered)
        {
            mesh.material.color = Color.red;
            magicCircle.SetActive(true);
            Debug.Log("Right and Left are pressed");
            recordButton.onClick.Invoke();
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
