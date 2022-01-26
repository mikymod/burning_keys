using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_NightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    [SerializeField] private float time; //da 0-1 (giorn0, notte)
    [Tooltip("Day Duration")] [SerializeField] private float dayLength;
    [SerializeField] private float startTime;
    [SerializeField] private Vector3 noon;

    [Header("Sun")]
    [SerializeField] private Light sun;
    [SerializeField] private Gradient sunColor;
    [SerializeField] private AnimationCurve sunIntensity;


    [Header("Moon")]
    [SerializeField] private Light moon;
    [SerializeField] private Gradient moonColor;
    [SerializeField] private AnimationCurve moonIntensity;

    [Header("Light Pannel")]
    [SerializeField] private AnimationCurve lightingIntensityMultiplier;
    [SerializeField] private AnimationCurve reflectionsIntensityMultiplier;

    private float timeRate;
    private void Start()
    {
        timeRate = 1.0f / dayLength;
        time = startTime;
    }
    private void Update()
    {
        // Increment Time
        time += timeRate * Time.deltaTime;
        if (time >= 1) time = 0;

        // Light Rotation
        sun.transform.eulerAngles = (time - 0.25f) * noon * 4;
        moon.transform.eulerAngles = (time - 0.75f) * noon * 4;

        //Light Intensity
        sun.intensity = sunIntensity.Evaluate(time);
        moon.intensity = moonIntensity.Evaluate(time);

        //Change Colors
        sun.color = sunColor.Evaluate(time);
        moon.color = moonColor.Evaluate(time);


        //Light & Reflection Intensity
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(time);

    }
}
