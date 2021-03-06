using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour
{
    [SerializeField] private Image mask;
    [SerializeField] private float maxValue;
    [SerializeField] private float keySmashCompletedValue = 1.5f;
    [SerializeField] private float wordCompletedValue = 2.25f;

    private float currentValue = 0f;
    private bool Filled { get => currentValue >= 100f; }

    private void OnEnable()
    {
        KeySmashValidator.KeySmashCompleted.AddListener(OnKeySmashCompleted);
        WordValidator.WordCompleted.AddListener(OnWordCompleted);
    }

    private void OnDisable()
    {
        KeySmashValidator.KeySmashCompleted.RemoveListener(OnKeySmashCompleted);
        WordValidator.WordCompleted.RemoveListener(OnWordCompleted);
    }

    private void OnKeySmashCompleted()
    {
        currentValue += keySmashCompletedValue;
    }

    private void OnWordCompleted()
    {
        currentValue += wordCompletedValue;
    }

    private void Update()
    {
        // if (Filled)
        // {
        //     return;
        // }

        mask.fillAmount = currentValue / maxValue;   

        if (currentValue >= maxValue)
        {
            GameManager.DesktopTaskFinished.Invoke();
        }     
    }
}
