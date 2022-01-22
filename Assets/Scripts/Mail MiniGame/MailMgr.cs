using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EmailTime : UnityEvent { }
public class MailMgr : MonoBehaviour
{
    public float TimeToMail;
    [HideInInspector] public static EmailTime EmailTime = new EmailTime();

    void Update()
    {
        TimeToMail -= Time.deltaTime;
        if (TimeToMail <= 0)
        {
            EmailTime.Invoke();
            TimeToMail = 2;
        }
    }
}
