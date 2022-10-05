using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;

    [SerializeField] private float cameraRotateSpeed;
    [SerializeField] private float cameraMovementSpeed;
    [SerializeField] private float movementArea;

    // Update is called once per frame
    void Update()
    {
        camera.transform.LookAt(this.transform.position);
        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Rotate(new Vector3( 0, cameraRotateSpeed, 0));
        }
        if (Input.GetKey(KeyCode.E))
        {
            this.transform.Rotate(new Vector3(0, -cameraRotateSpeed, 0));
        }

        Vector3 localForward = (transform.position - camera.transform.position);
        localForward.y = 0;
        localForward.Normalize();
        Vector3 left = Vector3.Cross(localForward, Vector3.up).normalized;
        Vector3 lastTransform = transform.position;

        if (Input.GetKey(KeyCode.W))
        { 
            this.transform.position += localForward * cameraMovementSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= localForward * cameraMovementSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position -= left * cameraMovementSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += left * cameraMovementSpeed;
        }

        if (transform.position.magnitude > movementArea)
        {
            transform.position = lastTransform;
        }

    }
}
