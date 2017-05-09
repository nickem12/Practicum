using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour {

    private GameObject can;
    private EventSystem eventSystem;
    private GameObject slotPrefab;
    private GameObject inventory;
    private GameObject inventoryCanvas;
    private GameObject dataStorage;

    public void OpenDropDownMenu()
    {
        //can = GameObject.FindGameObjectWithTag("DropDown");                                  //gets reference to the canvas, event system and data storage
        eventSystem = EventSystem.current;
        dataStorage = GameObject.FindGameObjectWithTag("ExtraDataStorage");
        can = dataStorage.GetComponent<DataStorage>().dropDownCanvas;
        //can.gameObject.SetActive(true);                                                                  //turns the canvas on
        can.SetActive(true);
        eventSystem.SetSelectedGameObject(can.transform.GetChild(0).GetChild(0).gameObject);                        //set the seleted item
        dataStorage.GetComponent<DataStorage>().SetSlotIndex(this.gameObject.GetComponent<Slot>().index);           //send the index to the data storage to be used later
    }
    public void CloseDropDownMenu()
    {
        dataStorage = GameObject.FindGameObjectWithTag("ExtraDataStorage");
        can = dataStorage.GetComponent<DataStorage>().dropDownCanvas;
        eventSystem = EventSystem.current;
        inventoryCanvas = GameObject.FindGameObjectWithTag("InventoryCanvas");

        can.gameObject.SetActive(false);
        eventSystem.SetSelectedGameObject(inventoryCanvas.transform.GetChild(1).gameObject);
    }
    public void UseItem()
    {
        
        eventSystem = EventSystem.current;
        dataStorage = GameObject.FindGameObjectWithTag("ExtraDataStorage");
        can = dataStorage.GetComponent<DataStorage>().dropDownCanvas;
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventoryCanvas = GameObject.FindGameObjectWithTag("InventoryCanvas");

        inventory.GetComponent<Inventory>().allSlots[dataStorage.GetComponent<DataStorage>().GetSlotIndex()].GetComponent<Slot>().UseItem();
        can.gameObject.SetActive(false);
        eventSystem.SetSelectedGameObject(inventoryCanvas.transform.GetChild(1).gameObject);
    }
}
