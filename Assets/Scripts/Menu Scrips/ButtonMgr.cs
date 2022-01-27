using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMgr : MonoBehaviour
{
    [SerializeField] private string nextScenestring;
    void Start()
    {
        if (nextScenestring == null) nextScenestring = "Test3DModelScene";
    }
    public void StartPressd()
    {
        SceneManager.LoadScene(nextScenestring);
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
}
