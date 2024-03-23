using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [Header("Camera Controller")]
    public Transform target;
    public float gap = 3f;
    public float rotspeed = 3f;
    float rotX, rotY;

    [Header("Camera Handling")]
    public float minVerAngle=-14f;
    public float maxVerAngle = 45f;
    public Vector2 framingBalance;

    public bool invertX, invertY;
    float invertXValue, invertYValue;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        invertXValue = (invertX) ? -1 : 1;
        invertYValue = (invertY) ? 1 : -1;

        rotX += Input.GetAxis("Mouse Y")*invertYValue * rotspeed;
        rotX = Mathf.Clamp(rotX, minVerAngle, maxVerAngle);
        rotY += Input.GetAxis("Mouse X")*invertXValue * rotspeed;

        var targetRotation=Quaternion.Euler(rotX, rotY, 0);

        var focusPos = target.position + new Vector3(framingBalance.x, framingBalance.y);

        transform.position = focusPos - targetRotation * new Vector3(0, 0, gap);
        transform.rotation=targetRotation;
    }

    public Quaternion flatRotation=> Quaternion.Euler(0, rotY, 0);
}
