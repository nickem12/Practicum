using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour {

    public GameObject panel;
    public EventSystem eventSystem;

    public void OpenDropDownMenu()
    {
        panel.gameObject.SetActive(true);        
    }
    public void CloseDropDownMenu()
    {
        panel.gameObject.SetActive(false);
        eventSystem.SetSelectedGameObject(this.gameObject);
    }
}
