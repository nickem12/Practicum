using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonControllerBehavior : MonoBehaviour {

	
	private Vector3 OldMouse;							//The mouse position last frame
	private Vector3 CurMouse;							//The mouse position this frame
	private Vector3 DiffMouse;							//The difference in movement for the mouse

	private Vector3 ForwardVector;						//The direction we would like to look in


	void Start () {

		OldMouse = Input.mousePosition;					//Use initial mouse position
		ForwardVector = new Vector3(0,0,1);				//We start by looking forward in the z axis

	}



	void Update () {

		DiffMouse = GetMouseInformation();				//Get the mouse information
		Debug.Log("Actual: " + DiffMouse);
		//Debug.Log(DiffMouse);
		//RotateForwardVec();								//Rotate the forward vector
		Debug.DrawLine(transform.position, transform.position + (ForwardVector * 5), Color.black);


	}





	private Vector3 GetMouseInformation(){

		CurMouse = Input.mousePosition;					//Get the current mouse information

		if(CurMouse.x < 0.0f) CurMouse.x = 0.0f;		//Clamp the cur mouse x and y to be greater than 0.
		if(CurMouse.y < 0.0f) CurMouse.y = 0.0f;

		Vector3 ResultVec = CurMouse - OldMouse;		//Get the result

		//Debug.Log(ResultVec.x + " " + ResultVec.y);

		//Debug.Log(ResultVec.x / (float)Screen.width);
		//Debug.Log(ResultVec.y / (float)Screen.height);

		ResultVec.x = ResultVec.x / (float)Screen.width;				//Divide by the width and height - will put values for x and y between 0 and 1
		ResultVec.y = ResultVec.y / (float)Screen.height;					

		//Debug.Log("Result : " + ResultVec.x + " " + ResultVec.y);
		Debug.Log("Calculated : " + ResultVec.x);

		return ResultVec;								//Return the difference, if any
	}


	private void RotateForwardVec(){

		if(DiffMouse.x != 0 || DiffMouse.y != 0 || DiffMouse.z != 0){

			ForwardVector = Quaternion.AngleAxis(DiffMouse.y, new Vector3(1.0f,0,0)) * ForwardVector;		//Rotate the vector on the x axis
			ForwardVector = Quaternion.AngleAxis(DiffMouse.x, new Vector3(0,1.0f,0)) * ForwardVector;		//Rotate the vector on the y axis
			ForwardVector = Quaternion.AngleAxis(DiffMouse.z, new Vector3(0,0,1.0f)) * ForwardVector;		//Rotate the vector on the z axis

			ForwardVector.Normalize();

			DiffMouse = new Vector3(0,0,0);
		}	
	}

}
