using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private RectTransform inventoryRect;

    private float inventoryWidth;
    private float inventoryHeight;

    public int slots;
    public int rows;

    public float paddingLeft;
    public float paddingTop;

    public float slotSize;

    public GameObject slotPrefab;

    private List<GameObject> allSlots;

    private int emptySlots;
	// Use this for initialization
	void Start () {

        CreateLayout();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateLayout()
    {
        allSlots = new List<GameObject>();

        emptySlots = slots;

        inventoryWidth = (slots / rows) * (slotSize + paddingLeft) + paddingLeft;
        inventoryHeight = rows * (slotSize + paddingTop) + paddingTop;

        inventoryRect = GetComponent<RectTransform>();

        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHeight);

        int columns = slots / rows;

        for(int y = 0; y<rows;y++)
        {
            for(int x = 0; x<columns; x++)
            {
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);

                RectTransform slotrect = newSlot.GetComponent<RectTransform>();

                newSlot.name = "Slot";

                newSlot.transform.SetParent(this.transform.parent);

                slotrect.localPosition = inventoryRect.localPosition + new Vector3(paddingLeft * (x + 1) + (slotSize * x), -paddingTop * (y + 1) - (slotSize * y), 0);

                slotrect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
                slotrect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

                allSlots.Add(newSlot);
            }
        }
    }
    public bool AddItem(Item item)
    {
        Debug.Log("Called This Function.");
        if(item.maxSize == 1)
        {
            PlaceEmpty(item);
            return true;
        }
        return false;
    }
    private bool PlaceEmpty(Item item)
    {
        if (emptySlots > 0)
        {
            foreach(GameObject slot in allSlots)
            {
                Slot temp = slot.GetComponent<Slot>();
                if(temp.IsEmpty)
                {
                    temp.AddItem(item);
                    emptySlots--;
                    return true;
                }
                //Debug.Log("Shouldn't be here.");
            }
        }
        return false;
    }
}
