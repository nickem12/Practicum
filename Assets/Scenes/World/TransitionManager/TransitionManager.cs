using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour {

    private static bool TransitionManagerExists;
    public GameObject[] LoadAreas;
    private GameObject[] LoadAreasCopies;
    public string key;
    private int counter = 0;

	// Use this for initialization
	void Start () {
        if (!TransitionManagerExists)
        {
            TransitionManagerExists = true;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadAreasCopies = LoadAreas;

    }
	
    public void ChangeRotation()
    {
        for(counter = 0; counter < LoadAreas.Length; counter++)
        {
            Debug.Log("counter = " + counter);
            if (LoadAreasCopies[counter].GetComponentInChildren<StartPoint>().pointName == key)
            {
                Debug.Log("inside counter = " + counter);
                Debug.Log("LoadAreas[counter].GetComponentInChildren<StartPoint>().pointName = " + LoadAreasCopies[counter].GetComponentInChildren<StartPoint>().pointName);
                Debug.Log("key = " + key);
                LoadAreasCopies[counter].GetComponentInChildren<RotatePlayer>().SetRotation();
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
