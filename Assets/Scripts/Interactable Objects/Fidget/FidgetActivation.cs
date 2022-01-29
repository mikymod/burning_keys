using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FidgetActivation : MonoBehaviour
{
    public static UnityEvent OnFidgetStop = new UnityEvent();

    [SerializeField] private float minSpeedForSpin, maxSpeedForSpin;
    [SerializeField] private float timeForSpin;
    [SerializeField] private Vector3 FidgetOffset;

    private float randomSpin;
    private bool isActive;
    private void Start()
    {
        isActive = false;
    }
    private void OnEnable()
    {
        //To Do = event for focus
    }
    void Update()
    {
        if (!isActive) return;
        gameObject.transform.position = Camera.main.transform.position + FidgetOffset;
        if (Input.GetMouseButtonDown(0))
        {
            var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //OnfidgetStartRoll.Invoke;
            }
        }
    }

    private void OnFidgetFocus()
    {
        isActive = true;
    }

    private void OnFidgetPressed()
    {
        StartCoroutine(FidgetSpin());
    }

    private IEnumerator FidgetSpin()
    {
        randomSpin = timeForSpin;
        while (randomSpin > 0)
        {
            randomSpin -= Time.deltaTime;
            yield return null;
        }
        OnFidgetStop.Invoke();
    }
}
