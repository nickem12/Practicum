using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public float Bullet_Forward_Force;
    public ItemData Weapon;
    private bool getData = false;
    private bool muzzleFlashTrigger;
    private float flashTimer = .2f;
    private WeaponSoundManager weaponSoundManager;
    public GameObject muzzleFlash;

    // Use this for initialization
    void Start()
    {

        Weapon = GetComponent<WeaponStats>().data;
        weaponSoundManager = this.GetComponentInChildren<WeaponSoundManager>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if(!getData)
        {
            Weapon = GetComponent<WeaponStats>().data;
            getData = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Weapon.currentAmmo > 0)
            {
                muzzleFlashTrigger = true;

                GameObject t_bullet;
                t_bullet = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

                Rigidbody t_rigidbody;
                t_rigidbody = t_bullet.GetComponent<Rigidbody>();
                t_rigidbody.AddForce(transform.forward * Bullet_Forward_Force);

                t_bullet.GetComponent<Bullet>().damage = Weapon.damage;

                Debug.Log("bullet damage: " + t_bullet.GetComponent<Bullet>().damage);
                Destroy(t_bullet, 10f);

                Weapon.currentAmmo--;
                weaponSoundManager.PlayFiringSoundEffect((int)Weapon.weapon);
            }
        }


        if (muzzleFlashTrigger)
        {
            muzzleFlash.SetActive(true);

            flashTimer -= Time.deltaTime;
            if (flashTimer <= 0)
            {
                muzzleFlashTrigger = false;
                muzzleFlash.SetActive(false);
                flashTimer = 0.2f;
            }
        }

        if (Input.GetButtonDown("Reload"))
        {
            Debug.Log(Weapon.currentAmmo);
            Reload();
            Debug.Log(Weapon.currentAmmo);
        }

    }
    public void Reload()
    {
        PlayerStat pStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        int ammoNeeded = Weapon.maxAmmo - Weapon.currentAmmo;

        switch(Weapon.weapon)
        {
            case global::Weapon.ASSAULTRIFLE:
                if(pStat.rifleAmmo>=ammoNeeded)
                {
                    Weapon.currentAmmo = Weapon.maxAmmo;
                    pStat.rifleAmmo -= ammoNeeded;
                }
                else if(pStat.rifleAmmo>0)
                {
                    Weapon.currentAmmo += pStat.rifleAmmo;
                    pStat.rifleAmmo = 0;
                }
                weaponSoundManager.PlayReloadingSoundEffect((int)Weapon.weapon);
                break;
            case global::Weapon.HANDGUN:
                if (pStat.handgunAmmo >= ammoNeeded)
                {
                    Weapon.currentAmmo = Weapon.maxAmmo;
                    pStat.handgunAmmo -= ammoNeeded;
                }
                else if (pStat.handgunAmmo > 0)
                {
                    Weapon.currentAmmo += pStat.handgunAmmo;
                    pStat.handgunAmmo = 0;
                }
                weaponSoundManager.PlayReloadingSoundEffect((int)Weapon.weapon);
                break;
            case global::Weapon.LMG:
                if (pStat.smgAmmo >= ammoNeeded)
                {
                    Weapon.currentAmmo = Weapon.maxAmmo;
                    pStat.smgAmmo -= ammoNeeded;
                }
                else if (pStat.smgAmmo > 0)
                {
                    Weapon.currentAmmo += pStat.smgAmmo;
                    pStat.smgAmmo = 0;
                }
                weaponSoundManager.PlayReloadingSoundEffect((int)Weapon.weapon);
                break;
            case global::Weapon.RIFLE:
                if (pStat.sniperAmmo >= ammoNeeded)
                {
                    Weapon.currentAmmo = Weapon.maxAmmo;
                    pStat.sniperAmmo -= ammoNeeded;
                }
                else if (pStat.sniperAmmo > 0)
                {
                    Weapon.currentAmmo += pStat.sniperAmmo;
                    pStat.sniperAmmo = 0;
                }
                weaponSoundManager.PlayReloadingSoundEffect((int)Weapon.weapon);
                break;
            case global::Weapon.ROCKETLAUNCHER:
                if (pStat.rocketAmmo >= ammoNeeded)
                {
                    Weapon.currentAmmo = Weapon.maxAmmo;
                    pStat.rocketAmmo -= ammoNeeded;
                }
                else if (pStat.rocketAmmo > 0)
                {
                    Weapon.currentAmmo += pStat.rifleAmmo;
                    pStat.rocketAmmo = 0;
                }
                weaponSoundManager.PlayReloadingSoundEffect((int)Weapon.weapon);
                break;
            case global::Weapon.SHOTGUN:
                if (pStat.shotgunAmmo >= ammoNeeded)
                {
                    Weapon.currentAmmo = Weapon.maxAmmo;
                    pStat.shotgunAmmo -= ammoNeeded;
                }
                else if (pStat.shotgunAmmo > 0)
                {
                    Weapon.currentAmmo += pStat.shotgunAmmo;
                    pStat.shotgunAmmo = 0;
                }
                weaponSoundManager.PlayReloadingSoundEffect((int)Weapon.weapon);
                break;
            case global::Weapon.STAPLER:
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
                break;
        }
    }
}
