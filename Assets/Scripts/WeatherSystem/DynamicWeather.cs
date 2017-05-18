using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherStates { PickWeather, SunnyWeather, ThunderWeather, MistWeather, OvercastWeather}            //defines all states of weather

public class DynamicWeather : MonoBehaviour {

    public ParticleSystem cloudParticleSystem;
    public ParticleSystem mistParticleSystem;
    public ParticleSystem thunderParticleSystem;
    public ParticleSystem overcastParticleSystem;


    public float switchWeatherTimer = 0f;
    public float resetWeatherTimer = 20f;

    public WeatherStates weatherState;                               //Defines the naming convention of our weather states
    private int switchWeather;                                      //Defines naming convention of our switch 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        switchWeatherTimer -= Time.deltaTime;
        if(switchWeatherTimer<=0)
        {
            weatherState = WeatherStates.PickWeather;
            switchWeatherTimer = resetWeatherTimer;
        }
	}
    IEnumerator WeatherFSM()
    {
        while(true)                                                                    //while the weather state machine is active
        {
            switch(weatherState)                                                       //switch the weather states
            {
                case WeatherStates.PickWeather:
                    PickWeather();
                    break;
                case WeatherStates.SunnyWeather:
                    SunnyWeather();
                    break;
                case WeatherStates.OvercastWeather:
                    OvercastWeather();
                    break;
                case WeatherStates.MistWeather:
                    MistWeather();
                    break;
                case WeatherStates.ThunderWeather:
                    ThunderWeather();
                    break;
            }
            yield return null;
        }
    }
    void PickWeather()
    {
        switchWeather = Random.Range(0, 4);

        cloudParticleSystem.enableEmission = false;
        thunderParticleSystem.enableEmission = false;
        overcastParticleSystem.enableEmission = false;
        mistParticleSystem.enableEmission = false;

        switch (switchWeather)
        {
            case 1:
                weatherState = WeatherStates.SunnyWeather;
                break;
            case 2:
                weatherState = WeatherStates.MistWeather;
                break;
            case 3:
                weatherState = WeatherStates.OvercastWeather;
                break;
            case 4:
                weatherState = WeatherStates.ThunderWeather;
                break;
        }
    }

    void SunnyWeather()
    {

    }

    void ThunderWeather()
    {

    }

    void MistWeather()
    {

    }
    void OvercastWeather()
    {

    }

}
