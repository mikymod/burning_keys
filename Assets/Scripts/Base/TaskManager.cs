using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    public enum TaskType
    {
        Main,
        Alarm,
        Email,
        Phone,
        Count
    }

    public static UnityEvent<TaskType> TaskStarted = new UnityEvent<TaskType>();
    public static UnityEvent<TaskType> TaskFinished = new UnityEvent<TaskType>();

    [SerializeField] private LayerMask mask;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, mask))
            {
                // TODO: Select Game object with task
                //Camera in
                //Task
                if (hit.collider.gameObject.tag is "Alarm")
                {
                    TaskStarted.Invoke(TaskType.Alarm);
                }
            }
        }    
    }
}
