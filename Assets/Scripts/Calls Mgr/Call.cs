using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Call : MonoBehaviour
{
    [SerializeField] private bool isActive;

    [Header("Customization")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject bar;
    [SerializeField] private KeyCode keycode = KeyCode.G;
    [SerializeField] private int numIterations = 1;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float minRange = 0.35f;
    [SerializeField] private float maxRange = 0.65f;

    private float value = 0f;
    private float orientation = 1f;
    private List<RectTransform> pointerList = new List<RectTransform>();
    private RectTransform pointer;
    private int pointerIndex;

    public static UnityEvent CallStepCompleted = new UnityEvent();
    public static UnityEvent CallStepFailed = new UnityEvent();

    private void OnEnable()
    {
        GameManager.PhoneTaskStart.AddListener(OnTaskStartedCallback);
        GameManager.PhoneTaskFinished.AddListener(OnTaskFinishedCallback);
    }

    private void OnDisable()
    {
        GameManager.PhoneTaskStart.RemoveListener(OnTaskStartedCallback);
        GameManager.PhoneTaskFinished.RemoveListener(OnTaskFinishedCallback);
    }

    private void OnTaskStartedCallback()
    {
        isActive = true;
        GameManager.PhoneAdverterEnd.Invoke();

        for (int i = 0; i < numIterations; i++)
        {
            var go = Instantiate(prefab, bar.transform);
            var rt = go.GetComponent<Image>().rectTransform;
            rt.anchoredPosition = new Vector2(0f, 0f);
            pointerList.Add(rt);
        }

        pointer = pointerList[0];
    }

    private void OnTaskFinishedCallback()
    {
        isActive = false;
        pointerList.Clear();
    }

    private void Update()
    {
        if (!isActive)
        {
            value = 0f;
            return;
        }

        value += (Time.deltaTime * speed * orientation);
        if (value <= 0f || value >= 1f - pointer.localScale.x)
        {
            orientation *= -1f;
        }
        value = Mathf.Clamp(value, 0, 1f - pointer.localScale.x);
        pointer.anchoredPosition = new Vector2(value, 0f);

        if (Input.GetKeyDown(keycode))
        {
            if (value >= minRange && value <= maxRange)
            {
                CallStepCompleted.Invoke();

                // TODO: add animation
                Destroy(pointer.gameObject);
                pointerIndex++;
                if (pointerIndex >= numIterations)
                {
                    GameManager.PhoneTaskFinished.Invoke();
                }
                else
                {
                    pointer = pointerList[pointerIndex];
                    value = 0f;
                }
            }
            else
            {
                CallStepFailed.Invoke();
            }
        }
    }
}
