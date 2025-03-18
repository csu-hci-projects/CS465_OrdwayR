using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureReciverTest : MonoBehaviour
{

    [SerializeField] public Text message;



    void Rocker()
    {
        Debug.Log("Rocker gesture received!");
        message.text = "Rocker";
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
