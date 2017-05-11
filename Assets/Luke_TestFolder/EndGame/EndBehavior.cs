using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBehavior : MonoBehaviour {

	public bool isEnd;
	public GameObject[] EndObjects;
	public GameObject PlayerObject;

	private Color colA;
	private Color colB;
	private float StartAlpha;
	private float TotalDistance;
	private float PlayerDistance;


	public AudioSource CryingSource;
	private float CryingVolume;

	void Start () {

		colA = EndObjects[2].GetComponent<SkinnedMeshRenderer>().material.color;
		colB = EndObjects[3].GetComponent<SkinnedMeshRenderer>().material.color;

		StartAlpha = colA.a * 255;

		TotalDistance = Vector3.Distance(this.transform.position, EndObjects[3].transform.position);

		CryingVolume = CryingSource.volume;
	}
	

	void Update () {

		if(isEnd){

			PlayerDistance = Vector3.Distance(PlayerObject.transform.position, EndObjects[3].transform.position) - 5.0f;


			if(PlayerDistance < TotalDistance){

				float Tran = (PlayerDistance / TotalDistance) * StartAlpha / 255;
				CryingSource.volume = CryingVolume * (PlayerDistance / TotalDistance);

				colA.a = Tran;
				colB.a = Tran;
				EndObjects[2].GetComponent<SkinnedMeshRenderer>().material.color = colA;
				EndObjects[3].GetComponent<SkinnedMeshRenderer>().material.color = colB;

			}
			else{
				colA.a = StartAlpha / 255;
				colB.a = StartAlpha / 255;

				EndObjects[2].GetComponent<SkinnedMeshRenderer>().material.color = colA;
				EndObjects[3].GetComponent<SkinnedMeshRenderer>().material.color = colB;

				CryingSource.volume = CryingVolume;
			}
		}

	}
}
