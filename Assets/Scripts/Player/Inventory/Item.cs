using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { SWORD,HAND}

public class Item : MonoBehaviour {

    public ItemType type;

    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;

    public int maxSize;

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
}
