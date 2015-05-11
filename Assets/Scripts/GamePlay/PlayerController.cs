using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour {
	
	public InputHistory inputs;									//Reference to the InputHistory script to instantiate direction arrows during runtime
	public WheelController wheel; 								//Reference to the WheelController script to enable flip mechanic
	private ArrowManager[] arrows;								//Reference to the ArrowManager script to access Animator component on the arrow gameobject
	private bool isPlayed;										//Check if return key is pressed
	private bool isFlip;										//Check if grid has been flipped
	public static bool collided;								//Check if player has collided with an obstacle or wall

	AudioSource PlayerDeath;
	AudioSource LevelComplete;

	private int temp;											
	private static int level = 0;								//Current level of tier the player object is on

	private List<string> inputHistory = new List<string> ();	//Array to simulate keystroke's using array of strings

	CharacterController controller;								//The CharacterController component that is attached to the player object
			
	private Vector3 curPos;										//Current Vector3 of the player
	private Vector3 newPos;										//The new Vector3 of the player to move to
	private Vector3 direction;									//The direction value for raycasting obstacles and walls


	void Awake(){
		collided = false;		//The player has yet to collide with anything
		isPlayed = false;		//The user has yet to finish entering their inputs
	}

	void Start(){
		controller = GetComponent<CharacterController>();
		temp = level;
		AudioSource[] sounds = GetComponents<AudioSource>();
		LevelComplete = sounds [0];
		PlayerDeath = sounds [1];
	}


	void Update() {
		recordInputs ();												//Record any keystrokes entered by the user into the inputHistory array
		if (Input.GetKeyDown (KeyCode.Return) && isPlayed == false) {	//If user presses the return key and only pressed once
			isPlayed = true;
			StartCoroutine ("RelayedInput");							//Move the player according to the user inputs
		}
	}
	
	void recordInputs(){
		if (isPlayed == false) {										//If user has not entered the return key

			if (Input.GetKeyDown (KeyCode.RightArrow)) 	{recordInputsHelper("R");} else 		//Store each keystrokes
			if (Input.GetKeyDown (KeyCode.LeftArrow)) 	{recordInputsHelper("L");} else 		//as a string value into 
			if (Input.GetKeyDown (KeyCode.UpArrow)) 	{recordInputsHelper("F");} else  		//the inputHistory array
			if (Input.GetKeyDown (KeyCode.DownArrow)) 	{recordInputsHelper("B");} else  		//and instantiate the arrow
			if (Input.GetKeyDown (KeyCode.Space)) 		{recordInputsHelper("Space");}  		//gameobject at the same time

		}
	}

	void recordInputsHelper(string direction){
		inputHistory.Add (direction); //inputs.makeArrows(direction);	//Reduce code usage for the recordInputs() method
	}

	IEnumerator RelayedInput(){
		arrows = FindObjectsOfType (typeof(ArrowManager)) as ArrowManager[];					//Find all GameObjects with the ArrowManager script attached
		for (int i = 0, j = inputHistory.Count; i < inputHistory.Count && j > 0; i++, j--) {	//Traverse the inputHistory back and forth at the same time
			if(wheel.getCollided() && inputHistory[i] == "Space" ){								//If player is on wheel, pressed space bar, and wheel has not been flipped
				wheel.setCollidedFalse();														//Prevets a single wheel from activating twice
				wheel.flip ();																//Flip all obstacles
			}else{
				getDirection(inputHistory[i]);													//Evaluate the correct position to move to
				Move (j);																		//Move the player towards that position
			}
			yield return new WaitForSeconds(1f);												//Wait 1 second before moving again
		}
	}

	void Move(int j){
		RaycastHit hit;															
		Ray landingRay = new Ray (curPos, direction);
		if (Physics.Raycast (landingRay, out hit, 1)) {								//If player has detect a collidable object towards the moving direction
			if (hit.collider.tag == "Obstacle" || hit.collider.tag == "Wall") {		//If the object is an Obstacle or a wall
				collided = true;					
				newPos = curPos;													//Don't move
				PlayerDeath.Play ();
				MoveHelper (j, true); 
				//StartCoroutine(LoadNextLevel(PlayerDeath));
			} else
				MoveHelper (j, false);
		} else
			MoveHelper (j, false);
	}

	void MoveHelper(int j, bool collided){
		if (collided)
			arrows [j - 1].SetCollided ();
		else
			arrows [j - 1].SetMove ();
			controller.Move(newPos - curPos);
	}

	void getDirection(string input){
		curPos = controller.transform.position;
		newPos = curPos;
		switch (input) {		//Evaluate position to move to based on string passsed in

		case "R" : 	getDirectionHelper(Vector3.right);		break;		//Evaluate each statement
		case "L" :	getDirectionHelper(Vector3.left);		break;		//accordingly and set 
		case "F" : 	getDirectionHelper(Vector3.forward);	break;		//the new position to move to
		case "B" : 	getDirectionHelper(Vector3.back);		break;		//and the direction to raycast

		}
	}

	void getDirectionHelper(Vector3 position){	//Reduces code usage for the getDirection() method
		newPos += position;
		direction = position;
	}


	
	IEnumerator LoadNextLevel(AudioSource sound) {
		yield return new WaitForSeconds(sound.clip.length);
		level++;
		Application.LoadLevel("D" + LevelReader.Difficulty + "L" + level);
	}

	void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.gameObject.tag == "End" && temp == level) {
			StartCoroutine(LoadNextLevel(LevelComplete));
			LevelComplete.Play();
		}
	}
}