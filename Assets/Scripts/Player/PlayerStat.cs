﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;


public class PlayerStat : MonoBehaviour {

    public bool healthRegen;
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

    public PostProcessingProfile[] cameraEffects;

    public Inventory inventory;
    public Canvas pickUp;
    // Use this for initialization
    void Start () {
        currentHealth = 100;
        maxHealth = 100;
        currentMotivation = 100;
        maxMotivation = 100;
        currentHunger = 100;                                        //The start function will initialize all our variables
        maxHunger = 100;
        currentHydration = 100;
        maxHydration = 100;

        hungerTimer = 36f;
        hydrationTimer = 18f;
        regenTimer = 15f;
        motivationTimer = 2f;

        healthRegen = false;
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
            currentMotivation -= 1;                                                 //Decrement motivation after a certain amount of time
            motivationTimer = 2f;                                                   //Reset the timer.

            if (currentMotivation < 0) currentMotivation = 0;                       //check so that motivation doesn't go under 0
        }
        int index = (int)currentMotivation / 5;                                     //get the index for the greyinng out of the screen
        if (index >= 20) index = 19;                                                //check if the index is outside the array

        this.GetComponentInChildren<PostProcessingBehaviour>().profile = cameraEffects[index];              //set the grey scale
    }
    void HungerUpdate()
    {
        hungerTimer -= Time.deltaTime;

        if (hungerTimer <= 0)
        {
            currentHunger -= 1;                                                      //Decrement hunger after a certain amount of time
            hungerTimer = 36f;                                                       //Reset the timer
         
            if (currentHunger < 0) currentHunger = 0;                                //Check so that hunger doesn't go under 0
        }
    }
    void HydrationUpdate()
    {
        hydrationTimer -= Time.deltaTime;

        if (hydrationTimer <= 0)
        {
            currentHydration -= 1;                                                    //Decrement hydration after a certain amount of time
            hydrationTimer = 18f;                                                     //Reset the timer
            
            if (currentHydration < 0) currentHydration = 0;                           //Check so that hydration doesn't go under 0
        }
    }
    void HealthUpdate()
    {
        if (currentHunger >= 80 && currentHydration >= 80 && healthRegen)             //check to see if we have the correct conditions to regen health
        {
            regenTimer -= Time.deltaTime;
            if (regenTimer <= 0)
            {
                currentHealth += 1;                                                   //Increment health after a certain amount of time
                regenTimer = 15f;                                                     //Reset the timer

                if (currentHealth > maxHealth) currentHealth = maxHealth;             //Make sure we don't go over the max health
            }
        }
        else if (currentHunger <= 25 && currentHydration <= 25)                       //check to see if we have the correct conditions to decrement health
        {
            regenTimer -= Time.deltaTime;
            if (regenTimer <= 0)
            {
                currentHealth -= 1;                                                   //Decrement health after a certain amount of time
                regenTimer = 15f;                                                     //Reset the timer
                
                if (currentHealth < 0) currentHealth = 0;                             //Check so that health doesn't go under 0
            }
        }
        else
        {
            regenTimer = 15f;
        }
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
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("I'm hitting you.");

        if(other.tag == "Item")
        {
            //Debug.Log("You are in me.");
            if(Input.GetButtonDown("Submit"))
            {
                inventory.AddItem(other.GetComponent<Item>());
                Destroy(other.gameObject);
                pickUp.gameObject.SetActive(false);
                //Debug.Log("Added the item.");
            }
        }
    }
}
