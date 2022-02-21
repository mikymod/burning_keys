using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Spinner : MonoBehaviour
{
    public static UnityEvent StopRotating = new UnityEvent();
    public static UnityEvent StartRotating = new UnityEvent();

    [SerializeField] private Volume depthOfField;
    [SerializeField] private float timeForSpin;
    [SerializeField] private int maxUsage;
    private int currentUsage = 0;

    private float randomSpin;
    private bool isActive = false;
    private bool isRotating = false;
    private Vector3 initialPos;
    private Quaternion initialRot;
    private Animator animator;
    private DepthOfField myDheph;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        //depthOfField = GetComponentInChildren<Volume>();
        depthOfField.profile.TryGet<DepthOfField>(out myDheph);
    }

    private void OnEnable()
    {
        GameManager.SpinnerTaskStart.AddListener(OnSpinnerTaskStarted);
        GameManager.SpinnerTaskFinished.AddListener(OnSpinnerTaskFinished);
    }

    private void OnDisable()
    {
        GameManager.SpinnerTaskStart.RemoveListener(OnSpinnerTaskStarted);
        GameManager.SpinnerTaskFinished.RemoveListener(OnSpinnerTaskFinished);
    }

    private void OnSpinnerTaskStarted()
    {
        animator.SetTrigger("MoveToCamera");
        isActive = true;
        depthOfField.enabled = true;
    }

    private void OnSpinnerTaskFinished()
    {
        isActive = false;
        animator.SetTrigger("MoveToInitialPos");
        depthOfField.enabled = false;
    }

    void Update()
    {
        if (!isActive) return;
        if(animator.GetCurrentAnimatorStateInfo(0).)
        myDheph.focalLength.value *= animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        print(animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
        if (Input.GetMouseButtonDown(1) && !isRotating)
        {
            StartCoroutine(Rotate());
        }
    }

    private IEnumerator Rotate()
    {
        currentUsage++;
        isRotating = true;
        animator.SetTrigger("Rotate");
        StartRotating.Invoke();
        yield return new WaitForSeconds(timeForSpin);

        animator.SetTrigger("StopRotate");
        isRotating = false;

        StopRotating.Invoke(); // TODO: Decrease stress

        if (currentUsage >= maxUsage)
        {
            GameManager.SpinnerTaskFinished.Invoke();
        }
    }
}
