using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    static public UnityAction DialogueUiOpen;
    static public UnityAction DialogueUiClose;
    static public UnityAction<DialogueData> DialogueStart;
    static public UnityAction<string> NextSentence;
    static public UnityAction DialogueFinish;

    private Queue<string> sentences;

    public GameObject dialogue;

    private void Awake()
    {
        sentences = new Queue<string>();
    }

    private void OnEnable()
    {
        DialogueUiOpen += OnDialogueUiOpen;
        DialogueUiClose += OnDialogueUiClose;
        DialogueStart += OnDialogueStart;
    }

    private void OnDisable()
    {
        DialogueUiOpen -= OnDialogueUiOpen;
        DialogueUiClose -= OnDialogueUiClose;
        DialogueStart -= OnDialogueStart;
    }

    //
    private void OnDialogueUiOpen() => dialogue.SetActive(true);

    //
    private void OnDialogueUiClose() => dialogue.SetActive(false);

    //
    private void OnDialogueStart(DialogueData dialogue)
    {
        InputManager.MouseInput.AddListener(OnMouseInput);

        sentences.Clear();
        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    //
    public void DisplayNextSentence()
    {

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        var sentence = sentences.Dequeue();
        NextSentence.Invoke(sentence);
    }

    //
    private void EndDialogue()
    {
        InputManager.MouseInput.RemoveListener(OnMouseInput);

        DialogueFinish?.Invoke();

        StartCoroutine(DisableDialogue());
    }

    //
    private void OnMouseInput() => DisplayNextSentence();

    //
    private IEnumerator DisableDialogue()
    {
        yield return new WaitForSeconds(0.5f);
        DialogueUiClose?.Invoke();
    }
}

[Serializable]
public class DialogueData
{
    public string name;
    [TextArea(3, 10)] public string[] sentences;
}