using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


public class MailGame : MonoBehaviour
{
    #region Una prova
    //    GameObject obj;
    //    Collider objCollider;

    //    Camera cam;
    //    Plane[] planes;
    //    void Start()
    //    {
    //        cam = Camera.main;
    //        planes = GeometryUtility.CalculateFrustumPlanes(cam);
    //        objCollider = GetComponent<Collider>();
    //    }

    //    void Update()
    //    {
    //        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
    //        {
    //            Debug.Log(obj.name + " has been detected!");
    //        }
    //        else
    //        {
    //            Debug.Log("Nothing has been detected");
    //        }
    //    } 
    #endregion
    [HideInInspector] public static UnityEvent<int> MailTime = new UnityEvent<int>();
    [HideInInspector] public static UnityEvent<int> EndThisMail = new UnityEvent<int>();
    public float TimeToMail;
    public TMP_Text[] TextGO;
    public int ValueOfProgress, MaxScore;
    public Camera MailGameCamera;
    public Camera MainCamera;
    public bool isActive;

    private string[] worlds;
    private int mailNumber;
    private int wordCurrentIndex = 0;
    private float time;
    private int keyOrder;
    private int myProgression;

    private void Start()
    {
        time = TimeToMail;
        worlds = new string[TextGO.Length];
        NewChars();
    }
    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            mailNumber++;
            time = TimeToMail;
            MailTime.Invoke(mailNumber);
        }
        if (isActive && Camera.main == MainCamera)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, LayerMask.NameToLayer("Mail")))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (!MailGameCamera.isActiveAndEnabled)
                    {
                        MailGameCamera.transform.gameObject.SetActive(true);
                        MainCamera.transform.gameObject.SetActive(false);
                    }
                }
            }
        }
        if (MailGameCamera.isActiveAndEnabled && mailNumber != 0)
            MiniGameStart();
    }
    private void NewChars()
    {
        for (int i = 0; i < worlds.Length; i++)
        {
            worlds[i] = ((char)Random.Range(65, 91)).ToString();
            TextGO[i].text = worlds[i];
        }
    }
    private void MiniGameStart()
    {
        KeyCode currentKeyCode_1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), worlds[0][wordCurrentIndex].ToString());
        KeyCode currentKeyCode_2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), worlds[1][wordCurrentIndex].ToString());
        if (keyOrder == 0 && Input.GetKeyDown(currentKeyCode_1))
        {
            Debug.Log("appooo  " + keyOrder);
            keyOrder++;
        }
        if (keyOrder == 1 && Input.GetKeyDown(currentKeyCode_2))
        {
            keyOrder--;
            myProgression += ValueOfProgress;
            Debug.Log("stai una favola  " + myProgression);
        }

        if (myProgression >= MaxScore)
        {
            myProgression = 0;
            mailNumber--;
            EndThisMail.Invoke(mailNumber);
            if (mailNumber != 0)
                NewChars();
        }
    }

    private void OnEnable()
    {
        TaskManager.TaskStarted.AddListener(OnTaskStartedCallback);
        TaskManager.TaskFinished.AddListener(OnTaskFinishedCallback);
    }
    private void OnDisable()
    {
        TaskManager.TaskStarted.RemoveListener(OnTaskStartedCallback);
        TaskManager.TaskFinished.RemoveListener(OnTaskFinishedCallback);
    }


    private void OnTaskStartedCallback(TaskManager.TaskType taskType)
    {
        isActive = true;
    }
    private void OnTaskFinishedCallback(TaskManager.TaskType taskType)
    {
        isActive = false;
    }
}

