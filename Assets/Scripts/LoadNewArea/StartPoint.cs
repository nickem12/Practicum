using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour {

    private GameObject thePlayer;

    public Vector3 startDirection;

    public string pointName;

    private Camera theCamera;

    

    // Use this for initialization
    void Start () {

        thePlayer = GameObject.FindGameObjectWithTag("Player");

        if (thePlayer.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().startPoint == pointName)
        {
            thePlayer.transform.position = new Vector3(0, 0, 0);

            Debug.Log(thePlayer.transform.rotation.eulerAngles.y);

            thePlayer.transform.Rotate(new Vector3(0, -thePlayer.transform.rotation.eulerAngles.y, 0));

            Debug.Log(thePlayer.transform.rotation.eulerAngles.y);

            thePlayer.transform.Rotate(startDirection);

            thePlayer.transform.position = transform.position;

            Debug.Log(thePlayer.transform.rotation.eulerAngles.y);

            theCamera = FindObjectOfType<Camera>();
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
          
        }
    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
