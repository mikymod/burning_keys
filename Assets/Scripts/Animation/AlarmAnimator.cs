using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        GameManager.AlarmAdverterStart.AddListener(AlarmIsActive);
        GameManager.AlarmTaskFinished.AddListener(AlarmIsNotActive);
    }
    private void OnDisable()
    {
        GameManager.AlarmAdverterStart.RemoveListener(AlarmIsActive);
        GameManager.AlarmTaskFinished.RemoveListener(AlarmIsNotActive);
    }

    private void AlarmIsActive()
    {
        animator.SetBool("AlarmIsActive", true);
    }

    private void AlarmIsNotActive()
    {
        animator.SetBool("AlarmIsActive", false);
    }

}
