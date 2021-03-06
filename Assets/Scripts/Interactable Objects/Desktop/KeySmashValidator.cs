using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
public class KeySmashDone : UnityEvent { }

public class KeySmashValidator : MonoBehaviour
{
    [SerializeField] public int numIterations = 6;

    private TMP_Text textGO;
    private string key;
    private float timer = 0f;
    private bool canSmash = true;
    private int currentIterations = 0;

    public static UnityEvent KeySmashCompleted = new UnityEvent();

    private void Awake()
    {
        textGO = GetComponent<TMP_Text>();
        key = textGO.text = GenerateKey();
    }

    private void OnEnable()
    {
        InputManager.KeyCodeInput.AddListener(OnKeyCodeInput);
    }

    private void OnDisable()
    {
        InputManager.KeyCodeInput.RemoveListener(OnKeyCodeInput);
    }

    private void OnKeyCodeInput(KeyCode keyCode)
    {
        KeyCode currentKeyCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), key.ToUpper().ToString());
        if (keyCode == currentKeyCode )
        {
            // TODO: start animation

            currentIterations++;

            if (currentIterations == numIterations)
            {
                key = textGO.text = GenerateKey();
                currentIterations = 0;
                KeySmashCompleted.Invoke();
            }
        }
    }

    private string GenerateKey() 
    {
        return ((char)Random.Range(65, 91)).ToString();
    } 
}
