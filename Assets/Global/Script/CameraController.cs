using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;

    [SerializeField] private float cameraRotateSpeed;
    [SerializeField] private float cameraMovementSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Rotate(new Vector3( 0, cameraRotateSpeed, 0));
        }
        if (Input.GetKey(KeyCode.E))
        {
            this.transform.Rotate(new Vector3(0, -cameraRotateSpeed, 0));
        }

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 localForward = camera.gameObject.transform.worldToLocalMatrix.MultiplyVector(camera.gameObject.transform.forward);
            this.transform.position += localForward * cameraMovementSpeed;
        }
    }
}
