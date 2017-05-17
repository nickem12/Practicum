using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFlashbackBehavior : MonoBehaviour {


	public GameObject PostApoRoom;
	public GameObject FlashBackRoom;
	private bool Flashback;
	private GameObject myPlayer;

	private bool FirstTimeFlag = false;


	void Start () {
		PostApoRoom.SetActive(true);
		FlashBackRoom.SetActive(false);
		Flashback = false;
		myPlayer = GameObject.FindGameObjectWithTag("Player");
	}


	public void Swap(){
		if(!FirstTimeFlag && Flashback || !Flashback){

			if(Flashback){
				PostApoRoom.SetActive(false);
				FlashBackRoom.SetActive(true);
			}
			else{
				PostApoRoom.SetActive(true);
				FlashBackRoom.SetActive(false);
			}

			FirstTimeFlag = true;
		}
	}

	void Update () {
		Debug.Log(Vector3.Distance(this.transform.position, myPlayer.transform.position));
	}

	void OnTriggerEnter(Collider Other){
		if(Vector3.Distance(this.transform.position, Other.transform.position) <= 5.5f){
			if (Other.gameObject.tag == "Player"){
				Flashback = true;
				Swap();
			}
		}
		else{
			if(Other.gameObject.tag == "Player"){
				Flashback = true;
				Swap();
			}
		}

	}

	void OnTriggerExit(Collider Other){
		if(Other.gameObject.tag == "Player"){
			if(Vector3.Distance(this.transform.position, Other.gameObject.transform.position) >= 5.5f){
				Flashback = false;

				Swap();
			}
			else{
				Flashback = true;
				Swap();
			}
		}

	}


}
