using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameMgr : MonoBehaviour
{
    [SerializeField] private string winTitleText;
    [SerializeField] private string loseTitleText;

    [SerializeField] private string retryText;
    [SerializeField] private string retryTextWin;
    [SerializeField] private string retryTextLose;

    [SerializeField] private string backToMenýText;
    [SerializeField] private string backToMenýTextWin;
    [SerializeField] private string backToMenýTextLose;

    [Header("Where i'll Write")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text retryButton;
    [SerializeField] private TMP_Text menuButton;

    [SerializeField] private GameObject allItems;
    private void OnEnable()
    {
        GameManager.FinishedAllTheTasks.AddListener(OnAllTaskEnd);
        GameManager.LoseForTheStress.AddListener(OnLoseForStress);
    }
    private void OnDisable()
    {
        GameManager.FinishedAllTheTasks.RemoveListener(OnAllTaskEnd);
        GameManager.LoseForTheStress.RemoveListener(OnLoseForStress);
    }

    private void Start()
    {
        if (backToMenýTextLose == null) backToMenýTextLose = backToMenýText;
        if (backToMenýTextWin == null) backToMenýTextWin = backToMenýText;

        if (retryTextLose == null) retryTextLose = retryText;
        if (retryTextWin == null) retryTextWin = retryText;
    }
    private void OnAllTaskEnd()
    {
        if (!allItems.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        title.text = winTitleText;
        retryButton.text = retryTextWin;
        menuButton.text = backToMenýTextWin;
    }
    private void OnLoseForStress()
    {
        if (!allItems.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        print("C'Ť UN PROBLEMA");
        title.text = loseTitleText;
        menuButton.text = backToMenýTextLose;
        retryButton.text = retryTextLose;
    }

}
