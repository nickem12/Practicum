using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class FirstPersonControllerBehavior : MonoBehaviour {

	private Vector3 DiffMouse;																			//The difference in the mouse

	private Vector3 ForwardVector;																		//The direction we would like to look in
	private Vector3 HeadingVector;

	public float Sensitivity;																			//The sensitivity for looking on the screen
	public float SnapSensitivity;																		//The snapping sensitivity between the forward vector and the heading vector

	public float HightRestraint;																		//What is the restraint to use for gimble lock?
	private Camera MainCam;

	private CharacterController CharController;															//The character controller component of the FPS controller
	private Vector3 MoveDir;																			//The current move dir
	private Vector3 ResultDir;																			//The result direction after all calculations

	public float gravity;																				//The gravity of the object
	public float jump;																					//The force of the jump
	public float speed; 																				//The speed at which we move
	private float DeltaHeight = 0.0f;																	//The change in height
	private float DeltaGravity;
	bool Groundedlast = false;

    public Canvas inventoryCanvas;
    public EventSystem eventSystem;
    private bool inventoryOn;
    public GameObject uiSoundManager;

    private static bool playerExists;
    public string startPoint;
    public bool canMove = true;


    void Start () {
		ForwardVector = new Vector3(0,0,1);																//We start by looking forward in the z axis
		HeadingVector = ForwardVector;																	//The way in which are actually looking
		MainCam = Camera.main;																			//Get the main camera of the scene (Also a child to this object)
		CharController = GetComponent<CharacterController>();											//Get the character controller component
		DeltaGravity = gravity;

        inventoryOn = false;

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }


	void Update () {
      
        MoveDir = GetInput();                                                                           //Get input from the player

        if(canMove)
        {
            DiffMouse = GetMouseInformation();                                                              //Get the mouse information
        }
      

        RotateForwardVec();																				//Rotate the forward vector
		RotateHeading();																				//Rotate the heading vector
		RotateFPC();                                                                                    //Rotate the First Personal Controller

        if (canMove)
        {
            MoveController();                                                                               //Move the controller
        }

        if (Input.GetButtonDown("Inventory"))
        {
            //uiSoundManager.GetComponent<UISoundManager>().PlayAudioClip(2);
            inventoryOn = !inventoryOn;
            if (inventoryOn)
            {
                eventSystem.SetSelectedGameObject(inventoryCanvas.gameObject.transform.GetChild(1).gameObject);
            }
            else
            {
                eventSystem.SetSelectedGameObject(null);
            }
        }

        inventoryCanvas.GetComponent<Canvas>().enabled = inventoryOn;
    }

	void FixedUpdate(){

		Debug.DrawLine(this.transform.position, this.transform.position + (MoveDir * 5));
		Debug.DrawLine(this.transform.position, this.transform.position + (HeadingVector * 2), Color.red);
	}

	private void MoveController(){
		
		ResultDir.x = MoveDir.x * speed;
		ResultDir.z = MoveDir.z * speed;

		if(Input.GetButton("Sprint")){
			ResultDir.x *= 2;
			ResultDir.z *= 2;
		}

		if(CharController.isGrounded){
			DeltaGravity = gravity;
			DeltaHeight = 0.0f;

			if(Input.GetButtonDown("Jump")){
				ResultDir.y = jump;
			}
		}

		if(!CharController.isGrounded) DeltaGravity = DeltaGravity + DeltaGravity * (1.5f * Time.deltaTime);
		ResultDir.y -= DeltaGravity * Time.deltaTime;
		CharController.Move(ResultDir * Time.deltaTime);											//Move the character controller
	}

	private Vector3 GetMouseInformation(){
		float MouseX = Input.GetAxis("Mouse X");
		float MouseY = Input.GetAxis("Mouse Y");
		Vector3 Result = new Vector3(MouseX, MouseY, 0.0f);
		return Result;
	}

	public void SetStartRotation(float RotationAmount){
		ForwardVector = Vector3.forward;
		ForwardVector = Quaternion.Euler(new Vector3(0,RotationAmount, 0)) * ForwardVector;																	//Rotate the forward vector by the start rotation amount
		HeadingVector = ForwardVector;
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
		MainCam.transform.Rotate(-MainCam.transform.rotation.eulerAngles);
		MainCam.transform.LookAt(MainCam.transform.position + HeadingVector);
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

	private Vector3 GetInput(){
		Vector3 InputDir =  new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0 , CrossPlatformInputManager.GetAxis("Vertical"));
		if(InputDir.x == 0.0f && InputDir.z == 0.0f)
			return Vector3.zero; 


		float Angle = Vector3.Angle(Vector3.forward, new Vector3(HeadingVector.x, 0, HeadingVector.z));															//Get the angle between the forward of the world and our heading
		float RightAngle = Vector3.Angle(Vector3.right, new Vector3(HeadingVector.x, 0, HeadingVector.z));														//The angle for the right to heading

		if(RightAngle >= 90.0f) Angle *= -1.0f;

		InputDir = Quaternion.Euler(0, Angle, 0) * InputDir;																									//Rotate the input dir by that angle on the y axis
		InputDir.Normalize();																																	//Normalize the vector
		return InputDir;																																		//Return the input direction
	}

}
