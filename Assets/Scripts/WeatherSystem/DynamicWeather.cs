using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeatherStates { PickWeather, SunnyWeather, ThunderWeather, MistWeather, OvercastWeather, Rain}            //defines all states of weather

[RequireComponent(typeof(AudioSource))]
public class DynamicWeather : MonoBehaviour {

    private Transform player;
    private Transform weather;
    public float weatherHeight = 15f;

    public ParticleSystem cloudParticleSystem;
    public ParticleSystem mistParticleSystem;
    public ParticleSystem thunderParticleSystem;
    public ParticleSystem overcastParticleSystem;
    public ParticleSystem rainParticleSystem;

    public float switchWeatherTimer = 0f;
    public float resetWeatherTimer = 20f;

    public float audioFadeTime = 0.25f;

    public AudioClip sunnyAudio;
    public AudioClip thunderAudio;
    public AudioClip mistAudio;
    public AudioClip overcastAudio;
    public AudioClip rainAudio;

    public float lightDimTime = 0.1f;
    public float minIntensity = 0.0f;
    public float maxIntensity = 1f;
    public float rainIntensity = .75f;
    public float mistIntensity = 0.5f;
    public float overcastIntensity = 0.25f;

    public WeatherStates weatherState;                               //Defines the naming convention of our weather states
    private int switchWeather;                                      //Defines naming convention of our switch 

    public Color sunFog;
    public Color thunderFog;
    public Color mistFog;
    public Color overcastFog;
    public float fogChangeSpeed = .1f;
    // Use this for initialization
    void Start () {
        GameObject playerGameobject = GameObject.FindGameObjectWithTag("Player");
        player = playerGameobject.transform;

        GameObject weatherGameObject = GameObject.FindGameObjectWithTag("Weather");
        weather = weatherGameObject.transform;

        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = 0.01f;

        RenderSettings.skybox.SetFloat("_Blend", 0);
        //cloud = cloudParticleSystem.emission;
        //thunder = thunderParticleSystem.emission;
        //mist = mistParticleSystem.emission;
        //overcast = overcastParticleSystem.emission;

        StartCoroutine(WeatherFSM());
	}
	
	// Update is called once per frame
	void Update () {

        switchWeatherTimer -= Time.deltaTime;
        if(switchWeatherTimer<=0)
        {
            resetWeatherTimer = Random.Range(30f, 60f);
            weatherState = WeatherStates.PickWeather;
            switchWeatherTimer = resetWeatherTimer;
        }

        weather.transform.position = new Vector3(player.position.x, player.position.y + weatherHeight, player.position.z);
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
                case WeatherStates.Rain:
                    RainWeather();
                    break;
            }
            yield return null;
        }
    }
    void PickWeather()
    {
        switchWeather = Random.Range(0, 6);

        cloudParticleSystem.Stop();
        thunderParticleSystem.Stop();
        mistParticleSystem.Stop();
        overcastParticleSystem.Stop();
        rainParticleSystem.Stop();

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
            case 5:
                weatherState = WeatherStates.Rain;
                break;
        }
    }

    void SunnyWeather()
    {
        Debug.Log("Sunny");

        if(!cloudParticleSystem.isPlaying)
            cloudParticleSystem.Play();

        if (GetComponent<Light>().intensity > maxIntensity)
            GetComponent<Light>().intensity -= Time.deltaTime * lightDimTime;

        if (GetComponent<Light>().intensity < maxIntensity)
            GetComponent<Light>().intensity += Time.deltaTime * lightDimTime;

        if (GetComponent<AudioSource>().volume > 0 && GetComponent<AudioSource>().clip != sunnyAudio)
            GetComponent<AudioSource>().volume -= Time.deltaTime * audioFadeTime;

        if(GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = sunnyAudio;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
        }

        if (GetComponent<AudioSource>().volume < 1 && GetComponent<AudioSource>().clip == sunnyAudio)
            GetComponent<AudioSource>().volume += Time.deltaTime * audioFadeTime;

        Color currentColor = RenderSettings.fogColor;

        RenderSettings.fogColor = Color.Lerp(currentColor, sunFog,fogChangeSpeed * Time.deltaTime );
    }

    void ThunderWeather()
    {
        Debug.Log("Thunder");

        if (!thunderParticleSystem.isPlaying)
            thunderParticleSystem.Play();

        if (GetComponent<Light>().intensity > minIntensity)
            GetComponent<Light>().intensity -= Time.deltaTime * lightDimTime;

        if (GetComponent<Light>().intensity < minIntensity)
            GetComponent<Light>().intensity += Time.deltaTime * lightDimTime;

        if (GetComponent<AudioSource>().volume > 0 && GetComponent<AudioSource>().clip != thunderAudio)
            GetComponent<AudioSource>().volume -= Time.deltaTime * audioFadeTime;

        if (GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = thunderAudio;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
        }

        if (GetComponent<AudioSource>().volume < 1 && GetComponent<AudioSource>().clip == thunderAudio)
            GetComponent<AudioSource>().volume += Time.deltaTime * audioFadeTime;

        Color currentColor = RenderSettings.fogColor;

        RenderSettings.fogColor = Color.Lerp(currentColor, thunderFog, fogChangeSpeed * Time.deltaTime);
    }

    void MistWeather()
    {
        Debug.Log("Misty");

        if (!mistParticleSystem.isPlaying)
            mistParticleSystem.Play();

        if (GetComponent<Light>().intensity > mistIntensity)
            GetComponent<Light>().intensity -= Time.deltaTime * lightDimTime;

        if (GetComponent<Light>().intensity < mistIntensity)
            GetComponent<Light>().intensity += Time.deltaTime * lightDimTime;

        if (GetComponent<AudioSource>().volume > 0 && GetComponent<AudioSource>().clip != mistAudio)
            GetComponent<AudioSource>().volume -= Time.deltaTime * audioFadeTime;

        if (GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = mistAudio;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
        }

        if (GetComponent<AudioSource>().volume < 1 && GetComponent<AudioSource>().clip == mistAudio)
            GetComponent<AudioSource>().volume += Time.deltaTime * audioFadeTime;

        Color currentColor = RenderSettings.fogColor;

        RenderSettings.fogColor = Color.Lerp(currentColor, mistFog, fogChangeSpeed * Time.deltaTime);
    }
    void OvercastWeather()
    {
        Debug.Log("Overcast");

        if (!overcastParticleSystem.isPlaying)
            overcastParticleSystem.Play();

        if (GetComponent<Light>().intensity > overcastIntensity)
            GetComponent<Light>().intensity -= Time.deltaTime * lightDimTime;

        if (GetComponent<Light>().intensity < overcastIntensity)
            GetComponent<Light>().intensity += Time.deltaTime * lightDimTime;

        if (GetComponent<AudioSource>().volume > 0 && GetComponent<AudioSource>().clip != overcastAudio)
            GetComponent<AudioSource>().volume -= Time.deltaTime * audioFadeTime;

        if (GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = overcastAudio;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
        }

        if (GetComponent<AudioSource>().volume < 1 && GetComponent<AudioSource>().clip == overcastAudio)
            GetComponent<AudioSource>().volume += Time.deltaTime * audioFadeTime;

        Color currentColor = RenderSettings.fogColor;

        RenderSettings.fogColor = Color.Lerp(currentColor, overcastFog, fogChangeSpeed * Time.deltaTime);

    }

    void RainWeather()
    {
        Debug.Log("Rain");

        if (!rainParticleSystem.isPlaying)
            rainParticleSystem.Play();

        if (GetComponent<Light>().intensity > rainIntensity)
            GetComponent<Light>().intensity -= Time.deltaTime * lightDimTime;

        if (GetComponent<Light>().intensity < rainIntensity)
            GetComponent<Light>().intensity += Time.deltaTime * lightDimTime;

        if (GetComponent<AudioSource>().volume > 0 && GetComponent<AudioSource>().clip != rainAudio)
            GetComponent<AudioSource>().volume -= Time.deltaTime * audioFadeTime;

        if (GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = rainAudio;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
        }

        if (GetComponent<AudioSource>().volume < 1 && GetComponent<AudioSource>().clip == rainAudio)
            GetComponent<AudioSource>().volume += Time.deltaTime * audioFadeTime;

        Color currentColor = RenderSettings.fogColor;

        RenderSettings.fogColor = Color.Lerp(currentColor, thunderFog, fogChangeSpeed * Time.deltaTime);
    }
}
