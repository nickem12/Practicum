using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour {

    public ItemData data;

	// Use this for initialization
	void Start () {
        data = GameObject.FindGameObjectWithTag("ExtraDataStorage").GetComponent<DataStorage>().data;
        Debug.Log("meme");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
