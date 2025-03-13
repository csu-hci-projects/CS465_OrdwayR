using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRotation : MonoBehaviour
{
    public float rotationTime = 15f;

    void Update()
    {
        float rotationSpeed = 360f / rotationTime;


        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

}
