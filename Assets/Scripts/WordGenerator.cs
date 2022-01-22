using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class WordGenerator : MonoBehaviour
{
    public string Word;
    [SerializeField] private TMP_Text textGO;

    private void OnValidate()
    {       
        Debug.Log(Word);
        textGO.text = Word;
    }
}
