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
		AttractionList = GameObject.FindGameObjectsWithTag(TagID);
		Debug.Log(AttractionList.Length);
	}
}
