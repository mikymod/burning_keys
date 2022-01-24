using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmObject : MonoBehaviour
{
    public bool IsActive;

    [SerializeField] private AudioClip alarmPlay;
    [SerializeField] private AudioClip alarmStop;
    private AudioSource audiosource;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

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
            audiosource.loop = true;
            audiosource.clip = alarmPlay;
            audiosource.Play();
            print("Alarm Activation");
            //Animation play
        }
    }

    private void DisableObject()
    {
        if (IsActive)
        {
            IsActive = false;
            audiosource.loop = false;
            audiosource.clip = alarmStop;
            audiosource.Play();
            //Animation stop
        }
    }

}
