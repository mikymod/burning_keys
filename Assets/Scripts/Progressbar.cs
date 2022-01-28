using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour
{
    [SerializeField] private Image mask;
    [SerializeField] private float maxValue;

    public float currentValue = 0f;

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
        currentValue += 1f;
    }

    private void OnWordCompleted()
    {
        currentValue += 2f;
    }

    private void Update()
    {
        mask.fillAmount = currentValue / maxValue;   

        if (currentValue >= maxValue)
        {
            GameManager.DesktopTaskFinished.Invoke();
        }     
    }
}
