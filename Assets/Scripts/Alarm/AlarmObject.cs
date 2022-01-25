using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmObject : MonoBehaviour
{
    public bool IsActive;

    private void OnEnable()
    {
        GameManager.AlarmAdverterStart.AddListener(ActiveObject);
        GameManager.AlarmAdverterEnd.AddListener(DisableObject);
    }
    private void OnDisable()
    {
        GameManager.AlarmAdverterStart.RemoveListener(ActiveObject);
        GameManager.AlarmAdverterEnd.RemoveListener(DisableObject);
    }
    private void ActiveObject()
    {
        if (!IsActive)
        {
            IsActive = true;
            print("Alarm Activation");
            //Animation play
        }
    }

    private void DisableObject()
    {
        if (IsActive)
        {
            IsActive = false;
            //Animation stop
        }
    }

}
