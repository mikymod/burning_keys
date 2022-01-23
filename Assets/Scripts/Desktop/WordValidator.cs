using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.Events;

public class WordValidator : MonoBehaviour
{
    private TMP_Text textGO;
    private string word;
    private int wordCurrentIndex = 0;

    public static UnityEvent WordCompleted = new UnityEvent();

    private void Awake()
    {
        textGO = GetComponent<TMP_Text>();
        word = textGO.text = GenerateWord();
    }

    private void Update()
    {
        KeyCode currentKeyCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), word.ToUpper()[wordCurrentIndex].ToString());
        if (Input.GetKeyDown(currentKeyCode))
        {
            wordCurrentIndex++;
            textGO.text = ColorWord();
            if (wordCurrentIndex == word.Length)
            {
                wordCurrentIndex = 0;
                WordCompleted.Invoke();
                word = textGO.text = GenerateWord();
            }
        }       
    }

    private string ColorWord()
    {
        var substring = word.Substring(0, wordCurrentIndex);
        var remaining = word.Substring(wordCurrentIndex);
        return $"<color=#000000>{substring}</color>{remaining}";
    }

    private string GenerateWord()
    {
        string[] words = {
            "war", "peace", "freedom", "slavery", "ignorance", "strength", "past", "future"
        };
        return words[Random.Range(0, words.Length-1)];
    }
}