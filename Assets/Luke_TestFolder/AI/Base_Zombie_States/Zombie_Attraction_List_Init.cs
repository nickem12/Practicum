using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zombie_Attraction_List_Init : MonoBehaviour {

	private const string TagID = "Zombie_Attraction";
	private GameObject[] AttractionList;

	private string myScene = "";

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
		myScene = SceneManager.GetActiveScene().name;
		FindAttractions();
	}

	public GameObject[] FindUpdatedAttractions(){

		string curScene = SceneManager.GetActiveScene().name;
		if(curScene != myScene){
			FindAttractions();
			myScene = curScene;
		}
		return AttractionList;
	}

	void OnLevelWasLoaded(int level){
		FindAttractions();
		Debug.Log("I found something");
	}

	void FindAttractions(){
		GameObject[] NonPlayerAttractions = GameObject.FindGameObjectsWithTag(TagID);
		GameObject[] PlayerAttractions = GameObject.FindGameObjectsWithTag("Player");

		AttractionList = new GameObject[NonPlayerAttractions.Length + PlayerAttractions.Length];
		for(int CurNonPlayer = 0; CurNonPlayer < NonPlayerAttractions.Length; CurNonPlayer++){
			AttractionList[CurNonPlayer] = NonPlayerAttractions[CurNonPlayer];
		}

		for(int CurPlayer = 0; CurPlayer < PlayerAttractions.Length; CurPlayer++){
			AttractionList[NonPlayerAttractions.Length + CurPlayer] = PlayerAttractions[CurPlayer];
		}

	}

	public GameObject[] GetList(){
		return AttractionList;
	}
}
