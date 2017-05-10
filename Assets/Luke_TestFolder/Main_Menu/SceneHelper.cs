using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour {

	public void ChangeScene(string Name){
		SceneManager.LoadScene(Name);
	}
}
