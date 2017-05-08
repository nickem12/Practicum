using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_ZombieAI : MonoBehaviour {

	Animator anim;																		//The animator attached to the zombie

	private bool isMoving = false;														//The isMoving parameter of the zombie Animator
	private bool isEating = false;														//The isEating parameter of the zombie Animator


	enum AIStates {AI_IDLE, AI_SCAN, AI_MOVE, AI_EAT};									//Different types of states available
	int CurrentAIState = (int)AIStates.AI_IDLE;											//Set the initial state

	public delegate void AI_Update();													//This is our delegate object for AI callback functions
	private Dictionary <int, AI_Update> AI_DICT = new Dictionary<int, AI_Update>();		//Dictionary to hold callbacks


	void  AI_IDLE_Update(){

	}

	void AI_SCAN_Update(){

	}

	void AI_MOVE_Update(){

	}

	void AI_EAT_Update(){

	}

	void Start () {
		anim = GetComponent<Animator>();												//We get the animator component
		AI_DICT.Add((int)AIStates.AI_IDLE, new AI_Update(AI_IDLE_Update));				//We add the callbacks to a dictionary
		AI_DICT.Add((int)AIStates.AI_SCAN, new AI_Update(AI_SCAN_Update));
		AI_DICT.Add((int)AIStates.AI_MOVE, new AI_Update(AI_MOVE_Update));
		AI_DICT.Add((int)AIStates.AI_EAT, new AI_Update(AI_EAT_Update));
	}

	void Update () {
		AI_DICT[CurrentAIState]();														//Call the appropriate callback function
	}
}
