using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameMgr : MonoBehaviour
{
    [SerializeField] private string winTitleText;
    [SerializeField] private string loseTitleText;

    [SerializeField] private string retryText;
    [SerializeField] private string retryTextWin;
    [SerializeField] private string retryTextLose;

    [SerializeField] private string backToMenuText;
    [SerializeField] private string backToMenuTextWin;
    [SerializeField] private string backToMenuTextLose;

    [Header("Where i'll Write")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text retryButton;
    [SerializeField] private TMP_Text menuButton;
    [SerializeField] private string tryAgainScene;
    [SerializeField] private string backToMenuScene;

    [SerializeField] private GameObject allItems;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        GameManager.DesktopTaskFinished.AddListener(OnDesktopTaskFinished);
        GameManager.StressBarFilled.AddListener(OnStressBarFilled);
    }
    private void OnDisable()
    {
        GameManager.DesktopTaskFinished.RemoveListener(OnDesktopTaskFinished);
        GameManager.StressBarFilled.RemoveListener(OnStressBarFilled);
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(backToMenuTextLose)) backToMenuTextLose = backToMenuText;
        if (string.IsNullOrEmpty(backToMenuTextWin)) backToMenuTextWin = backToMenuText;

        if (string.IsNullOrEmpty(retryTextLose)) retryTextLose = retryText;
        if (string.IsNullOrEmpty(retryTextWin)) retryTextWin = retryText;
    }
    private void OnDesktopTaskFinished()
    {
        allItems.SetActive(true);
        animator.SetTrigger("EndGameStart");

        title.text = winTitleText;
        retryButton.text = retryTextWin;
        menuButton.text = backToMenuTextWin;
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnStressBarFilled()
    {
        allItems.SetActive(true);
        animator.SetTrigger("EndGameStart");

        title.text = loseTitleText;
        menuButton.text = backToMenuTextLose;
        retryButton.text = retryTextLose;
        Cursor.lockState = CursorLockMode.None;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(backToMenuScene);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(tryAgainScene);
    }

}
