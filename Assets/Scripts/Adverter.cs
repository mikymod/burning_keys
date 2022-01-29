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
        //phoneAdverter.enabled = false;
        //alarmAdverter.enabled = false;
        phoneAdverter.color = new Color(phoneAdverter.color.r, phoneAdverter.color.g, phoneAdverter.color.b, 0);
        alarmAdverter.color = new Color(alarmAdverter.color.r, alarmAdverter.color.g, alarmAdverter.color.b, 0);
        mailAdverter.enabled = true;
        mailCounter.enabled = true;
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
        //phoneAdverter.enabled = true;
        phoneAdverter.color = new Color(phoneAdverter.color.r, phoneAdverter.color.g, phoneAdverter.color.b, 255);
    }

    private void OnPhoneEnd()
    {
        //phoneAdverter.enabled = false;
        phoneAdverter.color = new Color(phoneAdverter.color.r, phoneAdverter.color.g, phoneAdverter.color.b, 0);
    }

    private void OnAlarmStart()
    {
        //alarmAdverter.enabled = true;
        alarmAdverter.color = new Color(alarmAdverter.color.r, alarmAdverter.color.g, alarmAdverter.color.b, 255);
    }

    private void OnAlarmEnd()
    {
        //alarmAdverter.enabled = false;
        alarmAdverter.color = new Color(alarmAdverter.color.r, alarmAdverter.color.g, alarmAdverter.color.b, 0);
    }

    private void OnMailArrived()
    {
        //mailAdverter.enabled = true;
        //mailCounter.enabled = true;
        GameManager.MailCounter++;
        mailCounter.SetText($"{GameManager.MailCounter}");
    }

    private void OnMailDismissed()
    {
        mailCounter.SetText($"{ GameManager.MailCounter}");
    }
}
