using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartRotation : MonoBehaviour {

    private Camera camera;
    public GameObject computer;
	// Use this for initialization
	void Start () {
        camera = Camera.main;

        camera.transform.LookAt(computer.transform);
	}
	
	// Update is called once per frame
	void Update () {

    }
}
