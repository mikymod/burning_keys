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

    public static UnityEvent DesktopTaskFocus = new UnityEvent();
    public static UnityEvent DesktopTaskUnfocus = new UnityEvent();
    public static UnityEvent DesktopTaskFinished = new UnityEvent(); // end game
    #endregion

    public static int MailCounter;//needed for mail count info idk how to fix

    [Tooltip("Dopo quanto tempo avviene action")] public float MaxAlarmCounter, MaxMailCounter, MaxCallCounter;


    [SerializeField] private LayerMask mask;
    private float mailTimer, alarmTimer, callTimer;
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
            AlarmTaskStart.Invoke();
            print("Alarm Adverter");
        }
        if (mailTimer >= MaxMailCounter)
        {
            MailCounter++;
            mailTimer = 0;
            MailAdverterStart.Invoke();
            print("Mail Adverter");
        }
        if (callTimer >= MaxCallCounter)
        {
            callTimer = 0;
            PhoneAdverterEnd.Invoke();
            print("Phone Adverter");
        }

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, mask))
            {
                //Task switch
                switch (hit.collider.gameObject.tag)
                {
                    case "Mail":
                        MailTaskStart.Invoke();
                        break;
                    case "Phone":
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
