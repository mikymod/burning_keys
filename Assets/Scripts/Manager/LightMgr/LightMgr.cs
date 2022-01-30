using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMgr : MonoBehaviour
{
    [SerializeField] private Light Lamp_Light;

    private Light[] AreaLight;
    private void Awake()
    {
        AreaLight = Lamp_Light.GetComponentsInChildren<Light>();

    }
    private void OnEnable()
    {
        Day_NightCycle.NightTime.AddListener(OnNightArrive);
        Day_NightCycle.DayTime.AddListener(OnDayArrive);
    }
    private void OnDisable()
    {
        Day_NightCycle.NightTime.RemoveListener(OnNightArrive);
        Day_NightCycle.DayTime.AddListener(OnDayArrive);
    }
    private void OnNightArrive()
    {
        Lamp_Light.intensity = 2;
        AreaLight[1].intensity = 0.40f;
    }
    private void OnDayArrive()
    {
        Lamp_Light.intensity = 0;
        AreaLight[1].intensity = 0;

    }

}
