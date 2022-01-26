using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float visualSensitivity = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector2 rotation;
    private void Update()
    { 

        rotation.x += visualSensitivity * Input.GetAxis("Mouse X");
        rotation.y += visualSensitivity * Input.GetAxis("Mouse Y");
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);
        Camera.main.transform.localRotation = xQuat * yQuat;
    }
}