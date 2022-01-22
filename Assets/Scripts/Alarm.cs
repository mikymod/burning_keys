using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    bool isActive;

    private void OnEnable()
    {
        TaskManager.TaskStarted.AddListener(OnTaskStartedCallback);
    }

    private void OnDisable()
    {
        TaskManager.TaskStarted.RemoveListener(OnTaskStartedCallback);
    }

    private void OnTaskStartedCallback(TaskManager.TaskType taskType)
    {
        isActive = true;
    }

    private void OnTaskFinishedCallback(TaskManager.TaskType taskType)
    {
        isActive = false;
    }

    private void Update()
    {
        if (isActive)
        {
            //TODO Play NoiseSounds
            //TODO StressLevel++
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, LayerMask.NameToLayer("Alarm")))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("AlarmStopped");
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
