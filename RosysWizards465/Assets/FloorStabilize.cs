using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorStabilize : MonoBehaviour
{

    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        transform.SetPositionAndRotation(new Vector3(camera.transform.position.x, transform.position.y, camera.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
    }
}
