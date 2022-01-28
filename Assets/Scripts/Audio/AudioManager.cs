using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioSource relaxMusicSource;
    [SerializeField] private AudioSource metalMusicSource;
    [SerializeField] private AudioSource stingerForRelaxMusicSource;
    [SerializeField] private AudioSource stingerForMetalMusicSource;

    [Header("Alarm")]
    [SerializeField] private AudioSource alarmSource;
    [SerializeField] private AudioClip alarmPlay;
    [SerializeField] private AudioClip alarmStop;

    [Header("Phone")]
    [SerializeField] private AudioSource phoneSource;
    [SerializeField] private AudioClip phonePlay;
    [SerializeField] private AudioClip[] voices;

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

        GameManager.DesktopTaskFinished.AddListener(StopSounds);
        GameManager.StressBarFilled.AddListener(StopSounds);
    }

    private void OnDisable()
    {
        GameManager.AlarmAdverterStart.RemoveListener(OnAlarmAdverterStartedCallback);
        GameManager.AlarmTaskFinished.RemoveListener(OnAlarmTaskFinishedCallback);
        GameManager.PhoneAdverterStart.RemoveListener(OnPhoneAdverterStartedCallback);
        GameManager.PhoneTaskStart.RemoveListener(OnPhoneTaskStartedCallback);
        GameManager.PhoneTaskFinished.RemoveListener(OnPhoneTaskFinishedCallback);

        GameManager.DesktopTaskFinished.RemoveListener(StopSounds);
        GameManager.StressBarFilled.RemoveListener(StopSounds);
    }

    private void StopSounds()
    {
        alarmSource.Stop();
        phoneSource.Stop();
        // emailSource.Stop();
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
            phoneSource.clip = phonePlay;
            phoneSource.Play();
        }
    }
    private void OnPhoneTaskStartedCallback()
    {
            phoneSource.Stop();
            int random = Random.Range(0, 2);
            phoneSource.clip = voices[random];
            phoneSource.Play();
    }
    private void OnPhoneTaskFinishedCallback()
    {
        if (phoneSource.isPlaying)
        {
            phoneSource.Stop();
        }
    }
    void MusicMix()
    {
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
    }

}
