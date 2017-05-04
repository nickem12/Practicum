using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { SWORD,HAND}

public class Item : MonoBehaviour {

    public ItemType type;

    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;

    public int maxSize;

    private string name;
    public Canvas pickUp;
    public Text text;

    private void Start()
    {
        switch(type)
        {
            case ItemType.SWORD:
                name = "Sword";
                break;
            case ItemType.HAND:
                name = "Hand";
                break;
        }
    }

    public void Use()
    {
        switch(type)
        {
            case ItemType.SWORD:
                Debug.Log("I Used something.");
                break;
            case ItemType.HAND:
                Debug.Log("I Used something.");
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
