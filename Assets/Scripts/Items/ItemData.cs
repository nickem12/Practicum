﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon { SHOTGUN, ASSAULTRIFLE, RIFLE, HANDGUN, ROCKETLAUNCHER, LMG, MELEE, STAPLER }

public class ItemData{

    public Weapon weapon;

    public int currentAmmo;
    public int damage;
    public float fallOffRange;
    public int durability;

    public int maxAmmo;
    public GameObject weaponGameObject;
    public int allAmmo;

    public ItemData(Weapon inWeapon, GameObject inWeaponGameObject)
    {
        weapon = inWeapon;
        weaponGameObject = inWeaponGameObject;
        currentAmmo = 0;
        SetUpWeapon();
    }
    public ItemData(Weapon inWeapon, GameObject inWeaponGameObject, int bulletsInWeapon)
    {
        weapon = inWeapon;
        weaponGameObject = inWeaponGameObject;
        currentAmmo = bulletsInWeapon;
        SetUpWeapon();
    }

    void SetUpWeapon()
    {
        switch (weapon)
        {
            case Weapon.ASSAULTRIFLE:
                maxAmmo = 30;
                damage = 10;
                break;
            case Weapon.HANDGUN:
                maxAmmo = 12;
                damage = 3;
                break;
            case Weapon.LMG:
                maxAmmo = 25;
                damage = 5;
                break;
            case Weapon.MELEE:
                maxAmmo = 0;
                break;
            case Weapon.RIFLE:
                maxAmmo = 10;
                break;
            case Weapon.ROCKETLAUNCHER:
                maxAmmo = 1;
                break;
            case Weapon.SHOTGUN:
                maxAmmo = 8;
                break;
            case Weapon.STAPLER:
                maxAmmo = 5;
                break;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
