using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MailManager : MonoBehaviour
{
    [SerializeField] private KeyCode firstKey;
    [SerializeField] private KeyCode secondKey;
    [SerializeField] private int iterations = 6;
    [SerializeField] private TMP_Text firstGO;
    [SerializeField] private TMP_Text secondGO;


    private KeyCode currentKeyCode;
    private int counter = 0;
    private bool isCurrentKeyCodeSet = false;

    private void Start()
    {
        firstGO.text = firstKey.ToString();
        secondGO.text = secondKey.ToString();
    }

    private void Update()
    {
        if (!isCurrentKeyCodeSet)
        {
            if (Input.GetKeyDown(firstKey) || Input.GetKeyDown(secondKey))
            {
                SetInitialKeyCode();
                return;
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(currentKeyCode))
                {
                    counter++;
                    SwitchCurrentKeyCode();
                    Debug.Log("Correct key");

                    if (counter == iterations)
                    {
                        // Send Task Completed event
                        isCurrentKeyCodeSet = false;
                        Debug.Log("Task completed");
                    }
                }
                else
                {
                    counter = 0;
                    isCurrentKeyCodeSet = false;
                    Debug.Log("You have to restart");
                }
            }
        }
    }

    private void SwitchCurrentKeyCode()
    {
        currentKeyCode = currentKeyCode == firstKey ? secondKey : firstKey;
    }

    private void SetInitialKeyCode()
    {
        currentKeyCode = Input.GetKeyDown(firstKey) ? secondKey : firstKey;
        isCurrentKeyCodeSet = true;
        counter++;
        Debug.Log($"Init key pressed");
    }
}
