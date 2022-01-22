using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class WordValidator : MonoBehaviour
{
    private TMP_Text textGO;
    private string word;
    private int wordCurrentIndex = 0;

    private void Awake()
    {
        textGO = GetComponent<TMP_Text>();
        word = textGO.text;
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
                Debug.Log("Done!");
            }
        }       
    }

    private string ColorWord()
    {
        var substring = word.Substring(0, wordCurrentIndex);
        var remaining = word.Substring(wordCurrentIndex);
        return $"<color=#000000>{substring}</color>{remaining}";
    }
}
