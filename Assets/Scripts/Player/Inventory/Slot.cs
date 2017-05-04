using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler {

    private Stack<Item> items;

    public Text stackText;

    public Sprite slotEmpty;
    public Sprite slotHighlighted;
    public bool IsAvailable
    {
        get { return CurrentItem.maxSize > Items.Count; }
    }
    public Item CurrentItem
    {
        get { return Items.Peek(); }
    }
    public bool IsEmpty
    {
        get { return Items.Count == 0; }
    }

    public Stack<Item> Items
    {
        get
        {
            return items;
        }

        set
        {
            items = value;
        }
    }

    // Use this for initialization
    void Start () {

        Items = new Stack<Item>();

        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform txtRect = stackText.GetComponent<RectTransform>();

        int textScaleFactor = (int)(slotRect.sizeDelta.x * 0.60);

        stackText.resizeTextMaxSize = textScaleFactor;
        stackText.resizeTextMinSize = textScaleFactor;

        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
    }
	
    private void ChangeSprite(Sprite neutral, Sprite highlighted)
    {
        GetComponent<Image>().sprite = neutral;
        SpriteState st = new SpriteState();
        st.highlightedSprite = highlighted;
        st.pressedSprite = neutral;

        GetComponent<Button>().spriteState = st;
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void AddItem(Item item)
    {
        Items.Push(item);

        if(Items.Count>1)
        {
            stackText.text = Items.Count.ToString(); 
        }
        ChangeSprite(item.spriteNeutral, item.spriteHighlighted);
    }
    public void AddItems(Stack<Item> items)
    {
        this.Items = new Stack<Item>(items);
        stackText.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
        ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);
    }
    private void UseItem()
    {
        if(!IsEmpty)
        {
            Items.Pop().Use();

            stackText.text = Items.Count > 1 ? Items.Count.ToString() : string.Empty;
            if(IsEmpty)
            {
                ChangeSprite(slotEmpty, slotHighlighted);

                Inventory.EmptySlots++;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }
    public void ClearSlot()
    {
        items.Clear();
        ChangeSprite(slotEmpty, slotHighlighted);
        stackText.text = string.Empty;
    }
}
