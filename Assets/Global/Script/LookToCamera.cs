using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //transform.rotation.SetLookRotation(Camera.main.transform.position);
        transform.LookAt(Camera.main.transform.position);
    }
}
