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
    private float timer = .9f;
    // Use this for initialization
    void Start()
    {
        Weapon = GetComponent<WeaponStats>().data;
        
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            

            if(!initialized)
            {
                Weapon = GetComponent<WeaponStats>().data;
                initialized = true;
            }
            

            if (Weapon.currentAmmo > 0)
            {
                if(!triggered)
                {
                    anim.SetTrigger("Staple");
                    triggered = true;
                    source.clip = clips[0];
                    source.Play();

                    Weapon.currentAmmo--;
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
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                triggered = false;
                timer = .9f;
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
