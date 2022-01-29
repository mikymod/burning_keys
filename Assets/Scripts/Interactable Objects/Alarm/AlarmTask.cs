using UnityEngine;

public class AlarmTask : MonoBehaviour
{
    [SerializeField] private bool isActive;
    private Collider coll;

    private void Awake()
    {
        coll = GetComponentInChildren<Collider>();
        coll.enabled = false;
    }
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
                if (Input.GetMouseButtonDown(0))
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
            return;
        }
        coll.enabled = true;
        isActive = true;
    }
    private void OnTaskFinishCallback()
    {
        if (!isActive)
        {
            return;
        }
        coll.enabled = false;
        isActive = false;
        GameManager.AlarmAdverterEnd.Invoke();
    }
}
