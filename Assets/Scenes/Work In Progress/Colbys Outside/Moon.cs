using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour {

    public float smooth;
    public float speedOfMoon = 5f;
    public Light[] spotlight;
    private int counter = 0;
  
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.zero, Vector3.right, speedOfMoon * Time.deltaTime);
        transform.LookAt(Vector3.zero);

        for(counter = 0; counter < spotlight.Length; counter++)
        {
            spotlight[counter].transform.LookAt(transform.position);
        }


        GetComponent<Light>().intensity = transform.position.y / 250;
    }
}
