using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrongSideBehavior : MonoBehaviour {

	GameObject Stapler;
	private bool AllOutStaples = false;
	private bool FirstStaple = false;

	void Start(){
		Stapler = GameObject.FindGameObjectWithTag("Stapler");
		this.GetComponent<NarrativeTextBehavior>().DontDestroyOnFade = true;
		this.GetComponent<NarrativeTextBehavior>().Sleep = true;
		this.GetComponentInChildren<Text>().enabled = false;
	}

	void Update(){

		if(Stapler.GetComponent<Stapler>().stapleCount >= 3 && !FirstStaple){
			FirstStaple = true;

			this.GetComponentInChildren<Text>().enabled = true;
			this.GetComponent<NarrativeTextBehavior>().enabled = true;
			this.GetComponent<NarrativeTextBehavior>().Sleep = false;

			this.GetComponent<NarrativeTextBehavior>().BeginType();
		}
		else if (Stapler.GetComponent<Stapler>().stapleCount == 4 && !AllOutStaples && FirstStaple){
			AllOutStaples = true;
			GameObject.FindGameObjectWithTag("QuestManager").GetComponent<OpeningQuestData>().NextState();
		}	
	}

}
