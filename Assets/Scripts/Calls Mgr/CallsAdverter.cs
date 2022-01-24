using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallsAdverter : MonoBehaviour
{
    public bool IsActive;
    private AudioSource audiosource;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }
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
            audiosource.Play();
            print("Phone Adverter");
            //Animation play
        }
    }

    private void DisableObject()
    {
        if (IsActive)
        {
            IsActive = false;
            audiosource.Stop();
            print("Phone Adverter End");
            //Animation stop
        }
    }
}
