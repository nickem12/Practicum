using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour {

    private int slotIndex;

    public void SetSlotIndex(int value)
    {
        slotIndex = value;
    }
    public int GetSlotIndex()
    {
        return slotIndex;
    }
}
