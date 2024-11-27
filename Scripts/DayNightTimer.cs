using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
public class DayNightTimer : MonoBehaviour
{
    enum  DayTime
    {
        morning = 7, evening = 18, night = 20, dawn = 5,
        oneHour = 360, oneDay = 24
    }

    Color32 morningColor = new Color32(255,255,255,255);
    Color32 eveningColor = new Color32(255, 60, 60, 255);
    Color32 nightColor = new Color32(40, 40, 40, 255);

    [SerializeField] Material skybox1;
    [SerializeField] Material skybox2;
    [SerializeField] Material skybox3;
    [SerializeField] Material skybox4;

    [SerializeField] Light directionalLight;   //아침엔 True

    private float _timeScale = 1;
    public float timeScale
    {
        get 
        {
           return _timeScale;
        }
    }

    private float hour;  //시간 
    private float second = 0;  //시간 

    // Start is called before the first frame update
    void Start()
    {
        hour = (int)DayTime.morning;
        StartCoroutine(CO_HourCheck());
    }

    IEnumerator CO_HourCheck()
    {
        while (true)
        {
            Check_Environment();

            yield return null;
        }
    }


    // 아침,새벽 해지기 전, 밤 
    void Check_Environment()
    {
        if ((hour >= (int)DayTime.dawn && hour < (int)DayTime.morning))
        {
            RenderSettings.skybox = skybox4;
            directionalLight.color = eveningColor;
        }
        else if(hour >= (int)DayTime.morning && hour < (int)DayTime.evening)
        {
            RenderSettings.skybox = skybox1;
            directionalLight.color = morningColor;
        }
        else if ((hour >= (int)DayTime.evening && hour < (int)DayTime.night))
        {
            RenderSettings.skybox = skybox2;
            directionalLight.color = eveningColor;
        }
        else if (hour >= (int)DayTime.night || hour < (int)DayTime.dawn)
        {
            RenderSettings.skybox = skybox3;
            directionalLight.color = nightColor;
        }

        second += (Time.deltaTime * timeScale);

        if (second >= (int)DayTime.oneHour) 
        {
            second = 0;

            hour++;
        }

        if (hour >= (int)DayTime.oneDay)
        {
            hour = 0;
        }


        RenderSettings.skybox.SetFloat("_Rotation", (float)DayTime.oneHour - second);

        float min = second / (int)DayTime.oneHour;
        float sunRotationX = (int)DayTime.oneHour * ((hour +  min) / (int)DayTime.oneDay);

        transform.eulerAngles = new Vector3((sunRotationX - 105), 65, 0);
    }

    public void SetTimeScale(float _scale)
    {
        _timeScale = _scale;
    }

    public void SetTimeScale(float _scale, float _hour, float _second) 
    {
        _timeScale = _scale;
        hour = _hour;
        second = _second;
    }
}