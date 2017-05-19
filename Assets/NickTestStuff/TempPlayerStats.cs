using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerStats : MonoBehaviour {

    public Inventory inventory;
    public Canvas pickUp;
    private UISoundManager uiSoundManager;

    public int shotgunAmmo;
    public int rifleAmmo;
    public int sniperAmmo;
    public int smgAmmo;
    public int rocketAmmo;
    public int handgunAmmo;
    public int staples;

    private int itemCounter = 0;
    private GameObject t_object = null;

    // Use this for initialization
    void Start()
    {
        uiSoundManager = GameObject.FindGameObjectWithTag("PlayerSoundManager").GetComponent<UISoundManager>();
    }

    private void Update()
    {
        PlayerInput();
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (t_object == null)
        {
            itemCounter = 0;
        }

    }
    private void OnTriggerStay(Collider other)
    {

        //Debug.Log("I'm hitting you.");

        if (other.tag == "Item")                                         //Colision with an item
        {
            //Debug.Log("You are in me.");
            if (Input.GetButtonDown("Submit"))                           //hits the pick up button
            {
                if (itemCounter > 0)
                {

                    return;
                }
                inventory.AddItem(other.GetComponent<Item>());          //adds the item and destroys the game object
                t_object = other.gameObject;
                Destroy(other.gameObject);
                pickUp.GetComponent<Canvas>().enabled = false;
                Debug.Log("Added the item.");
                itemCounter++;

                //GameObject.FindGameObjectWithTag("Weapon_Slot").GetComponent<Equip>().EquipWeapon(other.GetComponent<Item>().weaponGameObject);

            }
        }
        if (other.tag == "Ammo")
        {
            if (Input.GetButtonDown("Submit"))
            {
                BulletPickUp temp = other.GetComponent<BulletPickUp>();
                shotgunAmmo += temp.shotgunAmmo;
                rifleAmmo += temp.rifleAmmo;
                sniperAmmo += temp.sniperAmmo;
                rocketAmmo += temp.rocketAmmo;
                smgAmmo += temp.smgAmmo;
                handgunAmmo += temp.handgunAmmo;
                staples += temp.staples;

                Destroy(other.gameObject);
                pickUp.GetComponent<Canvas>().enabled = false;
            }
        }
    }
    private void PlayerInput()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            uiSoundManager.PlayAudioClip(2);
        }
    }
}
