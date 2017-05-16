using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { WEAPON,CONSUMABLE}
public enum ConsumableType { FOOD, HYDRATION, HEALTH, NULL};
public class Item : MonoBehaviour {

    public ItemType type;
    public Weapon weapon;
    public WeaponType weaponType;
    public GameObject weaponGameObject;
    public ConsumableType consumableType;
    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;

    private GameObject player;

    public int maxSize;
    private string name;
    private Canvas pickUp;
    private Text text;
    public int statIncrease;

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
        player = GameObject.FindGameObjectWithTag("Player");
        //weaponGameObject.GetComponent<WeaponStats>().data = itemData;
        pickUp = GameObject.FindGameObjectWithTag("PickUpCanvas").GetComponent<Canvas>();
        text = GameObject.FindGameObjectWithTag("PickUpText").GetComponent<Text>();
    }

    public void Use()
    {
        switch(consumableType)
        {
            case ConsumableType.FOOD:
                player.GetComponent<PlayerStat>().AddToHunger(statIncrease);
                break;
            case ConsumableType.HEALTH:
                player.GetComponent<PlayerStat>().AddToHealth(statIncrease);
                break;
            case ConsumableType.HYDRATION:
                player.GetComponent<PlayerStat>().AddToHydration(statIncrease);
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
