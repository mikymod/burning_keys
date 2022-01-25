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
        InputManager.KeyCodeInput.AddListener(OnKeyCodeInput);

        GameManager.MailAdverterEnd.AddListener(OnCompleteAllMail);
        GameManager.MailTaskStart.AddListener(OnTaskStartedCallback);
        GameManager.MailTaskFinished.AddListener(OnTaskFinishedCallback);
    }
    private void OnDisable()
    {
        InputManager.KeyCodeInput.RemoveListener(OnKeyCodeInput);

        GameManager.MailAdverterEnd.RemoveListener(OnCompleteAllMail);
        GameManager.MailTaskStart.RemoveListener(OnTaskStartedCallback);
        GameManager.MailTaskFinished.RemoveListener(OnTaskFinishedCallback);
    }
    private void OnCompleteAllMail()
    {
        if (GameManager.MailCounter == 0)
        {
            print("Mail No More");
            counter = 0;
            GameManager.MailTaskFinished.Invoke();
            return;
        }
    }
    private void OnKeyCodeInput(KeyCode arg0)
    {
        if (!isActive)
        {
            return;
        }
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

    private void OnTaskStartedCallback()
    {
        // TODO: manage multiple email task started
        // sum new iterations to the old ones
        if (isActive)
        {
            return;
        }
        isActive = true;

        firstGO.gameObject.SetActive(true);
        secondGO.gameObject.SetActive(true);
    }

    private void OnTaskFinishedCallback()
    {
        isActive = false;

        firstGO.gameObject.SetActive(false);
        secondGO.gameObject.SetActive(false);
    }

    private void Start()
    {
        firstGO.text = firstKey.ToString();
        secondGO.text = secondKey.ToString();
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
