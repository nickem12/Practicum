using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Zombie_AI_SCAN : MonoBehaviour {


	float VisualRadius;																					//The visual radius for the scanning of the zombie
	float AudioRadius;																					//The audio radius for the scanning of the zombie

	float MinDistance;

	GameObject myFinder;																				//The finder object that will find our interests

	public Base_Zombie_AI_SCAN(float Visual, float Audio, float Min){
		VisualRadius = Visual;
		AudioRadius = Audio;
		MinDistance = Min;
	}

	void Start(){
		GetFinder();																					//Get the finder object
	}

	GameObject[] GetInterestList(){
		if(myFinder == null)
			GetFinder();																				//Safety check

		return myFinder.GetComponent<Zombie_Attraction_List_Init>().FindUpdatedAttractions();			//Return the list of attractions
	}

	void GetFinder(){
		myFinder = GameObject.FindGameObjectWithTag("AI_Finder");										//Get the finder in the scene
	}

	public GameObject FindInterest(Vector3 pos){
		GameObject[] Interests = GetInterestList();														//We get the interests list

		if(Interests == null) return null;																//If the list is null then return null

		int ClosestIndex = -1;																			//Tracking variables
		float CurrentDistance = AudioRadius + 1.0f;
		float ClosestDistance = AudioRadius + 1.0f;


		for(int CurrentInterest = 0; CurrentInterest < Interests.Length; CurrentInterest++){			//Find the closest attraction within the radius
			CurrentDistance = Vector3.Distance(pos, Interests[CurrentInterest].transform.position);
			if(CurrentDistance <= VisualRadius && CurrentDistance < ClosestDistance && CurrentDistance > MinDistance){
				Debug.Log(Interests[CurrentInterest].transform.position.x);
				ClosestIndex = CurrentInterest;
				ClosestDistance = CurrentDistance;
			}
		}

		if(ClosestIndex == -1) return null;																//Return the result
		return Interests[ClosestIndex];
	}

}
