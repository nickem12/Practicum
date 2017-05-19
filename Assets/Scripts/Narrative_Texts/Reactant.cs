using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactant : MonoBehaviour {

	public virtual void EndReact(string ID){
		Debug.Log(ID + "BASE");
	}

	public virtual void EndReact(){

	}
}
