using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { WEAPON,CONSUMABLE}

public class Item : MonoBehaviour {

    public ItemType type;
    public Weapon weapon;
    public WeaponType weaponType;
    public GameObject weaponGameObject;

    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;

    public int maxSize;

    private string name;
    public Canvas pickUp;
    public Text text;

    public ItemData itemData;

    public int numberOfBullets;

    private void Start()
    {
        switch(type)
        {
            case ItemType.WEAPON:
                name = "Weapon";
                itemData = new ItemData(weaponType, weapon, weaponGameObject, numberOfBullets);
                break;
            case ItemType.CONSUMABLE:
                name = "Consumable";
                break;
        }
        //weaponGameObject.GetComponent<WeaponStats>().data = itemData;
    }

    public void Use()
    {
        switch(type)
        {
            case ItemType.CONSUMABLE:
                Debug.Log("I Used sword.");
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            pickUp.gameObject.SetActive(true);
            text.text = "Pickup " + name;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            pickUp.gameObject.SetActive(false);
        }
    }
}
