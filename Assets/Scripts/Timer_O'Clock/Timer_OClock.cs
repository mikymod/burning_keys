using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Timer_OClock : MonoBehaviour
{
    public float StartTime;
    public bool Going;

    private float currentTime;
    private float finalTime;
    private TextMeshProUGUI timer;
    private void Awake()
    {
        timer = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        currentTime = StartTime;
        timer.SetText("00:00:000");
    }

    void Update()
    {
        if (Going)
        {
            currentTime += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            timer.SetText($"{time.ToString(@"mm\:ss\:fff")}");
        }
    }
}
