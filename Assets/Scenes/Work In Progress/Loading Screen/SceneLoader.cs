using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool loadScene = false;

    [SerializeField]
    private int scene;
    [SerializeField]
    private Text loadingText;

    public string exitPoint;

    private GameObject thePlayer;

    private GameObject LoadingCanvas;

    public float rotateBy;

    private void Start()
    {

        thePlayer = GameObject.FindGameObjectWithTag("Player");
        LoadingCanvas = GameObject.FindGameObjectWithTag("LoadingCanvas");
        loadingText = LoadingCanvas.GetComponentInChildren<Text>();
       // LoadingCanvas.SetActive(false);

    }

    // Updates once per frame
    void Update()
    {

        

        // If the new scene has started loading...
        if (loadScene == true)
        {

            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));

        }

    }


    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene()
    {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
       // yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        
        // If the player has pressed the space bar and a new scene is not loading yet...
        if (Input.GetButtonDown("Submit") && loadScene == false)
        {
            LoadingCanvas.GetComponent<Canvas>().enabled = true;
            // ...set the loadScene boolean to true to prevent loading a new scene more than once...
            loadScene = true;

            // ...change the instruction text to read "Loading..."
            loadingText.text = "Loading...";

            thePlayer.GetComponent<FirstPersonControllerBehavior>().canMove = false;
            thePlayer.GetComponent<FirstPersonControllerBehavior>().startPoint = exitPoint;
            thePlayer.GetComponent<FirstPersonControllerBehavior>().SetStartRotation(rotateBy);
            // ...and start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene());

            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        LoadingCanvas.GetComponent<Canvas>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(!loadScene)
        {
            LoadingCanvas.GetComponent<Canvas>().enabled = false;
        }
        
    }

}