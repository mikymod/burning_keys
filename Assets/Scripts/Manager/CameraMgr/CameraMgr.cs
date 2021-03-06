using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraMgr : MonoBehaviour
{
    //lista di punti raggiungibili
    [SerializeField] private List<Transform> cameraPos;
    [SerializeField] private Transform sitCamera;
    [SerializeField] private Transform desktopCamera;
    [SerializeField] private Transform phoneCamera;
    [SerializeField] private Transform emailCamera;
    [SerializeField] private Transform spinnerCamera;

    [SerializeField] private KeyCode keyForBack;
    //vengono utilizzati come time/value per incrementare il tempo di lerp
    [SerializeField] [Range(0f, 1f)] private float speed;
    [SerializeField] private float visualSensitivity;


    [SerializeField] private GameObject zoomOut;

    //prendo il valore poco prima di applicare il lerp della camera, avviene durante LerpMyCamToMail()/LerpMyCamToPhone()
    //private Transform LateCameraPos; Non funziona!
    private float yaw, pitch;

    private bool freeCamMove = true;
    public static bool moving = false;


    private void OnEnable()
    {
        GameManager.PhoneTaskStart.AddListener(LerpMyCamToPhone);
        GameManager.PhoneTaskFinished.AddListener(LerpMyCamToSit);

        GameManager.MailTaskStart.AddListener(LerpMyCamToMail);
        GameManager.MailTaskFinished.AddListener(LerpMyCamToSit);

        GameManager.DesktopTaskFocus.AddListener(LerpMyCamToDesktop);
        GameManager.DesktopTaskUnfocus.AddListener(LerpMyCamToSit);
    }


    private void OnDisable()
    {
        GameManager.PhoneTaskStart.RemoveListener(LerpMyCamToPhone);
        GameManager.PhoneTaskFinished.RemoveListener(LerpMyCamToSit);

        GameManager.MailTaskFinished.RemoveListener(LerpMyCamToSit);
        GameManager.MailTaskStart.RemoveListener(LerpMyCamToMail);

        GameManager.DesktopTaskFocus.RemoveListener(LerpMyCamToDesktop);
        GameManager.DesktopTaskUnfocus.RemoveListener(LerpMyCamToSit);
    }

    Vector2 rotation;
    private void Update()
    {
        // FIXME: Hack
        if (Time.timeScale == 0)
        {
            return;
        }


        if (!freeCamMove)
        {
            return;
        }

        if (zoomOut != null && zoomOut.activeInHierarchy) zoomOut.SetActive(false);

        rotation.y -= visualSensitivity * Input.GetAxis("Mouse Y");
        rotation.x += visualSensitivity * Input.GetAxis("Mouse X");
        Quaternion xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);
        Camera.main.transform.localRotation = xQuat * ClampRotationAroundXAxis(yQuat) /*xQuat * yQuat*/;
        Camera.main.transform.rotation *= Quaternion.Euler(0, -180, 0);
    }
    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, -50, 50);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
    //Darei un ritocchino a questa cosa non amo dover mettere due metodi per fare praticamente la stessa cosa
    private void LerpMyCamToDesktop()
    {
        if (moving) return;
        freeCamMove = false;
        if (zoomOut != null) zoomOut.SetActive(true);
        StartCoroutine(MoveCameraCoroutine(desktopCamera.position, desktopCamera.rotation, speed));
    }

    private void LerpMyCamToMail()
    {
        if (moving) return;
        freeCamMove = false;
        if (zoomOut != null) zoomOut.SetActive(true);
        StartCoroutine(MoveCameraCoroutine(emailCamera.position, emailCamera.rotation, speed));
    }
    private void LerpMyCamToPhone()
    {
        if (moving) return;
        freeCamMove = false;
        StartCoroutine(MoveCameraCoroutine(phoneCamera.position, phoneCamera.rotation, speed));
    }

    private void LerpMyCamToSit()
    {
        if (moving) return;
        freeCamMove = true;
        StartCoroutine(MoveCameraCoroutine(sitCamera.position, sitCamera.rotation, speed));
    }

    private void LerpMyCamToSpinner()
    {
        if (moving) return;
        freeCamMove = false;
        StartCoroutine(MoveCameraCoroutine(spinnerCamera.position, spinnerCamera.rotation, speed));
    }

    private void MoveCamera(Vector3 position, Quaternion rotation, float speed)
    {
        StartCoroutine(MoveCameraCoroutine(position, rotation, speed));
    }

    private IEnumerator MoveCameraCoroutine(Vector3 position, Quaternion rotation, float speed)
    {
        moving = true;
        while (Vector3.Distance(Camera.main.transform.position, position) > 0.01f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, position, speed);
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, rotation, speed);
            yield return null;
        }

        Camera.main.transform.position = position;
        moving = false;
    }
}
