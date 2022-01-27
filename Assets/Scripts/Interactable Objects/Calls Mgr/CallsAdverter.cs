using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallsAdverter : MonoBehaviour
{
    public bool IsActive;

    private void OnEnable()
    {
        GameManager.PhoneAdverterStart.AddListener(ActiveObject);
        GameManager.PhoneAdverterEnd.AddListener(DisableObject);
    }
    private void OnDisable()
    {
        GameManager.PhoneAdverterStart.RemoveListener(ActiveObject);
        GameManager.PhoneAdverterEnd.RemoveListener(DisableObject);
    }
    private void ActiveObject()
    {
        if (!IsActive)
        {
            IsActive = true;
            print("Phone Adverter");
            //Animation play
        }
    }

    private void DisableObject()
    {
        if (IsActive)
        {
            IsActive = false;
            print("Phone Adverter End");
            //Animation stop
        }
    }
}
