using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Base_Zombie_AI_MOVE : MonoBehaviour {

	float VisualRadius;
	float AudioRadius;
	public NavMeshAgent Agent;


	public bool MoveZombie(){
		return false;
	}

	void Start () {
		
	}

	public Base_Zombie_AI_MOVE(float Visual, float Audio)
	{
		VisualRadius = Visual;
		AudioRadius = Audio;
	}

	void Update (ref NavMeshAgent Agent, ref GameObject attraction) {
		


	}
}
