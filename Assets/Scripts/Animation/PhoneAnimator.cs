using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        GameManager.PhoneAdverterStart.AddListener(CallingReceived);
        GameManager.PhoneTaskStart.AddListener(CallingResponding);
    }
    private void OnDisable()
    {
        GameManager.PhoneAdverterStart.RemoveListener(CallingReceived);
        GameManager.PhoneTaskStart.RemoveListener(CallingResponding);
    }

    private void CallingResponding()
    {
        animator.SetTrigger("CallingResponding");
    }

    private void CallingReceived()
    {
        animator.SetTrigger("CallingReceived");
    }

}
