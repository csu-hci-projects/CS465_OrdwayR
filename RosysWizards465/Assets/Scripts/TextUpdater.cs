using System;
using System.Collections;
using System.Collections.Generic;
using Samples.Whisper;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    [SerializeField] private GameObject XRDeviceSimulator;


    void Start()
    {
        
    }
    public Transform cameraTransform; 
    public float orbitDistance = 2f; 
    public float orbitSpeed = 30f;    

    private float angle = 0f; 
    void Update()
    {
        Whisper whisperScript = FindObjectOfType<Whisper>();
        if (whisperScript != null)
        {

            if (whisperScript.message.text != null)
            {
                TextMeshPro textMesh = GetComponent<TextMeshPro>();
                textMesh.text = whisperScript.message.text;
            }
        }


        if (cameraTransform == null)
        {
            Debug.LogWarning("Camera Transform not assigned!");
            return;
        }

        // Increment the angle over time
        angle += orbitSpeed * Time.deltaTime;

 
        float radians = angle * Mathf.Deg2Rad;


        float x = cameraTransform.position.x + Mathf.Cos(radians) * orbitDistance;
        float z = cameraTransform.position.z + Mathf.Sin(radians) * orbitDistance;
        float y = cameraTransform.position.y; // Keep the same height

     
        transform.position = new Vector3(x, y, z);

  
        transform.LookAt(cameraTransform);
        transform.Rotate(0, 180, 0);

    }



    Transform FindChildByName(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            
            if (child.name == childName)
            {
                return child;
            }

           
            Transform found = FindChildByName(child, childName);
            if (found != null)
            {
                return found;
            }
        }

        return null; 
    }
}
