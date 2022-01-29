using UnityEngine;

public class AlarmTask : MonoBehaviour
{
    private bool isActive = false;
    private Collider coll;

    private void OnEnable()
    {
        GameManager.AlarmTaskStart.AddListener(OnTaskStartedCallback);
        GameManager.AlarmTaskFinished.AddListener(OnTaskFinishCallback);
    }
    
    private void OnDisable()
    {
        GameManager.AlarmTaskStart.RemoveListener(OnTaskStartedCallback);
        GameManager.AlarmTaskFinished.RemoveListener(OnTaskFinishCallback);
    }

    private void OnTaskStartedCallback()
    {
        if (isActive)
        {
            return;
        }

        isActive = true;
        GameManager.AlarmTaskFinished.Invoke();
    }

    private void OnTaskFinishCallback()
    {
        if (!isActive)
        {
            return;
        }

        isActive = false;
        GameManager.AlarmAdverterEnd.Invoke();
    }
}
