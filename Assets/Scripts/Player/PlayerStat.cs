using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {

    public float currentHealth;
    private float maxHealth;
    [SerializeField]
    private float currentMotivation;
    private float maxMotivation;
    [SerializeField]
    private float currentHunger;
    private float maxHunger;
    [SerializeField]
    private float currentHydration;
    private float maxHydration;

    private float hungerTimer;
    private float hydrationTimer;
    private float motivationTimer;
    private float regenTimer;

    public Texture text1;
    public Texture text2;
    public Texture text3;
    // Use this for initialization
    void Start () {
        currentHealth = 100;
        maxHealth = 100;
        currentMotivation = 100;
        maxMotivation = 100;
        currentHunger = 100;
        maxHunger = 100;
        currentHydration = 100;
        maxHydration = 100;

        hungerTimer = 36f;
        hydrationTimer = 18f;
        regenTimer = 15f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        HydrationUpdate();
        HungerUpdate();
        MotivationUpdate();
        HealthUpdate();

	}
    void MotivationUpdate()
    {
        motivationTimer -= Time.deltaTime;

        if(motivationTimer<=0)
        {
            currentMotivation -= 1;
            motivationTimer = 36f;

            if (currentMotivation < 0) currentMotivation = 0;
        }
        Debug.Log(currentMotivation);
    }
    void HungerUpdate()
    {
        hungerTimer -= Time.deltaTime;

        if (hungerTimer <= 0)
        {
            currentHunger -= 1;
            hungerTimer = 36f;
         
            if (currentHunger < 0) currentHunger = 0;
        }
        //Debug.Log(currentHunger);
    }
    void HydrationUpdate()
    {
        hydrationTimer -= Time.deltaTime;

        if (hydrationTimer <= 0)
        {
            currentHydration -= 1;
            hydrationTimer = 18f;
            
            if (currentHydration < 0) currentHydration = 0;
        }
        //Debug.Log(currentHydration);
    }
    void HealthUpdate()
    {
        if (currentHunger >= 80 && currentHydration >= 80)
        {
            regenTimer -= Time.deltaTime;
            if (regenTimer <= 0)
            {
                currentHealth += 1;
                regenTimer = 15f;

                if (currentHealth > 100) currentHealth = 100;
                //Debug.Log(currentHealth);
            }
        }
        else if (currentHunger <= 25 && currentHydration <= 25)
        {
            regenTimer -= Time.deltaTime;
            if (regenTimer <= 0)
            {
                currentHealth -= 1;
                regenTimer = 15f;
                
                if (currentHealth < 0) currentHealth = 0;
                //Debug.Log(currentHealth);
            }
        }
        else
        {
            regenTimer = 15f;
        }
        //Debug.Log(currentHealth);
    }
    void OnGUI()
    {
        if (currentHealth<75)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), text1);
        }
        if (currentHealth < 50)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), text2);
        }
        if (currentHealth < 25)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), text3);
        }

    }
    void NotifyPlayer(float playerStat)
    {

    }
}
