using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FidgetActivation : MonoBehaviour
{
    public static UnityEvent StopRotating = new UnityEvent();

    [SerializeField] private float minSpeedForSpin, maxSpeedForSpin;
    [SerializeField] private float timeForSpin;
    [SerializeField] private float FidgetOffset;
    [SerializeField] private int maxUsage;
    private int currentUsage = 0;

    private float randomSpin;
    private bool isActive = false;
    private bool isRotating = false;
    private Vector3 initialPos;
    private Quaternion initialRot;

    private void OnEnable()
    {
        GameManager.SpinnerTaskStart.AddListener(OnSpinnerTaskStarted);
        GameManager.SpinnerTaskFinished.AddListener(OnSpinnerTaskFinished);
    }

    private void OnDisable()
    {
        GameManager.SpinnerTaskStart.RemoveListener(OnSpinnerTaskStarted);
        GameManager.SpinnerTaskFinished.RemoveListener(OnSpinnerTaskFinished);
    }

    private void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
    }

    private void OnSpinnerTaskStarted()
    {
        if (currentUsage >= maxUsage)
        {
            OnSpinnerTaskFinished();
            return;
        }

        isActive = true;
        InputManager.KeyCodeInput.AddListener(OnKeyCodeInput);
    }

    private void OnSpinnerTaskFinished()
    {
        isActive = false;
        InputManager.KeyCodeInput.RemoveListener(OnKeyCodeInput);

        ResetTransform();
    }

    private void OnKeyCodeInput(KeyCode keyCode)
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        if (!isActive) return;
        // The spinner is in focus
        gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * FidgetOffset;

        if (Input.GetMouseButtonDown(1) && !isRotating)
        {
            StartCoroutine(Rotate());
        }
    }

    private IEnumerator Rotate()
    {
        currentUsage++;
        isRotating = true;
        
        randomSpin = timeForSpin;
        while (randomSpin > 0)
        {
            randomSpin -= Time.deltaTime;
            yield return null;
        }
        isRotating = false;
        
        StopRotating.Invoke();
    }

    private void ResetTransform()
    {
        transform.position = initialPos;
        transform.rotation = initialRot;
    }
}
