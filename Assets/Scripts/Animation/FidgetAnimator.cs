using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


class FidgetAnimator : MonoBehaviour
{
    [SerializeField] private float minTimeForSpin, maxTimeForSpin;


    private Animator animator;
    private float randomSpin;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        GameManager.AlarmAdverterStart.AddListener(FidgetIsActive);
        FidgetActivation.OnFidgetStop.AddListener(FidgetIsNotActive);
    }
    private void OnDisable()
    {
        GameManager.AlarmAdverterStart.RemoveListener(FidgetIsActive);
        FidgetActivation.OnFidgetStop.RemoveListener(FidgetIsNotActive);
    }

    private void FidgetIsActive()
    {
        animator.SetBool("FidgetIsActive", true);
        //StartCoroutine(FidgetSpin());
    }
    //private IEnumerator FidgetSpin()
    //{
    //    randomSpin = Random.Range(minTimeForSpin, maxTimeForSpin);
    //    while (randomSpin > 0)
    //    {
    //        randomSpin -= Time.deltaTime;
    //        //gameMgrStopFidjetEvent;
    //        yield return null;
    //    }
    //}

    private void FidgetIsNotActive()
    {
        animator.SetBool("FidgetIsActive", false);
    }
}

