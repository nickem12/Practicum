using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour {

    private GameObject thePlayer;

    public Vector3 startDirection;

    public string pointName;

    private Camera theCamera;

    private Vector3 VectorToRotate;

    // Use this for initialization
    void Start () {


       

        thePlayer = GameObject.FindGameObjectWithTag("Player");

        if (thePlayer.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().startPoint == pointName)
        {

            thePlayer.transform.position = transform.position;
            thePlayer.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);    //Rotate the vector by some angle


            theCamera = FindObjectOfType<Camera>();
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
          
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
