using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneAnimator : MonoBehaviour
{
    private Animator animator;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
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
        ResetTransform();
    }

    private void CallingReceived()
    {
        animator.SetTrigger("CallingReceived");
    }
    private void ResetTransform()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
