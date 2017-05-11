using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { MELEE, RANGE }

public enum Weapon { SHOTGUN, ASSAULTRIFLE, RIFLE, HANDGUN, ROCKETLAUNCHER, LMG, MELEE }

public class ItemData{

    private Weapon weapon;
    private WeaponType weaponType;

    public int currentAmmo;
    public int damage;
    public float fallOffRange;
    public int durability;

    private int maxAmmo;
    public GameObject weaponGameObject;
    public int allAmmo;

    public ItemData(WeaponType inWeaponType, Weapon inWeapon, GameObject inWeaponGameObject)
    {
        weapon = inWeapon;
        weaponType = inWeaponType;
        weaponGameObject = inWeaponGameObject;
        currentAmmo = 0;
        SetUpWeapon();
    }
    public ItemData(WeaponType inWeaponType, Weapon inWeapon, GameObject inWeaponGameObject, int bulletsInWeapon)
    {
        weapon = inWeapon;
        weaponType = inWeaponType;
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
                break;
            case Weapon.HANDGUN:
                maxAmmo = 12;
                break;
            case Weapon.LMG:
                maxAmmo = 25;
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
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
