using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TMP_Text dialogueName;
    public TMP_Text dialogueSentence;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }
    
    private void OnEnable()
    {
        DialogueManager.DialogueStart += OnDialogueStart;
        DialogueManager.NextSentence += OnNextSentence;
        DialogueManager.DialogueFinish += OnDialogueFinish;
    }

    private void OnDisable()
    {
        DialogueManager.DialogueStart -= OnDialogueStart;
        DialogueManager.NextSentence -= OnNextSentence;    
        DialogueManager.DialogueFinish -= OnDialogueFinish;
    }

    //
    private void OnDialogueStart(DialogueData data) 
    {
        dialogueName.text = data.name;
        animator.SetBool("open", true);
    }

    //
    private void OnNextSentence(string data) => StartCoroutine(TypeSentence(data));

    //
    private void OnDialogueFinish() => animator.SetBool("open", false);

    //
    private IEnumerator TypeSentence(string data)
    {
        dialogueSentence.text = "";
        foreach(var letter in data.ToCharArray())
        {
            dialogueSentence.text += letter;
            yield return null;
        }
    }
}
