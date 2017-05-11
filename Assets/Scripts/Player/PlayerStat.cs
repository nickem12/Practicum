using System.Collections;
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
    private PlayerStatsSound statsSound;
    private UISoundManager uiSoundManager;
    private bool playerIsDead;

    public int shotgunAmmo;
    public int rifleAmmo;
    public int sniperAmmo;
    public int smgAmmo;
    public int rocketAmmo;

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
        statsSound = GameObject.FindGameObjectWithTag("PlayerSoundManager").GetComponent<PlayerStatsSound>();
        uiSoundManager = GameObject.FindGameObjectWithTag("PlayerSoundManager").GetComponent<UISoundManager>();
        playerIsDead = false;
    }

    private void Update()
    {
        PlayerInput();
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
        if (currentHunger < 75 && currentHunger >= 40)
        {
            if (!statsSound.audioSource.isPlaying || statsSound.audioSource.clip != statsSound.stomachGrowling1)
            {
                statsSound.audioSource.Stop();
                statsSound.PlayAudioClip(5);
            }
        }
        else if (currentHunger < 40)
        {
            if (!statsSound.audioSource.isPlaying || statsSound.audioSource.clip != statsSound.stomachGrowling2)
            {
                statsSound.audioSource.Stop();
                statsSound.PlayAudioClip(6);
            }
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
        if(currentHydration < 75 && currentHydration >= 40)
        {
            if (!statsSound.audioSource.isPlaying || statsSound.audioSource.clip != statsSound.breathing1)
            {
                statsSound.audioSource.Stop();
                statsSound.PlayAudioClip(3);
            }
        }
        else if(currentHydration < 40)
        {
            if (!statsSound.audioSource.isPlaying || statsSound.audioSource.clip != statsSound.breathing2)
            {
                statsSound.audioSource.Stop();
                statsSound.PlayAudioClip(4);
            }
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

                if (currentHealth < 0)
                {
                    currentHealth = 0;                                                //Check so that health doesn't go under 0
                    statsSound.DeathSound();
                    playerIsDead = true;
                }
            }
        }
        else
        {
            regenTimer = 15f;
        }
    }
    void OnGUI()
    {
        if (currentHealth<75 && currentHealth >= 50)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), text1);
            if (!statsSound.audioSource.isPlaying || statsSound.audioSource.clip != statsSound.heartBeat1)
            {
                statsSound.audioSource.Stop();
                statsSound.PlayAudioClip(0);
            }
        }
        if (currentHealth < 50 && currentHealth >= 25)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), text2);
            if (!statsSound.audioSource.isPlaying || statsSound.audioSource.clip != statsSound.heartBeat2)
            {
                statsSound.audioSource.Stop();
                statsSound.PlayAudioClip(1);
            }
        }
        if (currentHealth < 25)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), text3);
            if (!statsSound.audioSource.isPlaying || statsSound.audioSource.clip != statsSound.heartBeat3)
            {
                statsSound.audioSource.Stop();
                statsSound.PlayAudioClip(2);
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("I'm hitting you.");

        if(other.tag == "Item")                                         //Colision with an item
        {
            //Debug.Log("You are in me.");
            if(Input.GetButtonDown("Submit"))                           //hits the pick up button
            {
                inventory.AddItem(other.GetComponent<Item>());          //adds the item and destroys the game object
                Destroy(other.gameObject);
                pickUp.gameObject.SetActive(false);
                //Debug.Log("Added the item.");
                //GameObject.FindGameObjectWithTag("Weapon_Slot").GetComponent<Equip>().EquipWeapon(other.GetComponent<Item>().weaponGameObject);

            }
        }
    }
    private void PlayerInput()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            uiSoundManager.PlayAudioClip(2);
        }
    }

    
}
