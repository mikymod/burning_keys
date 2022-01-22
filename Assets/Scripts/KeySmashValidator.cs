using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
public class KeySmashDone : UnityEvent { }

public class KeySmashValidator : MonoBehaviour
{
    [SerializeField] public int numIterations = 6;
    [SerializeField] public float timeBetweenSmash = 0.3f;

    private TMP_Text textGO;
    private string key;
    private float timer = 0f;
    private bool canSmash = true;
    private int currentIterations = 0;

    public static UnityEvent KeySmashCompleted = new UnityEvent();

    private void Awake()
    {
        textGO = GetComponent<TMP_Text>();
        key = textGO.text;
    }

    private void Update()
    {
        KeyCode currentKeyCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), key.ToUpper().ToString());
        if (Input.GetKeyDown(currentKeyCode) && canSmash)
        {
            canSmash = false;
            StartCoroutine(EnableSmash());

            // TODO: start animation

            currentIterations++;
            Debug.Log($"Smash! iter: {currentIterations}");

            if (currentIterations == numIterations)
            {
                ChangeKey();
                currentIterations = 0;
                KeySmashCompleted.Invoke();
            }
        }
    }

    private void ChangeKey() 
    {
        key = ((char)Random.Range(65, 91)).ToString();
        textGO.text = key;
    } 

    private IEnumerator EnableSmash()
    {
        yield return new WaitForSeconds(timeBetweenSmash);
        canSmash = true;
    }

    public void Reset() => canSmash = true;
}
