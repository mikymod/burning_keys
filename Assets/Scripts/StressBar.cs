using System;
using UnityEngine;
using UnityEngine.UI;

public class StressBar : MonoBehaviour
{
    [SerializeField] private Image mask;
    [SerializeField] private float maxValue;
    [SerializeField] private float speed;
    [SerializeField] private float spinnerBonusValue;

    public static float currentValue = 0f;

    private bool alarmActive = false;
    private int emailEventCounter = 0;
    private bool callActive = false;

    public bool StressActive { get => alarmActive || emailEventCounter > 0 || callActive; }

    private bool Filled { get => currentValue >= 100f; }

    private void OnEnable()
    {
        GameManager.AlarmAdverterStart.AddListener(OnAlarmStart);
        GameManager.MailAdverterStart.AddListener(OnMailStart);
        GameManager.PhoneAdverterStart.AddListener(OnPhoneStart);

        GameManager.AlarmAdverterEnd.AddListener(OnAlarmEnd);
        GameManager.MailAdverterEnd.AddListener(OnMailEnd);
        GameManager.PhoneAdverterEnd.AddListener(OnPhoneEnd);

        Spinner.StopRotating.AddListener(OnSpinnerStopRotating);
    }

    private void OnDisable()
    {
        GameManager.AlarmAdverterStart.RemoveListener(OnAlarmStart);
        GameManager.MailAdverterStart.RemoveListener(OnMailStart);
        GameManager.PhoneAdverterStart.RemoveListener(OnPhoneStart);

        GameManager.AlarmAdverterEnd.RemoveListener(OnAlarmEnd);
        GameManager.MailAdverterEnd.RemoveListener(OnMailEnd);
        GameManager.PhoneAdverterEnd.RemoveListener(OnPhoneEnd);

        Spinner.StopRotating.RemoveListener(OnSpinnerStopRotating);
    }

    private void Start() => currentValue = 0f;

    private void OnAlarmStart() => alarmActive = true;

    private void OnMailStart() => emailEventCounter++;

    private void OnPhoneStart() => callActive = true;

    private void OnAlarmEnd() => alarmActive = false;

    private void OnMailEnd()
    {
        emailEventCounter--;
        if (emailEventCounter < 0)
        {
            emailEventCounter = 0;
        }
    }

    private void OnPhoneEnd() => callActive = false;

    private void Update()
    {
        if (Filled)
        {
            return;
        }

        if (StressActive)
        {
            currentValue += speed * Time.deltaTime * GetCurrentMultiplier();
            mask.fillAmount = currentValue / maxValue;

            if (currentValue >= maxValue)
            {
                GameManager.StressBarFilled.Invoke();
            }
        }
    }


    private void OnSpinnerStopRotating() => currentValue -= spinnerBonusValue;

    private float GetCurrentMultiplier()
    {
        // TODO: shit! need refactoring
        if (currentValue >= 0f && currentValue < 20f)
        {
            return 2f;
        }
        else if (currentValue >= 20f && currentValue < 40f)
        {
            return 1.5f;
        }
        else if (currentValue >= 40f && currentValue < 60f)
        {
            return 1f;
        }
        else if (currentValue >= 60f && currentValue < 80f)
        {
            return 0.75f;
        }
        else if (currentValue >= 80f)
        {
            return 0.5f;
        }

        return 1f;
    }
}
