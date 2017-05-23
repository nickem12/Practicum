using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {

    private bool loadScene = false;

    public string levelToLoad;

    public string exitPoint;

    private GameObject thePlayer;

    private GameObject LoadingCanvas;

    private Text loadingText;

    // Use this for initialization
    void Start () {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        LoadingCanvas = GameObject.FindGameObjectWithTag("LoadingCanvas");
        loadingText = LoadingCanvas.GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        // If the new scene has started loading...
        if (loadScene == true)
        {

            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Input.GetButtonDown("Submit") && loadScene == false)
            {

                LoadingCanvas.GetComponent<Canvas>().enabled = true;
                // ...set the loadScene boolean to true to prevent loading a new scene more than once...
                loadScene = true;
                // ...change the instruction text to read "Loading..."
                loadingText.text = "Loading...";

                thePlayer.GetComponent<FirstPersonControllerBehavior>().canMove = false;
                thePlayer.GetComponent<FirstPersonControllerBehavior>().startPoint = exitPoint;

                DontDestroyOnLoad(thePlayer);

                SceneManager.LoadScene(levelToLoad);

                thePlayer.GetComponent<FirstPersonControllerBehavior>().startPoint = exitPoint;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        LoadingCanvas.GetComponent<Canvas>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!loadScene)
        {
            LoadingCanvas.GetComponent<Canvas>().enabled = false;
        }

    }

}
