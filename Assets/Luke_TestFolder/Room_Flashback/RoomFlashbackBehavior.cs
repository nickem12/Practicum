using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFlashbackBehavior : MonoBehaviour {


	public GameObject PostApoRoom;
	public GameObject FlashBackRoom;
	private bool Flashback;

	void Start () {
		PostApoRoom.SetActive(true);
		FlashBackRoom.SetActive(false);
		Flashback = false;
	}


	public void Swap(){
		if(Flashback){
			Flashback = false;
			PostApoRoom.SetActive(true);
			FlashBackRoom.SetActive(false);
		}
		else{
			Flashback = true;
			PostApoRoom.SetActive(false);
			FlashBackRoom.SetActive(true);
		}
	}

	void Update () {
		if(Input.GetButtonDown("Sprint")){
			Swap();
		}
	}
}
