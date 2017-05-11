using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour {

    private int slotIndex;
    public GameObject dropDownCanvas;
    public ItemData data;

    public void SetSlotIndex(int value)
    {
        slotIndex = value;
        //Debug.Log(slotIndex);
    }
    public int GetSlotIndex()
    {
        return slotIndex;
    }
}
