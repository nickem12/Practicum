using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Zombie_AI_IDLE {

	public float CurrentWaitTime = 0.0f;										//The current time of wait
	public float MaxWaitTime = 10.0f;											//Maximum wait time with getter and setter
	public bool CountTime = false;												//Do we count the time?

	public void Update () {
		if(CountTime)
		{
			CurrentWaitTime += Time.deltaTime;									//Update the time
		}
	}

	public bool isWaitDone(){
		
		if(CurrentWaitTime >= MaxWaitTime && CountTime){						//If we have reached the max wait time then reset and return true
			CurrentWaitTime = 0.0f;
			return true;
		}
		return false;
	}
}
