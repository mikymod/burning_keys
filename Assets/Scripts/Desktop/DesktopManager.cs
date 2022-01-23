using UnityEngine;

public class DesktopManager : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private GameObject wordPrefab;
    [SerializeField] private int numKeySmashes = 3;
    [SerializeField] private int numWordsToType = 3;
    private int numKeysSmashCompleted = 0;
    private int numWordsTypedCompleted = 0;

    private void OnEnable()
    {
        KeySmashValidator.KeySmashCompleted.AddListener(OnKeySmashDone);
        WordValidator.WordCompleted.AddListener(OnWordDone);
    }

    private void OnDisable()
    {
        KeySmashValidator.KeySmashCompleted.RemoveListener(OnKeySmashDone);
        WordValidator.WordCompleted.RemoveListener(OnWordDone);
    }

    private void OnKeySmashDone()
    {
        numKeysSmashCompleted++;

        if (numKeysSmashCompleted == numKeySmashes)
        {
            keyPrefab.SetActive(false);
            wordPrefab.SetActive(true);
            numKeysSmashCompleted = 0;
        }
    }

    private void OnWordDone()
    {
        numWordsTypedCompleted++;

        if (numWordsTypedCompleted == numWordsToType)
        {
            wordPrefab.SetActive(false);
            keyPrefab.SetActive(true);
            keyPrefab.GetComponent<KeySmashValidator>().Reset();

            numWordsTypedCompleted = 0;
        }
    }
}
