using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mail : MonoBehaviour
{
    [Header("Instruction")]
    [SerializeField] private string arrivalNotification;
    [SerializeField] private string prova1;
    [SerializeField] private string prova2;
    [SerializeField] private string prova3;


    [SerializeField] private float timeForMail;

    private float timer;
    private UnityEvent MailAdverter = new UnityEvent();

    void Update()
    {
        timeForMail -= Time.deltaTime;
        if (timeForMail <= 0)
        {
            MailAdverter.Invoke();
        }

    }
}
