using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Base_Zombie_AI_MOVE : MonoBehaviour {

	float VisualRadius;
	float AudioRadius;
	public NavMeshAgent Agent;


	public bool MoveZombie(ref NavMeshAgent Agent, GameObject attraction, Vector3 pos){
		if(attraction == null) {
			Debug.Log("attraction is null");
			return false;
		}
	
		Agent.destination = attraction.transform.position;
		Debug.Log("Distanec : " + Vector3.Distance(pos, attraction.transform.position));
		Debug.Log("Agent Remaining : " + Agent.remainingDistance);

		if(Vector3.Distance(pos, attraction.transform.position) <= Agent.stoppingDistance){
		 	return true;															
		}

		//if(Agent.remainingDistance <= Agent.stoppingDistance) return true;

		return false;
	}

	void Start () {
		
	}

	public Base_Zombie_AI_MOVE(float Visual, float Audio)
	{
		VisualRadius = Visual;
		AudioRadius = Audio;
	}

}
