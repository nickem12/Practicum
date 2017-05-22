using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishedBehvior : MonoBehaviour {

	GameObject Stapler;
	private bool DonePapers = false;

	// Use this for initialization
	void Start () {
		Stapler = GameObject.FindGameObjectWithTag("Stapler");
		this.GetComponent<NarrativeTextBehavior>().DontDestroyOnFade = true;
		this.GetComponent<NarrativeTextBehavior>().Sleep = true;
		this.GetComponentInChildren<Text>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(Stapler.GetComponent<Stapler>().stapleCount == 6 && !DonePapers){
			DonePapers = true;
			this.GetComponentInChildren<Text>().enabled = true;
			this.GetComponent<NarrativeTextBehavior>().enabled = true;
			this.GetComponent<NarrativeTextBehavior>().Sleep = false;

			this.GetComponent<NarrativeTextBehavior>().BeginType();
		}
	}
}
