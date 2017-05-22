using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour {

    private GameObject thePlayer;
    public float RotateBy;
    // Use this for initialization
    void Start() {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        thePlayer.GetComponent<FirstPersonControllerBehavior>().SetStartRotation(RotateBy);
    }

	// Update is called once per frame
	void Update () {

    }
}
