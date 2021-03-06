using System;
using UnityEngine;

public class DesktopManager : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private GameObject wordPrefab;
    [SerializeField] private int numKeySmashes = 3;
    [SerializeField] private int numWordsToType = 3;
    private int numKeysSmashCompleted = 0;
    private int numWordsTypedCompleted = 0;

    private void OnEnable()
    {
        GameManager.DesktopTaskFocus.AddListener(ResumeTask);


        KeySmashValidator.KeySmashCompleted.AddListener(OnKeySmashDone);
        WordValidator.WordCompleted.AddListener(OnWordDone);
    }

    private void OnDisable()
    {
        GameManager.DesktopTaskFocus.RemoveListener(ResumeTask);


        KeySmashValidator.KeySmashCompleted.RemoveListener(OnKeySmashDone);
        WordValidator.WordCompleted.RemoveListener(OnWordDone);
    }


    private void ResumeTask()
    {
        InputManager.KeyCodeInput.AddListener(OnKeyCodeInput);

        GameManager.DesktopTaskFocus.RemoveListener(ResumeTask);
        GameManager.DesktopTaskUnfocus.AddListener(PauseTask);

        keyPrefab.SetActive(true);
        wordPrefab.SetActive(false);
    }

    private void PauseTask()
    {
        InputManager.KeyCodeInput.RemoveListener(OnKeyCodeInput);
        
        GameManager.DesktopTaskFocus.AddListener(ResumeTask);
        GameManager.DesktopTaskUnfocus.RemoveListener(PauseTask);

        keyPrefab.SetActive(false);
        wordPrefab.SetActive(false);         
    }

    private void OnKeyCodeInput(KeyCode keyCode)
    {
        if (keyCode == KeyCode.Space)
        {
            GameManager.DesktopTaskUnfocus.Invoke();
        }
    }

    private void OnKeySmashDone()
    {
        numKeysSmashCompleted++;

        if (numKeysSmashCompleted == numKeySmashes)
        {
            keyPrefab.SetActive(false);
            wordPrefab.SetActive(true);
            numKeysSmashCompleted = 0;
        }
    }

    private void OnWordDone()
    {
        numWordsTypedCompleted++;

        if (numWordsTypedCompleted == numWordsToType)
        {
            wordPrefab.SetActive(false);
            keyPrefab.SetActive(true);

            numWordsTypedCompleted = 0;
        }
    }
}
