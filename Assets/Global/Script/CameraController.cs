using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;

    [SerializeField] 
    private float cameraRotateSpeed;

    [SerializeField]
    [Range(0,360)]
    private float cameraRotateAngle = 30;

    [SerializeField] 
    private float cameraMovementSpeed;

    [SerializeField]
    private Vector2 cameraLimitX;

    [SerializeField]
    private Vector2 cameraLimitZ;

    private Quaternion beginRot;
    private Quaternion endRot;

    bool isRotation = false;
    float rotationTimeCount = 0;

    private void Start()
    {
        endRot = beginRot = transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        CameraMovement();

        CameraRotation();

    }

    private void CameraRotation()
    {
        void QWRot(int rotAxis)
        {
            if (isRotation) return;

            isRotation = true;
            rotationTimeCount = 0;
            beginRot = transform.rotation;
            endRot = Quaternion.Euler(0, transform.rotation.eulerAngles.y + (cameraRotateAngle * rotAxis), 0);

            //Debug.Log(endRot.eulerAngles);
        }

        if (Input.GetKeyDown(KeyCode.Q)) QWRot(-1);
        if (Input.GetKeyDown(KeyCode.E)) QWRot(1);

        float interpolation = cameraRotateSpeed * rotationTimeCount;

        if (interpolation > 1)
        {
            isRotation = false;

            transform.rotation = endRot;

        }
        else
        {
            transform.rotation = Quaternion.Lerp(beginRot, endRot, interpolation);

            rotationTimeCount += Time.deltaTime;
        }
    }

    private void CameraMovement()
    {
        float axisY = Input.GetAxis("Vertical");
        float axisX = Input.GetAxis("Horizontal");

        Vector3 localForward = (transform.position - camera.transform.position);
        localForward.y = 0;
        localForward.Normalize();
        Vector3 left = Vector3.Cross(localForward, Vector3.up).normalized;        

        transform.position += localForward * cameraMovementSpeed * Time.deltaTime * axisY;

        transform.position -= left * cameraMovementSpeed * Time.deltaTime * axisX;

        CameraLimitation();
    }

    private void CameraLimitation()
    {
        float xPos = Mathf.Clamp(transform.position.x, cameraLimitX.x, cameraLimitX.y);

        float zPos = Mathf.Clamp(transform.position.z, cameraLimitZ.x, cameraLimitZ.y);

        transform.position = new Vector3(xPos, transform.position.y, zPos);
    }
}
