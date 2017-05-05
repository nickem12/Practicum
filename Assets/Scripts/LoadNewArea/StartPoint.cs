using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour {

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController thePlayer;

    public Vector3 startDirection;

    public string pointName;

    private Camera theCamera;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();



        if (thePlayer.startPoint == pointName)
        {

            thePlayer.transform.position = transform.position;

            thePlayer.transform.rotation = Quaternion.Euler(startDirection);

            theCamera = FindObjectOfType<Camera>();
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
