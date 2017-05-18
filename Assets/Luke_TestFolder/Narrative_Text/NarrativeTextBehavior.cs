using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeTextBehavior : MonoBehaviour {

	public float Type_Delay;
	public float Fade_Time;
	public float StayTime;
	public bool ShowOnlyOnce;

	private string Text;
	private string currentText = "";

	private Color Textcolor;
	private Text TextObject;
	enum NarrativeState {NAR_SLEEP, NAR_TYPING, NAR_STAY, NAR_FADING};
	private int myState = (int)NarrativeState.NAR_SLEEP;

	private bool PlayerInside = false;

	private float TimeOn = 0.0f;

	void Start () {
		TextObject = GetComponentInChildren<Text>();								//Get the text object
		Text = TextObject.text;														//Get the value of the text and store it
		TextObject.text = "";														//Set the text value in the text object to empty
		Textcolor = TextObject.color;												//Get the color
	}

	public int GetState(){
		return myState;
	}

	public void SetState(int NewState){
		myState = NewState;
	}

	IEnumerator ShowText(){
		myState = (int)NarrativeState.NAR_TYPING;									//We are typing
		Textcolor.a = 1.0f;															//Set the alpha of the color to full
		TextObject.color = Textcolor;												//Give the color to the text object

		for(int i = 0; i < Text.Length; i++){										//Loop through the text's length
			currentText = Text.Substring(0, i);										//Set the current text
			TextObject.text = currentText;											//Set the text object's text to the current text
			yield return new WaitForSeconds(Type_Delay);							//Wait for a certain amount of time
		}

		myState = (int)NarrativeState.NAR_STAY;									//We are typing
	}


	IEnumerator FadeText(){
		myState = (int)NarrativeState.NAR_FADING;

		do{

			Textcolor.a -= 0.01f;													//Subtract 1% alpha														
			TextObject.color = Textcolor;											//Set the color to the text color
			yield return new WaitForSeconds(Fade_Time / 100.0f);					//Wait for the specified amount of time

		}while(Textcolor.a > 0.00f);												//Continue to loop while the alpha is greater than 0.0f

		if(Textcolor.a < 0.0f) Textcolor.a = 0.0f;									//Safety, make sure its not less than 0
		myState = (int)NarrativeState.NAR_SLEEP;									//We are typing

		if(ShowOnlyOnce) Destroy(this.gameObject);
	}



	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){										//if the player collides with the trigger box

			PlayerInside = true;

			if(myState == (int)NarrativeState.NAR_SLEEP){														//If the text is not yet triggered trigger it
				StartCoroutine(ShowText());	
			}

		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "Player"){
			PlayerInside = false;

			if(myState == (int)NarrativeState.NAR_STAY){
				StartCoroutine(FadeText());
			}
		}
	}

	void Update(){
		if(myState == (int)NarrativeState.NAR_STAY){
			if(StayTime > 0.0f){
				if(PlayerInside){

					TimeOn += Time.deltaTime;
					if(TimeOn >= StayTime){

						TimeOn = 0.0f;
						StartCoroutine(FadeText());

					}
				}
				else{

					TimeOn = 0.0f;
					StartCoroutine(FadeText());

				}
			}

		}
	}

}
