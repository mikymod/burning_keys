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
    [SerializeField] private float blurAmount;

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
    }

    private void OnSpinnerTaskFinished()
    {
        isActive = false;
        animator.SetTrigger("MoveToInitialPos");
    }

    void Update()
    {
        if (!isActive) return;
        if (Input.GetMouseButtonDown(1) && !isRotating)
        {
            StartCoroutine(Rotate());
        }
    }
    public void OnBlurStart()
    {
        StartCoroutine(BlurFadeIn());
    }
    public void OnBlurEnd()
    {
        StartCoroutine(BlurFadeOut());
    }

    private IEnumerator BlurFadeIn()
    {
        if (!depthOfField.enabled)
            depthOfField.enabled = true;

        for (float i = 0; i < animator.GetCurrentAnimatorStateInfo(0).length; i += Time.deltaTime)
        {
            myDheph.focalLength.value = Mathf.Lerp(0, blurAmount, i);
            yield return null;
        }
    }
    private IEnumerator BlurFadeOut()
    {
        for (float i = 0; i < animator.GetCurrentAnimatorStateInfo(0).length; i += Time.deltaTime)
        {
            myDheph.focalLength.value = Mathf.Lerp(blurAmount, 0, i);
            yield return null;
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
