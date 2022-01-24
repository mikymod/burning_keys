using UnityEngine;

public class AlarmTask : MonoBehaviour
{
    [SerializeField] private bool isActive;

    
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
    private void Update()
    {
        if (isActive)
        {
            //TODO Play NoiseSounds
            //TODO StressLevel++
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, LayerMask.NameToLayer("Interactable")))
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Debug.Log("AlarmStopped");
                    GameManager.AlarmTaskFinished.Invoke();
                }
            }
        }
        else
        {
            //TODO Stop NoiseSounds
            //TODO StressLevel--
        }
    }
    private void OnTaskStartedCallback()
    {
        if (isActive)
        {
            print("Alarm was alredy active");
            return;
        }
        print("Alarm Task iniziata");
        isActive = true;
    }
    private void OnTaskFinishCallback()
    {
        if (!isActive)
        {
            print("Alarm was alredy InActive");
            return;
        }
        print("Alarm Task finita");
        isActive = false;
        GameManager.AlarmAdverterEnd.Invoke();
    }
}
