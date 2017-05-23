using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeatherStates { PickWeather, SunnyWeather, ThunderWeather, MistWeather, OvercastWeather}            //defines all states of weather

[RequireComponent(typeof(AudioSource))]
public class DynamicWeather : MonoBehaviour {

    private Transform player;
    private Transform weather;
    public float weatherHeight = 15f;

    public float skyboxBlendValue;
    public float skyboxBlendTimer = 0.25f;
    private bool sunnyState;
    private bool thunderState;
    private bool mistState;
    private bool overcastState;

    public ParticleSystem cloudParticleSystem;
    public ParticleSystem mistParticleSystem;
    public ParticleSystem thunderParticleSystem;
    public ParticleSystem overcastParticleSystem;

    //private ParticleSystem.EmissionModule cloud;
    //private ParticleSystem.EmissionModule mist;
    //private ParticleSystem.EmissionModule thunder;
    //private ParticleSystem.EmissionModule overcast;

    public float switchWeatherTimer = 0f;
    public float resetWeatherTimer = 20f;

    public float audioFadeTime = 0.25f;

    public AudioClip sunnyAudio;
    public AudioClip thunderAudio;
    public AudioClip mistAudio;
    public AudioClip overcastAudio;

    public float lightDimTime = 0.1f;
    public float minIntensity = 0.0f;
    public float maxIntensity = 1f;
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
            }
            yield return null;
        }
    }
    void PickWeather()
    {
        switchWeather = Random.Range(0, 5);

        sunnyState = false;
        mistState = false;
        thunderState = false;
        overcastState = false;

        // cloud.enabled = false;
        //thunder.enabled = false;
        // overcast.enabled = false;
        // mist.enabled = false;

        cloudParticleSystem.Stop();
        thunderParticleSystem.Stop();
        mistParticleSystem.Stop();
        overcastParticleSystem.Stop();

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
        Debug.Log("Sunny");
        //cloud.enabled = true;
        if(!cloudParticleSystem.isPlaying)
            cloudParticleSystem.Play();

        sunnyState = true;

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
        Debug.Log("Rain");
        //thunder.enabled = true;
        if (!thunderParticleSystem.isPlaying)
            thunderParticleSystem.Play();

        thunderState = true;

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
        //mist.enabled = true;
        if (!mistParticleSystem.isPlaying)
            mistParticleSystem.Play();

        mistState = true;

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
        //overcast.enabled = true;
        if (!overcastParticleSystem.isPlaying)
            overcastParticleSystem.Play();

        overcastState = true;

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
    void SkyboxBlendManager()
    {
        if(sunnyState)
        {
            if(skyboxBlendValue == 0)
            {
                return;
            }
            skyboxBlendValue -= skyboxBlendTimer * Time.deltaTime;
            if(skyboxBlendValue<0)
            {
                skyboxBlendValue = 0;
            }
            RenderSettings.skybox.SetFloat("_Blend", skyboxBlendValue);

        }
        if (thunderState)
        {
            if (skyboxBlendValue == 1)
            {
                return;
            }
            skyboxBlendValue += skyboxBlendTimer * Time.deltaTime;
            if (skyboxBlendValue > 1)
            {
                skyboxBlendValue = 1;
            }
            RenderSettings.skybox.SetFloat("_Blend", skyboxBlendValue);

        }
        if (mistState)
        {
            if (skyboxBlendValue == .25f)
            {
                return;
            }
            if (skyboxBlendValue<0.25f)
            {
                skyboxBlendValue += skyboxBlendTimer * Time.deltaTime;
                if(skyboxBlendValue>0.25f)
                {
                    skyboxBlendValue = 0.25f;
                }
            }
            if(skyboxBlendValue>0.25f)
            {
                skyboxBlendValue -= skyboxBlendTimer * Time.deltaTime;
                if(skyboxBlendValue<0)
                {
                    skyboxBlendValue = 0;
                }
            }
            RenderSettings.skybox.SetFloat("_Blend", skyboxBlendValue);

        }
        if (overcastState)
        {
            if (skyboxBlendValue == .75f)
            {
                return;
            }
            if (skyboxBlendValue < 0.75f)
            {
                skyboxBlendValue += skyboxBlendTimer * Time.deltaTime;
                if (skyboxBlendValue > 0.75f)
                {
                    skyboxBlendValue = 0.75f;
                }
            }
            if (skyboxBlendValue > 0.75f)
            {
                skyboxBlendValue -= skyboxBlendTimer * Time.deltaTime;
                if (skyboxBlendValue < 0.75f)
                {
                    skyboxBlendValue = 0.75f;
                }
            }
            RenderSettings.skybox.SetFloat("_Blend", skyboxBlendValue);

        }
    }
}
