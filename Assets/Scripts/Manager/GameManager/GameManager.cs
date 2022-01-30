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
    [HideInInspector] public bool done;
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
    public static UnityEvent MailSubTaskFinished = new UnityEvent();

    public static UnityEvent PhoneAdverterStart = new UnityEvent();
    public static UnityEvent PhoneAdverterEnd = new UnityEvent();
    public static UnityEvent PhoneTaskStart = new UnityEvent();
    public static UnityEvent PhoneTaskFinished = new UnityEvent();

    public static UnityEvent DesktopTaskFocus = new UnityEvent();
    public static UnityEvent DesktopTaskUnfocus = new UnityEvent();
    public static UnityEvent DesktopTaskFinished = new UnityEvent(); // end game: win
    public static UnityEvent StressBarFilled = new UnityEvent(); // end game: lose

    public static UnityEvent SpinnerTaskFocus = new UnityEvent();
    public static UnityEvent SpinnerTaskUnfocus = new UnityEvent();
    public static UnityEvent SpinnerTaskStart = new UnityEvent();
    public static UnityEvent SpinnerTaskFinished = new UnityEvent();

    #endregion

    public static int MailCounter;//needed for mail count info idk how to fix

    private bool phoneIsFocusable = false;
    private bool alarmIsFocusable = false;
    private bool mailIsFocusable = false;

    [SerializeField] private LayerMask mask;

    private float mailTimer, alarmTimer, callTimer;
    private float timer = 0;

    [SerializeField] TimedEvent[] timedEvents;
    private bool spinnerActive = false;
    private bool spinnerConsumed = false;

    private void OnEnable()
    {
        GameManager.AlarmAdverterStart.AddListener(OnAlarmAdverterStart);
        GameManager.PhoneAdverterStart.AddListener(OnPhoneAdverterStart);
        GameManager.MailAdverterStart.AddListener(OnMailAdverterStart);

        GameManager.AlarmAdverterEnd.AddListener(OnAlarmAdverterEnd);
        GameManager.PhoneAdverterEnd.AddListener(OnPhoneAdverterEnd);
        GameManager.MailAdverterEnd.AddListener(OnMailAdverterEnd);

        GameManager.SpinnerTaskStart.AddListener(OnSpinnerTaskStart);
        GameManager.SpinnerTaskFinished.AddListener(OnSpinnerTaskFinished);

        GameManager.DesktopTaskFinished.AddListener(GameWon);
        GameManager.StressBarFilled.AddListener(GameLost);
    }

    private void OnDisable()
    {
        GameManager.AlarmAdverterStart.RemoveListener(OnAlarmAdverterStart);
        GameManager.PhoneAdverterStart.RemoveListener(OnPhoneAdverterStart);
        GameManager.MailAdverterStart.RemoveListener(OnMailAdverterStart);

        GameManager.AlarmAdverterEnd.RemoveListener(OnAlarmAdverterEnd);
        GameManager.PhoneAdverterEnd.RemoveListener(OnPhoneAdverterEnd);
        GameManager.MailAdverterEnd.RemoveListener(OnMailAdverterEnd);
        
        GameManager.SpinnerTaskStart.RemoveListener(OnSpinnerTaskStart);
        GameManager.SpinnerTaskFinished.RemoveListener(OnSpinnerTaskFinished);

        GameManager.DesktopTaskFinished.RemoveListener(GameWon);
        GameManager.StressBarFilled.RemoveListener(GameLost);
    }

    private void OnAlarmAdverterStart() => alarmIsFocusable = true;
    private void OnPhoneAdverterStart() => phoneIsFocusable = true;
    private void OnMailAdverterStart() => mailIsFocusable = true;
    private void OnAlarmAdverterEnd() => alarmIsFocusable = false;
    private void OnPhoneAdverterEnd() => phoneIsFocusable = false;
    private void OnMailAdverterEnd() => mailIsFocusable = false;
    private void OnSpinnerTaskStart() => spinnerActive = true;
    private void OnSpinnerTaskFinished() 
    {
        spinnerConsumed = true;
        spinnerActive = false;
    } 

    private void GameWon() => Time.timeScale = 0f;
    private void GameLost() => Time.timeScale = 0f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // FIXME: Hack
        if (Time.timeScale == 0)
        {
            return;
        }

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
                    case "Alarm":
                        if (alarmIsFocusable && !spinnerActive)
                            AlarmTaskStart.Invoke();
                        break;
                    case "Mail":
                        if (mailIsFocusable && !spinnerActive)
                            MailTaskStart.Invoke();
                        break;
                    case "Phone":
                        if (phoneIsFocusable && !spinnerActive)
                            PhoneTaskStart.Invoke();
                        break;
                    case "Desktop":
                        if (!spinnerActive)
                            DesktopTaskFocus.Invoke();
                        break;
                    case "Spinner":
                        if (!spinnerActive && !spinnerConsumed)
                            SpinnerTaskStart.Invoke();
                        break;
                }
            }
        }
    }
}
