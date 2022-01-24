using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource relaxMusicSource;
    [SerializeField] private AudioSource metalMusicSource;
    [SerializeField] private AudioSource stingerForRelaxMusicSource;
    [SerializeField] private AudioSource stingerForMetalMusicSource;

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

    [Range(0, 1)]
    public float stressValue;

    private void Update()
    {
        MusicMix();
    }

    void MusicMix()
    {
        //TODO change music on stress value change
        if (stressValue <= 0.5f)
        {
            if (metalMusicSource.isPlaying)
            {
                stingerForMetalMusicSource.Stop();
                metalMusicSource.Stop();
            }
            if (!relaxMusicSource.isPlaying)
            {
                stingerForRelaxMusicSource.Play();
                relaxMusicSource.PlayDelayed(1.0f);
            }
        }
        if (stressValue >= 0.5f)
        {
            if (relaxMusicSource.isPlaying)
            {
                stingerForRelaxMusicSource.Stop();
                relaxMusicSource.Stop();
            }
            if (!metalMusicSource.isPlaying)
            {
                stingerForMetalMusicSource.Play();
                metalMusicSource.PlayDelayed(2.2f);
            }
        }
    }
}
