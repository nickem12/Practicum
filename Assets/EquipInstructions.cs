using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInstructions : MonoBehaviour {

	GameObject thePlayer;
	public bool staplerEquiped = false;

	void Start () {
		thePlayer = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () {
		
		if(staplerEquiped){
			this.GetComponent<NarrativeTextBehavior>().BeginFade();
		}

	}
}
