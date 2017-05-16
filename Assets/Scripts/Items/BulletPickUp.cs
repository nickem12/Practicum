using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPickUp : MonoBehaviour {

    public int rifleAmmo;
    public int handgunAmmo;
    public int sniperAmmo;
    public int shotgunAmmo;
    public int rocketAmmo;
    public int smgAmmo;
    public int ammoAmount;
    public Weapon weapon;
    public string name;

    private Canvas pickupDisplay;
    private Text pickupText;
	// Use this for initialization
	void Start () {

        pickupDisplay = GameObject.FindGameObjectWithTag("PickUpCanvas").GetComponent<Canvas>();
        pickupText = GameObject.FindGameObjectWithTag("PickUpText").GetComponent<Text>();

        rifleAmmo = 0;
        handgunAmmo = 0;
        sniperAmmo = 0;
        shotgunAmmo = 0;
        rocketAmmo = 0;
        smgAmmo = 0;

        switch(weapon)
        {
            case Weapon.ASSAULTRIFLE:
                rifleAmmo = ammoAmount;
                name = "Rifle Ammo";
                break;
            case Weapon.HANDGUN:
                handgunAmmo = ammoAmount;
                name = "Handgun Ammo";
                break;
            case Weapon.LMG:
                smgAmmo = ammoAmount;
                name = "LMG Ammo";
                break;
            case Weapon.RIFLE:
                sniperAmmo = ammoAmount;
                name = "Sniper Ammo";
                break;
            case Weapon.ROCKETLAUNCHER:
                rocketAmmo = ammoAmount;
                name = "Rocket Launcher Ammo";
                break;
            case Weapon.SHOTGUN:
                shotgunAmmo = ammoAmount;
                name = "Shotgun Ammo";
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {

	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            pickupDisplay.enabled = true;
            pickupText.text = "Pickup " + name;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            pickupDisplay.enabled = false;
        }
    }
}
