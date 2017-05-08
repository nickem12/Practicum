using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour {

    private Canvas can;
    private EventSystem eventSystem;
    private GameObject slotPrefab;
    private GameObject inventory;
    private GameObject dataStorage;

    public void OpenDropDownMenu(GameObject slot)
    {
        can = GameObject.FindGameObjectWithTag("DropDown").GetComponent<Canvas>();
        eventSystem = EventSystem.current;
        dataStorage = GameObject.FindGameObjectWithTag("ExtraDataStorage");
        dataStorage.GetComponent<DataStorage>().SetSlotIndex(slot.GetComponent<Slot>().index);
        can.GetComponent<Canvas>().enabled = true;
        eventSystem.SetSelectedGameObject(can.transform.GetChild(0).GetChild(0).gameObject);
    }
    public void CloseDropDownMenu()
    {
        can = GameObject.FindGameObjectWithTag("DropDown").GetComponent<Canvas>();
        eventSystem = EventSystem.current;
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        can.GetComponent<Canvas>().enabled = false;
        eventSystem.SetSelectedGameObject(inventory.transform.GetChild(1).gameObject);
    }
}
