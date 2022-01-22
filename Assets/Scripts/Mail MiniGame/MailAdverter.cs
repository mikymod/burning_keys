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
        mailPopUp = gameObject.transform.GetChild(0).gameObject;
        textMailNumber = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        MailGame.MailTime.AddListener(ActivePopUp);
        MailGame.EndThisMail.AddListener(DisactivePopUp);
    }
    private void OnDisable()
    {
        MailGame.MailTime.RemoveListener(ActivePopUp);
        MailGame.EndThisMail.RemoveListener(DisactivePopUp);
    }
    private void ActivePopUp(int mailNumber)
    {
        Debug.Log("mi hanno chiamato");
        if (!mailPopUp.activeInHierarchy)
        {
            mailPopUp.SetActive(true);
        }
        textMailNumber.SetText($"{ mailNumber}");
    }
    private void DisactivePopUp(int mailNumber)
    {
        if (mailNumber == 0 && mailPopUp.activeInHierarchy)
        {
            mailPopUp.SetActive(false);
        }
        textMailNumber.SetText($"{ mailNumber}");
    }
}
