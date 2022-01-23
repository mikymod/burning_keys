// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.Events;

// public class WorldDone : UnityEvent { }

// public class MainDesktop : MonoBehaviour {
//     public TMP_Text TextGO;
//     public List<string> Words;
//     public int SingleSmashScore = 1, ChangeSmashScore = 5, QuitSmashScore = 30;

//     private int wordCurrentIndex = 0;

//     // TODO: Debug - remove SerilizedField
//     [SerializeField]private bool isSmashing = false;
//     [SerializeField]private bool isTyping = false;
//     private string word;
//     private int localSmashScore;

//     private void OnEnable() {
//         TaskManager.TaskFinished.AddListener(OnTaskFinishedCallback);
//         TaskManager.TaskStarted.AddListener(OnTaskStartedCallback);
//     }

//     private void OnDisable() {
//         TaskManager.TaskFinished.RemoveListener(OnTaskFinishedCallback);
//         TaskManager.TaskStarted.RemoveListener(OnTaskStartedCallback);
//     }

//     private void OnTaskFinishedCallback(TaskManager.TaskType oldTaskType) {
//         if (oldTaskType == TaskManager.TaskType.Smash) {
//             localSmashScore = 0;
//             TaskManager.TaskStarted.Invoke(TaskManager.TaskType.Typing);
//         } else if (oldTaskType == TaskManager.TaskType.Typing) {
//             TaskManager.TaskStarted.Invoke(TaskManager.TaskType.Smash);
//         }
//     }

//     private void OnTaskStartedCallback(TaskManager.TaskType currTaskType) {
//         if (currTaskType == TaskManager.TaskType.Smash) {
//             wordCurrentIndex = 0;
//             NewString(false);
//             isSmashing = true;
//             isTyping = false;
//         } else if (currTaskType == TaskManager.TaskType.Typing) {
//             NewString(true);
//             wordCurrentIndex = 0;
//             isSmashing = false;
//             isTyping = true;
//         }
//     }

//     private void NewString(bool isWord) {
//         if (isWord) {
//             word = Words[Random.Range(0, Words.Count)].ToUpper();
//         } else {
//             word = ((char)Random.Range(65, 91)).ToString();
//         }
//         TextGO.text = word;
//     }

//     private void Update() {
//         if (isTyping || isSmashing) {
//             KeyCode currentKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), word[wordCurrentIndex].ToString());
//             if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))) {
//                 if (Input.GetKeyDown(currentKeyCode)) {
//                     if (isSmashing) {
//                         //TODO: Punteggio globale
//                         localSmashScore += SingleSmashScore;
//                         if (localSmashScore % ChangeSmashScore == 0) {
//                             NewString(false);
//                             if (localSmashScore >= QuitSmashScore) {
//                                 TaskManager.TaskFinished.Invoke(TaskManager.TaskType.Smash);
//                             }
//                         }
//                     } else {
//                         wordCurrentIndex++;
//                         TextGO.text = ColorWord();
//                         if (wordCurrentIndex == word.Length) {
//                             TaskManager.TaskFinished.Invoke(TaskManager.TaskType.Typing);
//                         }
//                     }
//                 }
//                 /*else {
//                     MainDesktop.WordCurrentIndex = Mathf.Max(0, MainDesktop.WordCurrentIndex - 1);
//                     textGO.text = ColorWord();
//                 }*/
//             }
//         }

//     }
//     private string ColorWord(bool error = false) {
//         string doneString = word.Substring(0, wordCurrentIndex);
//         string errorstring = "";
//         if (error) {
//             errorstring = word.Substring(wordCurrentIndex, 1);
//         }
//         string remainingString = word.Substring(error ? wordCurrentIndex + 1 : wordCurrentIndex);
//         return $"<color=#000000>{doneString}</color><color=#ff0000>{errorstring}</color>{remainingString}";
//     }
// }
