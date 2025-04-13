using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorStabilize : MonoBehaviour
{

    public GameObject targetCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(new Vector3(targetCamera.transform.position.x, transform.position.y, targetCamera.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
    }
}
