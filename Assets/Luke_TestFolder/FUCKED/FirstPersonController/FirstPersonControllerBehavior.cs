using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonControllerBehavior : MonoBehaviour {

	
	private Vector3 OldMouse;																			//The mouse position last frame
	private Vector3 CurMouse;																			//The mouse position this frame
	private Vector3 DiffMouse;																			//The difference in the mouse

	private Vector3 ForwardVector;																		//The direction we would like to look in
	private Vector3 HeadingVector;

	public float Sensitivity;																			//The sensitivity for looking on the screen
	public float SnapSensitivity;																		//The snapping sensitivity between the forward vector and the heading vector

	public float HightRestraint;																		//What is the restraint to use for gimble lock?

	void Start () {
		OldMouse = Input.mousePosition;																	//Use initial mouse position
		ForwardVector = new Vector3(0,0,1);																//We start by looking forward in the z axis
		HeadingVector = ForwardVector;																	//The way in which are actually looking
	}


	void Update () {

		DiffMouse = GetMouseInformation();																//Get the mouse information
		RotateForwardVec();																				//Rotate the forward vector
		RotateHeading();																				//Rotate the heading vector
		RotateFPC();																					//Rotate the First Personal Controller
	}

	private Vector3 GetMouseInformation(){
		float MouseX = Input.GetAxis("Mouse X");
		float MouseY = Input.GetAxis("Mouse Y");
		Vector3 Result = new Vector3(MouseX, MouseY, 0.0f);
		return Result;
	}

	private void PrintVec(Vector3 Vec){
		Debug.Log("( " + Vec.x + " , " + Vec.y + " , " + Vec.z + " ) " );									//Print out the vector in full
	}


	private void RotateForwardVec(){

		if(DiffMouse.x != 0 || DiffMouse.y != 0 || DiffMouse.z != 0){																							//If the diff vec is not 0

			float angle = Vector3.Angle(new Vector3(ForwardVector.x, 0, ForwardVector.z) , Vector3.forward);													//Angle of the forward vector and the forward of the world
			float leftAngle = Vector3.Angle(new Vector3(ForwardVector.x, 0, ForwardVector.z), Vector3.left);													//Angle of the forward vector and the left of the world

			if(angle <= 45.0f){																																	//If were in the forward quadrant 
				ForwardVector = Quaternion.Euler(-DiffMouse.y * Sensitivity, DiffMouse.x * Sensitivity, DiffMouse.z * Sensitivity) * ForwardVector;
			}
			else if (angle <= 90.0f){																															//If were in the left or right quadrant but in the front half 
				if(leftAngle <= 45.0f){
					ForwardVector = Quaternion.Euler(-DiffMouse.z * Sensitivity, DiffMouse.x * Sensitivity, -DiffMouse.y * Sensitivity) * ForwardVector;
				}
				else{
					ForwardVector = Quaternion.Euler(-DiffMouse.z * Sensitivity, DiffMouse.x * Sensitivity, DiffMouse.y * Sensitivity) * ForwardVector;
				}
			}
			else if (angle <= 135.0f){																															//If were in the left or right quadrant but in the back half

				if(leftAngle <= 45.0f){
					ForwardVector = Quaternion.Euler(DiffMouse.z * Sensitivity, DiffMouse.x * Sensitivity, -DiffMouse.y * Sensitivity) * ForwardVector;
				}
				else{
					ForwardVector = Quaternion.Euler(DiffMouse.z * Sensitivity, DiffMouse.x * Sensitivity, DiffMouse.y * Sensitivity) * ForwardVector;
				}
			}
			else{																																				//If were in the back quadrant
				ForwardVector = Quaternion.Euler(DiffMouse.y * Sensitivity, DiffMouse.x * Sensitivity, DiffMouse.z * Sensitivity) * ForwardVector;
			}

			PreventGimbleLock();																																//Prevent gimble lock for the forward vector
			ForwardVector.Normalize();																															//Normalize the forward vector
			DiffMouse = new Vector3(0,0,0);																														//Reset the diff vec
		}	
	}

	private void RotateFPC(){
		transform.Rotate(-transform.rotation.eulerAngles);																										//Rotate the controller object to have 0 rotation on all axis
		transform.LookAt(transform.position + HeadingVector);																									//Lookat the heading vector
	}

	private void RotateHeading(){
		HeadingVector = Vector3.Lerp(HeadingVector, ForwardVector, SnapSensitivity);																			//Lerp the heading vector by some amount
	}

	private void PreventGimbleLock(){
		if(ForwardVector.y > HightRestraint){
			ForwardVector.y = HightRestraint;
		}
		else if(ForwardVector.y < -HightRestraint){
			ForwardVector.y = -HightRestraint;
		}
	}

}
