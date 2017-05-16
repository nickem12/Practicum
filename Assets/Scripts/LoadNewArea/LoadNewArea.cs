using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {

    public string levelToLoad;

    public string exitPoint;

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController thePlayer; 

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Input.GetButtonDown("Submit"))
            {
                DontDestroyOnLoad(thePlayer);

                SceneManager.LoadScene(levelToLoad);

                thePlayer.startPoint = exitPoint;
            }
        }
    }
}
