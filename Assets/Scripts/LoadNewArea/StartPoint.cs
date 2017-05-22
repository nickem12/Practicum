using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPoint : MonoBehaviour {

    private GameObject thePlayer;

    public Vector3 startDirection;

    public string pointName;

    private Camera theCamera;

    private GameObject loadingCanvas;

    // Use this for initialization
    void Start () {

        thePlayer = GameObject.FindGameObjectWithTag("Player");

        thePlayer.GetComponent<FirstPersonControllerBehavior>().canMove = true;

        loadingCanvas = GameObject.FindGameObjectWithTag("LoadingCanvas");

        if (thePlayer.GetComponent<FirstPersonControllerBehavior>().startPoint == pointName)
        {
            thePlayer.transform.position = new Vector3(0, 0, 0);

            Debug.Log(thePlayer.transform.rotation.eulerAngles.y);

            thePlayer.transform.position = transform.position;

            Debug.Log(thePlayer.transform.rotation.eulerAngles.y);

            loadingCanvas.GetComponent<Canvas>().enabled = false;

            theCamera = FindObjectOfType<Camera>();

            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            loadingCanvas.GetComponentInChildren<Text>().color = Color.white;
            loadingCanvas.GetComponentInChildren<Text>().text = "Press Enter to Continue";
        }
    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
