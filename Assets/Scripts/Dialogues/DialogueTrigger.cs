using System.Collections;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogue;
    public float waitTime = 1f;

    public void Start()
    {
        StartCoroutine(StartDialogue());
    }

    public IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(waitTime);

        DialogueManager.DialogueUiOpen?.Invoke();
        DialogueManager.DialogueStart?.Invoke(dialogue);
    }
}
