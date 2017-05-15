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

    private float growlTimer = 5.0f;

    public GameObject zombieSoundManager;

	enum AIStates {AI_IDLE, AI_SCAN, AI_MOVE, AI_EAT};									//Different types of states available
	int CurrentAIState = (int)AIStates.AI_IDLE;											//Set the initial state

	enum MOVE_Result {MOVE_MOVING, MOVING_FIN, MOVE_STUCK};								//The different returns that we will return back to the base AI	

	public delegate void AI_Update();													//This is our delegate object for AI callback functions
	private Dictionary <int, AI_Update> AI_DICT = new Dictionary<int, AI_Update>();		//Dictionary to hold callbacks

	private GameObject EatingObject;													//This is the object that we are eating
	private GameObject CurrentInterest;													//The current interest of the zombie
	private GameObject Interest;														//The current object we are interested in
	private Vector3 InterestLastPosition;												//The position of the interest object last frame

	public bool DEBUG;																	//Are we currently in debug mode?
	public bool HIGH_DEBUG;																//Would we like even more information?
	private GameObject Finder;															//The finder for the zombie AI
	private bool FoundFinder = false;

	Base_Zombie_AI_IDLE IDLE_OBJ = new Base_Zombie_AI_IDLE();							//The idle object
	Base_Zombie_AI_SCAN SCAN_OBJ = new Base_Zombie_AI_SCAN(10.0f, 20.0f, 2.0f);			//The scan object
	Base_Zombie_AI_MOVE MOVE_OBJ = new Base_Zombie_AI_MOVE(10.0f, 20.0f);				//The move object

	void  AI_IDLE_Update(){
		if(!IDLE_OBJ.CountTime) IDLE_OBJ.CountTime = true;								//Start counting again

		if (IDLE_OBJ.isWaitDone()){														//If we are done waiting
			IDLE_OBJ.CountTime = false;													//Set the count time to false
			CurrentAIState = (int)AIStates.AI_SCAN;										//Set state to scanning
		}
		isMoving = false;
		isEating = false;
		if(DEBUG) Debug.Log("I am in IDLE state");
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
		if(DEBUG) Debug.Log("I am in SCAN state");
	}


	void AI_MOVE_Update(){

		int Result = (int)MOVE_OBJ.MoveZombie(ref ZombieAgent, Interest,  transform.position);	//Get the result of the move
		switch(Result){																			//Switch on the result
			case (int)MOVE_Result.MOVING_FIN:													//If we have finished then check if we can eat it or not

				if(Interest.GetComponent<Zombie_Attraction>().canEat){
					CurrentAIState = (int)AIStates.AI_EAT;
				}
				else{
					CurrentAIState = (int)AIStates.AI_IDLE;
				}
				break;

			case (int)MOVE_Result.MOVE_MOVING:
				InterestLastPosition = Interest.transform.position;							//Get the position of the interest object
				isMoving = true;															//Set variables for animation purposes
				isEating = false;
				break;

			case (int)MOVE_Result.MOVE_STUCK:													//If we're stuck then set back to idle. 
				CurrentAIState = (int)AIStates.AI_IDLE;
				break;
		}

		if(DEBUG) Debug.Log("I am in MOVE state");
	}


	void AI_EAT_Update(){
		isMoving = false;																//Set the anim variables
		isEating = true;

		if(Interest.transform.position != InterestLastPosition){
			if(Vector3.Distance(this.transform.position, Interest.transform.position) < VisualRadius){
				CurrentAIState = (int)AIStates.AI_MOVE;
			}
			else{
				CurrentAIState = (int)AIStates.AI_IDLE;
			}
		}

		if(DEBUG) Debug.Log("I am in EAT state");
	}


	void Start () {
		anim = GetComponent<Animator>();												//We get the animator component
		ZombieAgent = GetComponent<NavMeshAgent>();										//Get the nav mesh agent

		AI_DICT.Add((int)AIStates.AI_IDLE, new AI_Update(AI_IDLE_Update));				//We add the callbacks to a dictionary
		AI_DICT.Add((int)AIStates.AI_SCAN, new AI_Update(AI_SCAN_Update));
		AI_DICT.Add((int)AIStates.AI_MOVE, new AI_Update(AI_MOVE_Update));
		AI_DICT.Add((int)AIStates.AI_EAT, new AI_Update(AI_EAT_Update));			

		FindFinder();		
	}


	void AnimUpdate(){
		anim.SetBool("isMoving", isMoving);												//Set the isMoving variable
		anim.SetBool("isEating", isEating);												//Set the isEating variable
	}

	void ZombieAudio(){
		growlTimer -= Time.deltaTime;													//The timer for growling
        if (growlTimer <= 0){															//If growl has ran out
            zombieSoundManager.GetComponent<ZombieSoundManager>().PlayGrowl();			//Play the growl
            growlTimer = Random.Range(2, 10);											//Set timer
        }
	}

	void DebugRadius(){
		if(DEBUG){
			//Audio
			Debug.DrawLine(this.transform.position, (this.transform.position + new Vector3(0,0,AudioRadius)), Color.red);
			Debug.DrawLine(this.transform.position, (this.transform.position + new Vector3(0,0,-AudioRadius)), Color.red);
			Debug.DrawLine(this.transform.position, (this.transform.position + new Vector3(AudioRadius,0,0)), Color.red);
			Debug.DrawLine(this.transform.position, (this.transform.position + new Vector3(-AudioRadius,0,0)), Color.red);

			//Visual
			Debug.DrawLine(this.transform.position, (this.transform.position + new Vector3(0,0,VisualRadius)), Color.blue);
			Debug.DrawLine(this.transform.position, (this.transform.position + new Vector3(0,0,-VisualRadius)), Color.blue);
			Debug.DrawLine(this.transform.position, (this.transform.position + new Vector3(VisualRadius,0,0)), Color.blue);
			Debug.DrawLine(this.transform.position, (this.transform.position + new Vector3(-VisualRadius,0,0)), Color.blue);

			if(HIGH_DEBUG && FoundFinder){

				//Interest lines
				GameObject[] InterestList = Finder.GetComponent<Zombie_Attraction_List_Init>().GetList();
				for(int curInterest = 0; curInterest < InterestList.Length; curInterest++){
					if(InterestList[curInterest] != null)
						Debug.DrawLine(this.transform.position, InterestList[curInterest].transform.position, Color.cyan);
				}

				if(CurrentAIState == (int)AIStates.AI_MOVE){
					Debug.DrawLine(this.transform.position, Interest.transform.position, Color.green);
				}

			}
		}
	}

	void FindFinder(){
		Finder = GameObject.FindGameObjectWithTag("AI_Finder");
		if(Finder != null) FoundFinder = true;		
	}

	void Update () {
		if(!FoundFinder)FindFinder();													//If we have not found the finder then try again

		IDLE_OBJ.Update();																//Update the Idle object

		AI_DICT[CurrentAIState]();														//Call the appropriate callback function
		AnimUpdate();																	//Update the anim component
		ZombieAudio();																	//Update the zombie audio

		DebugRadius();
	}
}
