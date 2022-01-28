using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TimedEvent
{
    public enum TimedEventType
    {
        Alarm,
        Email,
        Phone
    }

    public TimedEventType type;
    public float time;
    [HideInInspector]public bool done;
}

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

    public static UnityEvent DesktopTaskFocus = new UnityEvent();
    public static UnityEvent DesktopTaskUnfocus = new UnityEvent();
    public static UnityEvent DesktopTaskFinished = new UnityEvent(); // end game: win
    public static UnityEvent StressBarFilled = new UnityEvent(); // end game: lose
    #endregion

    public static int MailCounter;//needed for mail count info idk how to fix

    private bool phoneIsFocusable = false;
    private bool alarmIsFocusable = false;
    private bool mailIsFocusable = false;

    [SerializeField] private LayerMask mask;

    private float mailTimer, alarmTimer, callTimer;
    private float timer = 0;

    [SerializeField] TimedEvent[] timedEvents;

    private void OnEnable()
    {
        AlarmAdverterStart.AddListener(OnAlarmAdverterStart);
        PhoneAdverterStart.AddListener(OnPhoneAdverterStart);
        MailAdverterStart.AddListener(OnMailAdverterStart);

        AlarmAdverterEnd.AddListener(OnAlarmAdverterEnd);
        PhoneAdverterEnd.AddListener(OnPhoneAdverterEnd);
        MailAdverterEnd.AddListener(OnMailAdverterEnd);

        GameManager.DesktopTaskFinished.AddListener(GameWon);
        GameManager.StressBarFilled.AddListener(GameLost);
    }

    private void OnDisable()
    {
        AlarmAdverterStart.RemoveListener(OnAlarmAdverterStart);
        PhoneAdverterStart.RemoveListener(OnPhoneAdverterStart);
        MailAdverterStart.RemoveListener(OnMailAdverterStart);

        AlarmAdverterEnd.RemoveListener(OnAlarmAdverterEnd);
        PhoneAdverterEnd.RemoveListener(OnPhoneAdverterEnd);
        MailAdverterEnd.RemoveListener(OnMailAdverterEnd);
        
        GameManager.DesktopTaskFinished.RemoveListener(GameWon);
        GameManager.StressBarFilled.RemoveListener(GameLost);
    }

    private void OnAlarmAdverterStart()
    {
        alarmIsFocusable = true;
    }

    private void OnPhoneAdverterStart()
    {
        phoneIsFocusable = true;
    }

    private void OnMailAdverterStart()
    {
        mailIsFocusable = true;
    }

    private void OnAlarmAdverterEnd()
    {
        // alarmTimer = 0;
        alarmIsFocusable = false;
    }

    private void OnPhoneAdverterEnd()
    {
        // callTimer = 0;
        phoneIsFocusable = false;
    }

    private void OnMailAdverterEnd()
    {
        // mailTimer = 0;
        mailIsFocusable = false;
    }

    private void GameWon()
    {
        Time.timeScale = 0f;
    }

    private void GameLost()
    {
        Time.timeScale = 0f;
    }

    private void Start()
    {
    	Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;  
    }

    private void Update()
    {
        timer += Time.deltaTime;

        foreach (var timedEvent in timedEvents)
        {
            if (timer >= timedEvent.time && !timedEvent.done)
            {
                timedEvent.done = true;
                
                switch (timedEvent.type)
                {
                    case TimedEvent.TimedEventType.Alarm:
                        AlarmAdverterStart.Invoke();
                        AlarmTaskStart.Invoke();
                        break;
                    case TimedEvent.TimedEventType.Email:
                        MailAdverterStart.Invoke();
                        break;
                    case TimedEvent.TimedEventType.Phone:
                        PhoneAdverterStart.Invoke();
                        break;
                }
            }
        }

        RaycastInterctable();
    }

    private void RaycastInterctable()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * 1000f, Color.red);
            if (Physics.Raycast(ray, out RaycastHit hit, mask))
            {
                //Task switch
                switch (hit.collider.gameObject.tag)
                {
                    case "Mail":
                        if (mailIsFocusable)
                            MailTaskStart.Invoke();
                        break;
                    case "Phone":
                        if (phoneIsFocusable)
                            PhoneTaskStart.Invoke();
                        break;
                    case "Desktop":
                        DesktopTaskFocus.Invoke();
                        break;
                }
            }
        }
    }   
}
