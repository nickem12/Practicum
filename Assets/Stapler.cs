using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stapler : MonoBehaviour
{

    private Animator anim;
    public AudioSource source;
    public AudioClip[] clips;
    public ItemData Weapon;
    private bool initialized = false;
    private bool triggered = false;
    private float timer = 2.9f;
    private GameObject thePlayer;
    public int stapleCount = 0;
    public bool doneStapling = false;

    // Use this for initialization
    void Start()
    {
        Weapon = GetComponent<WeaponStats>().data;
        
        anim = GetComponent<Animator>();

        thePlayer = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            
            if(thePlayer.GetComponent<FirstPersonControllerBehavior>().canMove == false)
            {
                if (!initialized)
                {
                    Weapon = GetComponent<WeaponStats>().data;
                    initialized = true;
                }


                if (Weapon.currentAmmo > 0)
                {
                    if (!triggered)
                    {
                        triggered = true;
                        anim.SetTrigger("Staple");
                        source.clip = clips[0];
                        source.Play();
                        stapleCount++;
                        Weapon.currentAmmo--;
                    }
                    if(stapleCount == 2)
                    {
                        Debug.Log("doneStapling = true");
                        doneStapling = true;
                    }
                }
            }
        }
        if (Input.GetButtonDown("Reload"))
        {
            if (!initialized)
            {
                Weapon = GetComponent<WeaponStats>().data;
                initialized = true;
            }
            Debug.Log(Weapon.currentAmmo);
            Reload();
            Debug.Log(Weapon.currentAmmo);
        }

        if(triggered)
        {
            if (!doneStapling)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    triggered = false;
                    timer = 2.9f;
                }
            }
        }

    }

    public void Reload()
    {

        PlayerStat pStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        int ammoNeeded = Weapon.maxAmmo - Weapon.currentAmmo;
        if (pStat.staples >= ammoNeeded)
        {
            Weapon.currentAmmo = Weapon.maxAmmo;
            pStat.staples -= ammoNeeded;
        }
        else if (pStat.staples > 0)
        {
            Weapon.currentAmmo += pStat.staples;
            pStat.staples = 0;
        }
        source.clip = clips[1];
        source.Play();
    }
}
