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
    private int mailNumber;
    private void Awake()
    {
        mailPopUp = gameObject.transform.GetChild(0).gameObject;
        textMailNumber = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        MailMgr.EmailTime.AddListener(ActivePopUp);
    }
    private void OnDisable()
    {
        MailMgr.EmailTime.RemoveListener(ActivePopUp);
    }
    private void ActivePopUp()
    {
        mailNumber++;
        if (!mailPopUp.activeInHierarchy)
        {
            mailPopUp.SetActive(true);
        }
        textMailNumber.SetText($"{ mailNumber}");
    }
}
