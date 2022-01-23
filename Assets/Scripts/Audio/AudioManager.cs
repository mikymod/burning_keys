using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        float time = 0f;
        float duration = fadeTime;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 1f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 1f;
    }

    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float time = 0f;
        float duration = fadeTime;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 0f;
    }

    void MusicMix()
    {
        //TODO change music on stress value change
    }
}
