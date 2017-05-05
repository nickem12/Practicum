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

    private void Start()
    {
        //can = GameObject.FindGameObjectWithTag("DropDown").GetComponent<Canvas>();
        eventSystem = EventSystem.current;
    }
    public void OpenDropDownMenu()
    {
        can = GameObject.FindGameObjectWithTag("DropDown").GetComponent<Canvas>();
        eventSystem = EventSystem.current;
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
