using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour {

    public GameObject canvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            canvas.GetComponent<Canvas>().enabled = true;
            if (Input.GetButtonDown("Cancel"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
