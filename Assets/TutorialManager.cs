using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private float transitionTime;

    private float timer = 0f;
    private int mailTasksCompleted = 0;
    private int desktopTasksCompleted = 0;

    private void OnEnable()
    {
        GameManager.AlarmTaskFinished.AddListener(OnAlarmTaskFinished);        
        GameManager.PhoneTaskFinished.AddListener(OnPhoneTaskFinished);        
        GameManager.MailTaskFinished.AddListener(OnMailTaskFinished);
        KeySmashValidator.KeySmashCompleted.AddListener(OnDesktopTaskFinished);
        WordValidator.WordCompleted.AddListener(OnDesktopTaskFinished);
    }

    private void OnDisable()
    {
        GameManager.AlarmTaskFinished.AddListener(OnAlarmTaskFinished);        
        GameManager.PhoneTaskFinished.AddListener(OnPhoneTaskFinished);        
        GameManager.MailTaskFinished.AddListener(OnMailTaskFinished);
        KeySmashValidator.KeySmashCompleted.AddListener(OnDesktopTaskFinished);
        WordValidator.WordCompleted.AddListener(OnDesktopTaskFinished);        
    }

    private IEnumerator GoToNextScene()
    {
        timer += Time.deltaTime;

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(nextScene);
    }

    private void OnAlarmTaskFinished()
    {
        StartCoroutine(GoToNextScene());
    }

    private void OnPhoneTaskFinished()
    {
        StartCoroutine(GoToNextScene());
    }

    private void OnMailTaskFinished()
    {
        mailTasksCompleted++;

        if (mailTasksCompleted == 2)
        {
            StartCoroutine(GoToNextScene());
        }
    }

    private void OnDesktopTaskFinished()
    {
        desktopTasksCompleted++;

        if (desktopTasksCompleted == 5)
        {
            StartCoroutine(GoToNextScene());
        }
    }
}
