using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Base_Zombie_AI_MOVE : MonoBehaviour {

	float VisualRadius;																				//The radius of the visual for the  zombie
	float AudioRadius;																				//The audio radius for the zombie
	public NavMeshAgent Agent;																		//Navmesh agent 

	private float WaitTime = 0.0f;																	//How much time we've been waiting for
	private const float MaxWaitTime = 1.0f;															//How long we're willing to wait
	private bool WaitTrigger = false;
	private Vector3 LastPosition;
	private Vector3 ThisPosition;

	enum MOVE_Result {MOVE_MOVING, MOVING_FIN, MOVE_STUCK};											//The different returns that we will return back to the base AI	

	public int MoveZombie(ref NavMeshAgent Agent, GameObject attraction, Vector3 pos){
		ThisPosition = pos;

		if(attraction == null) {																	//If the attraction item is null													
			Debug.Log("attraction is null");														//return false
			return (int)MOVE_Result.MOVE_STUCK;
		}
	
		Agent.destination = attraction.transform.position;											//Set the destination

		if(Vector3.Distance(pos, attraction.transform.position) <= Agent.stoppingDistance){			//If we have reached the appropriate distance
		 	return (int)MOVE_Result.MOVING_FIN;															
		} else {
			if (CalculateWaitTime() == (int)MOVE_Result.MOVE_STUCK) return (int)MOVE_Result.MOVE_STUCK;
		}
		LastPosition = pos;
		return (int)MOVE_Result.MOVE_MOVING;
	}

	void Start(){
		LastPosition = Agent.transform.position;
		ThisPosition = Agent.transform.position;
	}

	public int CalculateWaitTime(){
		if(!WaitTrigger){																			//If we are not waiting
			if(LastPosition == Agent.transform.position){											//If the last position is the current position
				 WaitTrigger = true;
				 WaitTime = 0.0f;
			}
		}

		if(WaitTrigger){																			//If we are currently waiting
			WaitTime += Time.deltaTime;																//Add the time

			if(WaitTime >= MaxWaitTime){															//If we have reached the max wait time
				ResetWait();																		//Reset the wait
				return (int)MOVE_Result.MOVE_STUCK;
			}

			if(LastPosition != Agent.transform.position){											//If the positions have changed 
				ResetWait();
			}
		}

		return (int)MOVE_Result.MOVE_MOVING;
	}

	private void ResetWait(){
		WaitTime = 0.0f;
		WaitTrigger = false;
	}

	public Base_Zombie_AI_MOVE(float Visual, float Audio){
		VisualRadius = Visual;																		//Set the radius's
		AudioRadius = Audio;	
	}

}
