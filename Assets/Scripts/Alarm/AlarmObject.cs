using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmObject : MonoBehaviour
{
    public bool IsActive;

    private void OnEnable()
    {
        GameManager.AlarmStart.AddListener(ActiveObject);
        GameManager.AlarmEnd.AddListener(DisableObject);
    }
    private void OnDisable()
    {
        GameManager.AlarmStart.RemoveListener(ActiveObject);
        GameManager.AlarmEnd.RemoveListener(DisableObject);
    }
    private void ActiveObject()
    {
        if (!IsActive)
        {
            IsActive = true;
            print("aiuto, rispondi");
            //Animation play
            //Audio play
        }
    }

    private void DisableObject()
    {
        if (IsActive)
        {
            IsActive = false;
            //Animation stop
            //Audio stop
        }
    }

}
