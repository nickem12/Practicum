using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStartRotation : MonoBehaviour {

    private GameObject player;
    private BoxCollider[] colliders;
    public bool sitting;
    public bool ableToSit;

    public Canvas displayCanvas;
    private Image button;
    private Text displayText;

    public Sprite xButton;
    public Sprite bButton;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<FirstPersonControllerBehavior>().SetStartRotation(120f);

        colliders = this.GetComponents<BoxCollider>();

        button = displayCanvas.transform.GetChild(0).GetComponent<Image>();
        displayText = displayCanvas.transform.GetChild(1).GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {

        if (sitting)
        {
            displayText.text = "Exit chair";
            button.sprite = bButton;
        }
        else
        {
            displayText.text = "Enter chair";
            button.sprite = xButton;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(ableToSit)
            {
                displayCanvas.enabled = true;
                if (sitting)
                {
                    if (Input.GetButtonDown("Cancel"))
                    {
                        sitting = false;
                        player.GetComponent<FirstPersonControllerBehavior>().canMove = true;
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Submit"))
                    {
                        sitting = true;
                        player.GetComponent<FirstPersonControllerBehavior>().canMove = false;
                        player.transform.position = new Vector3(5.41f, 1.73f, -3.24f);
                    }
                }
            }
            else
            {
                displayCanvas.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(ableToSit)
            {
                displayCanvas.enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            displayCanvas.enabled = false;
        }
    }
}
