using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region EventsMgr
    public static UnityEvent AlarmAdverterStart = new UnityEvent();
    public static UnityEvent AlarmAdverterEnd = new UnityEvent();
    public static UnityEvent AlarmTaskStart = new UnityEvent();
    public static UnityEvent AlarmTaskFinished = new UnityEvent();

    public static UnityEvent MailAdverterStart = new UnityEvent();
    public static UnityEvent MailAdverterEnd = new UnityEvent();
    public static UnityEvent MailTaskStart = new UnityEvent();
    public static UnityEvent MailTaskFinished = new UnityEvent();

    public static UnityEvent PhoneAdverterStart = new UnityEvent();
    public static UnityEvent PhoneAdverterEnd = new UnityEvent();
    public static UnityEvent PhoneTaskStart = new UnityEvent();
    public static UnityEvent PhoneTaskFinished = new UnityEvent();
    #endregion

    public static int MailCounter;//needed for mail count info idk how to fix

    [Tooltip("Dopo quanto tempo avviene action")] public float MaxAlarmCounter, MaxMailCounter, MaxCallCounter;


    [SerializeField] private LayerMask mask;
    private float mailTimer, alarmTimer, callTimer;

    private void OnEnable()
    {
        AlarmAdverterEnd.AddListener(alarmTimerStop);
        AlarmAdverterEnd.AddListener(callTimerStop);
        AlarmAdverterEnd.AddListener(mailTimerStop);
    }

    private void OnDisable()
    {
        AlarmAdverterEnd.RemoveListener(alarmTimerStop);
        AlarmAdverterEnd.RemoveListener(callTimerStop);
        AlarmAdverterEnd.RemoveListener(mailTimerStop);
    }


    private void Update()
    {
        //Time Gestrue need a Fix
        mailTimer += Time.deltaTime;
        alarmTimer += Time.deltaTime;
        callTimer += Time.deltaTime;
        if (alarmTimer >= MaxAlarmCounter)
        {
            alarmTimer = 0;
            AlarmAdverterStart.Invoke();
            print("Alarm Adverter");
        }
        if (mailTimer >= MaxMailCounter)
        {
            mailTimer = 0;
            MailCounter++;            
            MailAdverterStart.Invoke();
            print("Mail Adverter");
        }
        if (callTimer >= MaxCallCounter)
        {
            callTimer = 0;
            PhoneAdverterStart.Invoke();
            print("Phone Adverter");
        }


        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, mask))
            {
                // TODO: Select Game object with task
                //Camera in

                //Task switch
                switch (hit.collider.gameObject.tag)
                {
                    case "Alarm":
                        AlarmTaskStart.Invoke();
                        break;
                    case "Mail":
                        MailTaskStart.Invoke();
                        break;
                    case "Phone":
                        PhoneTaskStart.Invoke();
                        break;

                    default:
                        break;
                }
            }
        }
    }

    void alarmTimerStop()
    {
        alarmTimer = 0;
    }

    void callTimerStop()
    {
        callTimer = 0;
    }

    void mailTimerStop()
    {
        mailTimer = 0;
    }
}
