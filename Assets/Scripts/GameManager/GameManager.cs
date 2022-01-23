using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityEvent AlarmStart = new UnityEvent();
    public static UnityEvent AlarmEnd = new UnityEvent();

    private float alarmCounter;
    public float maxAlarmCounter;


    private void Update()
    {
        alarmCounter += Time.deltaTime;
        if (alarmCounter >= maxAlarmCounter)
        {
            AlarmStart.Invoke();
            alarmCounter = 0;
            print("alarm");
        }
    }
}
