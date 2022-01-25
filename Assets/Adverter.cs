using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Adverter : MonoBehaviour
{
    [SerializeField] private Image phoneAdverter;
    [SerializeField] private Image alarmAdverter;
    [SerializeField] private Image mailAdverter;
    [SerializeField] private TMP_Text mailCounter;

    private void Start()
    {
        phoneAdverter.enabled = false;
        alarmAdverter.enabled = false;
        mailAdverter.enabled = false;
        mailCounter.enabled = false;
    }

    private void OnEnable()
    {
        GameManager.PhoneAdverterStart.AddListener(OnPhoneStart);
        GameManager.PhoneAdverterEnd.AddListener(OnPhoneEnd);

        GameManager.AlarmAdverterStart.AddListener(OnAlarmStart);
        GameManager.AlarmAdverterEnd.AddListener(OnAlarmEnd);

        GameManager.MailAdverterStart.AddListener(OnMailArrived);
        GameManager.MailAdverterEnd.AddListener(OnMailDismissed);
    }
    private void OnDisable()
    {
        GameManager.PhoneAdverterStart.RemoveListener(OnPhoneStart);
        GameManager.PhoneAdverterEnd.RemoveListener(OnPhoneEnd);
        
        GameManager.AlarmAdverterStart.RemoveListener(OnAlarmStart);
        GameManager.AlarmAdverterEnd.RemoveListener(OnAlarmEnd);
        
        GameManager.MailAdverterStart.RemoveListener(OnMailArrived);
        GameManager.MailAdverterEnd.RemoveListener(OnMailDismissed);
    }

    private void OnPhoneStart()
    {
        phoneAdverter.enabled = true;
    }

    private void OnPhoneEnd()
    {
        phoneAdverter.enabled = false;
    }

    private void OnAlarmStart()
    {
        alarmAdverter.enabled = true;
    }

    private void OnAlarmEnd()
    {
        alarmAdverter.enabled = false;
    }

    private void OnMailArrived()
    {
        mailAdverter.enabled = true;
        mailCounter.enabled = true;
        mailCounter.SetText($"{GameManager.MailCounter}");
    }

    private void OnMailDismissed()
    {
        if (GameManager.MailCounter == 0)
        {
            mailAdverter.enabled = false;
            mailCounter.enabled = false;
            mailCounter.SetText($"");
        }
        else
            mailCounter.SetText($"{ GameManager.MailCounter}");
    }
}
