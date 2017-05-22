using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStartRotation : Reactant {

    private GameObject player;
    private BoxCollider[] colliders;
    public bool sitting;
    public bool ableToSit;

    public Canvas displayCanvas;
    private Image button;
    private Text displayText;

    public Sprite xButton;
    public Sprite bButton;

    private OpeningQuestData Quest;

	public override void EndReact(string ID){
		if(ID == "1_8_Its in the back"){
			ableToSit = true;
			Quest.NextState();
		}
	}

    // Use this for initialization
    void Start () {
    	Quest = GetComponent<OpeningQuestData>();

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
                        player.transform.position = this.transform.position;
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
