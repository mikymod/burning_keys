using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioTutorial : MonoBehaviour
{
    public static AudioTutorial Instance { get; set; }
    public static UnityAction StartMusicTutorial;
    public static UnityAction StopMusicTutorial;
    private AudioSource musicTutorial;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        musicTutorial = GetComponent<AudioSource>();
        StartMusicTutorial.Invoke();
    }

    private void OnEnable()
    {
        StartMusicTutorial += OnStartMusicTutorial;
        StopMusicTutorial += OnStopMusicTutorial;
    }

    private void OnDisable()
    {
        StartMusicTutorial -= OnStartMusicTutorial;
        StopMusicTutorial -= OnStopMusicTutorial;
    }

    private void OnStopMusicTutorial()
    {
        musicTutorial.Stop();
    }

    private void OnStartMusicTutorial()
    {
        musicTutorial.Play();
    }
}
