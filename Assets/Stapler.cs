using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stapler : MonoBehaviour
{

    private Animator anim;
    private WeaponSoundManager weaponSoundManager;
    public ItemData Weapon;
    private bool initialized = false;
    // Use this for initialization
    void Start()
    {
        Weapon = GetComponent<WeaponStats>().data;
        weaponSoundManager = this.GetComponentInChildren<WeaponSoundManager>();
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
                weaponSoundManager.PlayFiringSoundEffect((int)Weapon.weapon);

                anim.SetTrigger("Staple");
                Weapon.currentAmmo--;
                
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
        weaponSoundManager.PlayReloadingSoundEffect((int)Weapon.weapon);
    }
}
