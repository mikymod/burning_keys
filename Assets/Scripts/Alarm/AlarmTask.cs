using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmTask : MonoBehaviour
{
    [SerializeField] private bool isActive;

    
    private void OnEnable()
    {
        TaskManager.TaskStarted.AddListener(OnTaskStartedCallback);
        TaskManager.TaskFinished.AddListener(OnTaskFinishedCallback);
    }

    private void OnDisable()
    {
        TaskManager.TaskStarted.RemoveListener(OnTaskStartedCallback);
        TaskManager.TaskFinished.RemoveListener(OnTaskFinishedCallback);
    }

    private void OnTaskStartedCallback(TaskManager.TaskType taskType)
    {
        if (taskType == TaskManager.TaskType.Alarm)
        {
            isActive = true;
        }
    }

    private void OnTaskFinishedCallback(TaskManager.TaskType taskType)
    {
        if (taskType == TaskManager.TaskType.Alarm)
        {
            isActive = false;
            GameManager.AlarmEnd.Invoke();
        }
    }

    private void Update()
    {
        if (isActive)
        {
            //TODO Play NoiseSounds
            //TODO StressLevel++
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, LayerMask.NameToLayer("Interactable")))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("AlarmStopped");
                    TaskManager.TaskFinished.Invoke(TaskManager.TaskType.Alarm);
                }
            }
        }
        else
        {
            //TODO Stop NoiseSounds
            //TODO StressLevel--
        }
    }
}
