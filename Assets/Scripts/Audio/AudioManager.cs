using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioSource relaxMusicSource;
    [SerializeField] private AudioSource metalMusicSource;
    [SerializeField] private AudioSource spinnerMusicSource;
    [SerializeField] private AudioSource stingerForRelaxMusicSource;
    [SerializeField] private AudioSource stingerForMetalMusicSource;

    [Header("Alarm")]
    [SerializeField] private AudioSource alarmSource;
    [SerializeField] private AudioClip alarmPlay;
    [SerializeField] private AudioClip alarmStop;

    [Header("Phone")]
    [SerializeField] private AudioSource phoneSource;
    [SerializeField] private AudioClip phonePlay;
    [SerializeField] private AudioClip phoneFailed;
    [SerializeField] private AudioClip[] voices;

    [Header("Email")]
    [SerializeField] private AudioSource emailSource;

    [Header("TaskCompleted")]
    [SerializeField] private AudioSource taskFinishedSource;

    [Header("GameWin/Over")]
    [SerializeField] private AudioSource gameWinOverSource;
    [SerializeField] private AudioClip gameWinClip;
    [SerializeField] private AudioClip gameOverClip;
    private bool phoneVoices;

    IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        float time = 0f;
        float duration = fadeTime;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 1f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 1f;
    }

    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float time = 0f;
        float duration = fadeTime;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 0f;
    }

    private void OnEnable()
    {
        GameManager.AlarmAdverterStart.AddListener(OnAlarmAdverterStartedCallback);
        GameManager.AlarmTaskFinished.AddListener(OnAlarmTaskFinishedCallback);
        GameManager.PhoneAdverterStart.AddListener(OnPhoneAdverterStartedCallback);
        GameManager.PhoneTaskStart.AddListener(OnPhoneTaskStartedCallback);
        GameManager.PhoneTaskFinished.AddListener(OnPhoneTaskFinishedCallback);
        Call.CallStepFailed.AddListener(OnPhoneFailedTask);
        GameManager.MailAdverterStart.AddListener(OnEmailAdverterStartedCallback);
        GameManager.MailTaskFinished.AddListener(OnEmailTaskFinishedCallback);
        Spinner.StartRotating.AddListener(OnSpinnerTaskStarted);
        GameManager.SpinnerTaskFinished.AddListener(OnSpinnerTaskFinished);

        GameManager.DesktopTaskFinished.AddListener(OnGameCompleted);
        GameManager.StressBarFilled.AddListener(OnGameOver);
    }

    private void OnDisable()
    {
        GameManager.AlarmAdverterStart.RemoveListener(OnAlarmAdverterStartedCallback);
        GameManager.AlarmTaskFinished.RemoveListener(OnAlarmTaskFinishedCallback);
        GameManager.PhoneAdverterStart.RemoveListener(OnPhoneAdverterStartedCallback);
        GameManager.PhoneTaskStart.RemoveListener(OnPhoneTaskStartedCallback);
        GameManager.PhoneTaskFinished.RemoveListener(OnPhoneTaskFinishedCallback);
        Call.CallStepFailed.RemoveListener(OnPhoneFailedTask);
        GameManager.MailAdverterStart.RemoveListener(OnEmailAdverterStartedCallback);
        GameManager.MailTaskFinished.RemoveListener(OnEmailTaskFinishedCallback);
        Spinner.StartRotating.RemoveListener(OnSpinnerTaskStarted);
        GameManager.SpinnerTaskFinished.RemoveListener(OnSpinnerTaskFinished);

        GameManager.DesktopTaskFinished.RemoveListener(OnGameCompleted);
        GameManager.StressBarFilled.RemoveListener(OnGameOver);
    }

    private void StopSounds()
    {
        alarmSource.Stop();
        phoneSource.Stop();
        emailSource.Stop();
        spinnerMusicSource.Stop();
    }

    private void OnGameCompleted()
    {
        StopSounds();
        gameWinOverSource.clip = gameWinClip;
        gameWinOverSource.Play();
    }

    private void OnGameOver()
    {
        StopSounds();
        gameWinOverSource.clip = gameOverClip;
        gameWinOverSource.Play();
    }

    private void OnAlarmAdverterStartedCallback()
    {
        if (!alarmSource.isPlaying)
        {
            alarmSource.loop = true;
            alarmSource.clip = alarmPlay;
            alarmSource.Play();
        }
    }

    private void OnAlarmTaskFinishedCallback()
    {
        OnTaskFinishedCallback();
        if (alarmSource.isPlaying)
        {
            alarmSource.loop = false;
            alarmSource.clip = alarmStop;
            alarmSource.Play();
        }
    }
    private void OnPhoneAdverterStartedCallback()
    {
        if (!phoneSource.isPlaying)
        {
            phoneSource.loop = true;
            phoneSource.clip = phonePlay;
            phoneSource.Play();
        }
    }
    private void OnPhoneTaskStartedCallback()
    {
        phoneSource.Stop();
        phoneSource.loop = false;
        phoneVoices = true;
    }
    private void OnPhoneTaskFinishedCallback()
    {
        OnTaskFinishedCallback();
        if (phoneSource.isPlaying)
        {
            phoneVoices = false;
            phoneSource.Stop();
        }
    }

    private void OnPhoneFailedTask()
    {
        phoneSource.clip = phoneFailed;
        phoneSource.Play();
    }

    private void OnEmailAdverterStartedCallback()
    {
        if (!emailSource.isPlaying)
        {
            emailSource.Play();
        }
    }

    private void OnEmailTaskFinishedCallback()
    {
        OnTaskFinishedCallback();
    }

    private void OnTaskFinishedCallback()
    {
        taskFinishedSource.Play();
    }

    private void OnSpinnerTaskStarted()
    {
        if (spinnerMusicSource.isPlaying)
        {
            return;
        }
        relaxMusicSource.Stop();
        metalMusicSource.Stop();
        spinnerMusicSource.Stop();
        stingerForRelaxMusicSource.Stop();
        stingerForMetalMusicSource.Stop();
        spinnerMusicSource.Play();
    }

    private void OnSpinnerTaskFinished()
    {
        if (spinnerMusicSource.isPlaying)
        {
            spinnerMusicSource.Stop();
        }
    }

    private void PhoneVoicesMix()
    {
        if (phoneVoices)
        {
            if (!phoneSource.isPlaying)
            {
                int random = Random.Range(0, 2);
                phoneSource.clip = voices[random];
                phoneSource.Play();
            }
        }
    }

    private void MusicMix()
    {
        if (spinnerMusicSource.isPlaying)
        {
            return;
        }
        //TODO change music on stress value change
        if (StressBar.currentValue <= 50f)
        {
            if (metalMusicSource.isPlaying)
            {
                stingerForMetalMusicSource.Stop();
                metalMusicSource.Stop();
            }
            if (!relaxMusicSource.isPlaying)
            {
                stingerForRelaxMusicSource.Play();
                relaxMusicSource.PlayDelayed(1.0f);
            }
        }
        if (StressBar.currentValue > 50f)
        {
            if (relaxMusicSource.isPlaying)
            {
                stingerForRelaxMusicSource.Stop();
                relaxMusicSource.Stop();
            }
            if (!metalMusicSource.isPlaying)
            {
                stingerForMetalMusicSource.Play();
                metalMusicSource.PlayDelayed(2.2f);
            }
        }
    }

    private void Update()
    {
        MusicMix();
        PhoneVoicesMix();
    }

}
