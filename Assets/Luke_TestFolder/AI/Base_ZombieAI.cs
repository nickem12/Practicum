using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Base_ZombieAI : MonoBehaviour {

	Animator anim;																		//The animator attached to the zombie
	NavMeshAgent ZombieAgent;															//The nav mesh agent of the zombie

	private bool isMoving = false;														//The isMoving parameter of the zombie Animator
	private bool isEating = false;														//The isEating parameter of the zombie Animator


	private float VisualRadius = 10.0f;													//The radius of the zombies
	private float AudioRadius = 20.0f;



	enum AIStates {AI_IDLE, AI_SCAN, AI_MOVE, AI_EAT};									//Different types of states available
	int CurrentAIState = (int)AIStates.AI_IDLE;											//Set the initial state

	public delegate void AI_Update();													//This is our delegate object for AI callback functions
	private Dictionary <int, AI_Update> AI_DICT = new Dictionary<int, AI_Update>();		//Dictionary to hold callbacks

	private GameObject EatingObject;													//This is the object that we are eating
	private GameObject CurrentInterest;													//The current interest of the zombie
	private GameObject Interest;														//The current object we are interested in

	Base_Zombie_AI_IDLE IDLE_OBJ = new Base_Zombie_AI_IDLE();							//The idle object
	Base_Zombie_AI_SCAN SCAN_OBJ = new Base_Zombie_AI_SCAN(10.0f, 20.0f, 2.0f);	//The scan object
	Base_Zombie_AI_MOVE MOVE_OBJ = new Base_Zombie_AI_MOVE(10.0f, 20.0f);				//The move object

	void  AI_IDLE_Update(){
		if(!IDLE_OBJ.CountTime) IDLE_OBJ.CountTime = true;								//Start counting again

		if (IDLE_OBJ.isWaitDone()){														//If we are done waiting
			IDLE_OBJ.CountTime = false;													//Set the count time to false
			CurrentAIState = (int)AIStates.AI_SCAN;										//Set state to scanning
		}
		isMoving = false;
		isEating = false;
		Debug.Log("I am in IDLE state");
	}

	void AI_SCAN_Update(){

		Interest = SCAN_OBJ.FindInterest(transform.position);							//Find our interest
		if(Interest == null){
			CurrentAIState = (int)AIStates.AI_IDLE;										//Switch to either idle or move whether or not we have an interest
		}
		else{
			CurrentAIState = (int)AIStates.AI_MOVE;
		}

		isMoving = false;
		isEating = false;
		Debug.Log("I am in SCAN state");
	}

	void AI_MOVE_Update(){

		if(MOVE_OBJ.MoveZombie(ref ZombieAgent, Interest,  transform.position)){

			Debug.Log("I get here");
			if(Interest.GetComponent<Zombie_Attraction>().canEat){
				CurrentAIState = (int)AIStates.AI_EAT;
			}
			else{
				CurrentAIState = (int)AIStates.AI_IDLE;
			}
		}

		isMoving = true;
		isEating = false;

		Debug.Log("I am in MOVE state");
	}

	void AI_EAT_Update(){
		isMoving = false;
		isEating = true;
		Debug.Log("I am in EAT state");
	}

	void Start () {
		anim = GetComponent<Animator>();												//We get the animator component
		ZombieAgent = GetComponent<NavMeshAgent>();										//Get the nav mesh agent

		AI_DICT.Add((int)AIStates.AI_IDLE, new AI_Update(AI_IDLE_Update));				//We add the callbacks to a dictionary
		AI_DICT.Add((int)AIStates.AI_SCAN, new AI_Update(AI_SCAN_Update));
		AI_DICT.Add((int)AIStates.AI_MOVE, new AI_Update(AI_MOVE_Update));
		AI_DICT.Add((int)AIStates.AI_EAT, new AI_Update(AI_EAT_Update));
	}

	void AnimUpdate(){
		

		anim.SetBool("isMoving", isMoving);
		anim.SetBool("isEating", isEating);
	}

	void Update () {
		IDLE_OBJ.Update();

		AI_DICT[CurrentAIState]();														//Call the appropriate callback function
		AnimUpdate();
	}
}
