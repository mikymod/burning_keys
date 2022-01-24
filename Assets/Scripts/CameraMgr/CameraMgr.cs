using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    //lista di punti raggiungibili
    [SerializeField] private List<GameObject> cameraPos;
    [SerializeField] private KeyCode keyForBack;
    //vengono utilizzati come time/value per incrementare il tempo di lerp
    [SerializeField] [Tooltip("Più valore GRANDE più tempo impiega, mantnerlo sempre sopra 1!")] private float mailLerpTime, phoneLerpTime, desktopLerpTime;
    [SerializeField] private float visualSensitivity;

    //prendo il valore poco prima di applicare il lerp della camera, avviene durante LerpMyCamToMail()/LerpMyCamToPhone()
    //private Transform LateCameraPos; Non funziona!
    private bool mailLerp, phoneLerp, lateCamera, deskLerp, freeCamMove;
    private float yaw, pitch;
    private void OnEnable()
    {
        GameManager.PhoneTaskStart.AddListener(LerpMyCamToPhone);
        GameManager.MailTaskStart.AddListener(LerpMyCamToMail);
    }
    private void OnDisable()
    {
        GameManager.PhoneTaskStart.RemoveListener(LerpMyCamToPhone);
        GameManager.MailTaskStart.RemoveListener(LerpMyCamToMail);
    }
    private void Start()
    {
        //piccolo controllo per non far esplodere tutto in caso valore<=0
        if (mailLerpTime == 0) mailLerpTime = 1;
        if (phoneLerpTime == 0) phoneLerpTime = 1;
        if (desktopLerpTime == 0) desktopLerpTime = 1;
        //piccolo controllo se dovessimo dimenticarci di impostare il tasto
        if (keyForBack == KeyCode.None) keyForBack = KeyCode.Space;
        //controllo per visualSensitivit
        if (visualSensitivity == 0) visualSensitivity = 1;
        freeCamMove = true;
    }
    private void Update()
    {

        if (Input.GetKeyDown(keyForBack))
            lateCamera = true;
        if (lateCamera) //Lerp per tornare posizione iniziale
        {
            freeCamMove = false;
            mailLerp = false;
            phoneLerp = false;
            if (Camera.main.transform.position == cameraPos[2].transform.position)
            {
                freeCamMove = true;
                yaw = 0;
                pitch = 0;
                lateCamera = false;
            }
            //Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, LateCameraPos.transform.position, fractionReturn);
            //Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, LateCameraPos.transform.rotation, fractionReturn);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPos[2].transform.position, Time.deltaTime * desktopLerpTime);
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, cameraPos[2].transform.rotation, Time.deltaTime * desktopLerpTime);
        }

        //Lerp per le Mail
        if (mailLerp)
        {
            freeCamMove = false;
            if (Camera.main.transform.position == cameraPos[0].transform.position) mailLerp = false;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPos[0].transform.position, Time.deltaTime * phoneLerpTime);
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, cameraPos[0].transform.rotation, Time.deltaTime * phoneLerpTime);
        }

        //Lerp per il Phone
        if (phoneLerp)
        {
            freeCamMove = false;
            if (Camera.main.transform.position == cameraPos[1].transform.position) phoneLerp = false;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPos[1].transform.position, Time.deltaTime * phoneLerpTime);
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, cameraPos[1].transform.rotation, Time.deltaTime * phoneLerpTime);
        }

        if (deskLerp) { } //Da aggiungere il desktop

        //bozza camera move
        if (freeCamMove)
        {
            yaw += visualSensitivity * Input.GetAxis("Mouse X");
            pitch -= visualSensitivity * Input.GetAxis("Mouse Y");
            Camera.main.transform.eulerAngles = new Vector3(pitch, yaw, 0);
            //LateCameraPos = Camera.main.transform; //non capisco perchè non funzioni! aggiorna il valore anche se non entra nell'if!
        }
        
    }

    //Darei un ritocchino a questa cosa non amo dover mettere due metodi per fare praticamente la stessa cosa
    private void LerpMyCamToMail()
    {
        if (mailLerp) return;
        mailLerp = true;
    }
    private void LerpMyCamToPhone()
    {
        if (phoneLerp) return;
        phoneLerp = true;
    }
}
