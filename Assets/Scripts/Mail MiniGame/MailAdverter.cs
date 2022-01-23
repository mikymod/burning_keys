using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

public class MailAdverter : MonoBehaviour
{
    private GameObject mailPopUp;
    private TextMeshProUGUI textMailNumber;
    private void Awake()
    {
        //Need a Fix: per il momento prende due obj (ovvero il text e l'image della notifica)
        mailPopUp = gameObject.transform.GetChild(0).gameObject;
        textMailNumber = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        GameManager.MailAdverterStart.AddListener(ActivePopUp);
        GameManager.MailAdverterEnd.AddListener(DisactivePopUp);
    }
    private void OnDisable()
    {
        GameManager.MailAdverterStart.RemoveListener(ActivePopUp);
        GameManager.MailAdverterEnd.RemoveListener(DisactivePopUp);
    }
    private void ActivePopUp()
    {
        Debug.Log("Arrivata Mail");
        if (!mailPopUp.activeInHierarchy)
        {
            mailPopUp.SetActive(true);
        }
        textMailNumber.SetText($"{GameManager.MailCounter}");
    }
    private void DisactivePopUp()
    {
        if (GameManager.MailCounter == 0 && mailPopUp.activeInHierarchy)
        {
            mailPopUp.SetActive(false);
            textMailNumber.SetText($"");
        }
        else
            textMailNumber.SetText($"{ GameManager.MailCounter}");
    }
}
