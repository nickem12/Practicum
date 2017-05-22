using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadNarBehavior : MonoBehaviour {

	public bool DoneReloading = false;

	// Update is called once per frame
	void Update () {
		if(DoneReloading) this.GetComponent<NarrativeTextBehavior>().BeginFade();
	}
}
