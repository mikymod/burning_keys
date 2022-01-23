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
    private bool isActive;

    private void OnEnable()
    {
        GameManager.MailTaskStart.AddListener(OnTaskStartedCallback);
        GameManager.MailTaskFinished.AddListener(OnTaskFinishedCallback);
    }
    private void OnDisable()
    {
        GameManager.MailTaskStart.RemoveListener(OnTaskStartedCallback);
        GameManager.MailTaskFinished.RemoveListener(OnTaskFinishedCallback);
    }
    private void OnTaskStartedCallback()
    {
        // TODO: manage multiple email task started
        // sum new iterations to the old ones
        if (isActive)
        {
            print("Mail was alredy active");
            return;
        }
        print("Mail Task iniziata");
        isActive = true;
    }
    private void OnTaskFinishedCallback()
    {
        if (!isActive)
        {
            print("Mail was alredy InActive");
            return;
        }
        print("Mail Task finita");
        isActive = false;
    }


    private void Start()
    {
        firstGO.text = firstKey.ToString();
        secondGO.text = secondKey.ToString();
    }

    private void Update()
    {
        if (isActive)
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
                            //Check the presence of other mails, take value from GameManager
                            if (GameManager.MailCounter != 0)
                            {
                                print("Mail More");
                                GameManager.MailCounter--;
                                counter = 0;
                                firstGO.text = firstKey.ToString();
                                secondGO.text = secondKey.ToString();
                            }
                            //No other mail? Task End
                            else
                            {
                                print("Mail No More");
                                GameManager.MailTaskFinished.Invoke();
                            }

                            GameManager.MailAdverterEnd.Invoke();
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
