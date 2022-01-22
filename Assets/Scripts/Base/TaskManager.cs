using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    public enum TaskType
    {
        Smash,
        Typing,
        Alarm,
        Email,
        Phone,
        Count
    }

    public static UnityEvent<TaskType> TaskStarted = new UnityEvent<TaskType>();
    public static UnityEvent<TaskType> TaskFinished = new UnityEvent<TaskType>();
}
