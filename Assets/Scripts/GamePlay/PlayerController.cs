using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;



public class PlayerController : MonoBehaviour {
	public WheelController wheel; 								//Reference to the WheelController script to enable flip mechanic
	private ArrowManager[] arrows;								//Reference to the ArrowManager script to access Animator component on the arrow gameobject
	private bool isFlip;										//Check if grid has been flipped
	public static bool collided;								//Check if player has collided with an obstacle or wall
	public static bool stranded;								//If the player is stranded, the level will fail
	public static bool levelComplete;
	AudioSource PlayerDeath;
	AudioSource LevelCompleteSound;
	public bool failedOnce = false;
	
	private int temp;											
	public static int level = 0;								//Current level of tier the player object is on

	InputReader inputs;
	CharacterController controller;								//The CharacterController component that is attached to the player object
			
	private Vector3 curPos;										//Current Vector3 of the player
	private Vector3 newPos;										//The new Vector3 of the player to move to
	private Vector3 direction;									//The direction value for raycasting obstacles and walls
	
	private Button playButton;
	void Awake(){
		collided = false;		//The player has yet to collide with anything
		levelComplete = false;
	}

	void Start(){
		playButton = GameObject.Find ("PlayButton").GetComponent<Button> ();
		controller = GetComponent<CharacterController>();
		inputs = GetComponent<InputReader> ();
		temp = level;
		AudioSource[] sounds = GetComponents<AudioSource>();
		LevelCompleteSound = sounds [0];
		PlayerDeath = sounds [1];
		playButton.onClick.AddListener (() => {
			if(InputReader.isPlayed == false && InGameGui.paused == false){
				InputReader.isPlayed = true;
				StartCoroutine("RelayedInput");
			}
		});
	}

	private bool happenOnce = false;
	void Update() {
		if (Input.GetKeyDown (KeyCode.Return) && InputReader.isPlayed == false) {	//If user presses the return key and only pressed once
			InputReader.isPlayed = true;
			StartCoroutine ("RelayedInput");							//Move the player according to the user inputs
		}
		
		if (InGameGui.second == 0 && happenOnce ==false) {
			happenOnce = true;
			StartCoroutine(LoadNextLevelFail());
		}
	}

	public IEnumerator RelayedInput(){
		int i, j;
		stranded = false;
		arrows = FindObjectsOfType (typeof(ArrowManager)) as ArrowManager[];					//Find all GameObjects with the ArrowManager script attached
		for (i = 0, j = inputs.inputStrings.Count; i < inputs.inputStrings.Count && j > 0; i++, j--) {	//Traverse the inputHistory back and forth at the same time
			if(failedOnce || levelComplete) {
				break;
			}
			if(wheel.getCollided() && inputs.inputStrings[i] == "Space" ){						//If player is on wheel, pressed space bar, and wheel has not been flipped
				wheel.setCollidedFalse();														//Prevents a single wheel from activating twice
				yield return StartCoroutine(wheel.flip ());
			}else{
				getDirection(inputs.inputStrings[i]);													//Evaluate the correct position to move to
				Move (j);																		//Move the player towards that position
			}
			yield return new WaitForSeconds(1f);												//Wait 1 second before moving again
		}
		//If the player gets stranded, i.e doesn't complete the level or fail
		//Moves on to the next level and player scores 0
		if(levelComplete == false && i == (inputs.inputStrings.Count) && failedOnce == false){ //|| (levelComplete == false && failedOnce == false)) {
			stranded = true;
			failedOnce = true;
			StartCoroutine(LoadNextLevelFail());
		}
	}

	void Move(int j){
		RaycastHit hit;															
		Ray landingRay = new Ray (curPos, direction);
		if (Physics.Raycast (landingRay, out hit, 1)) {								//If player has detect a collidable object towards the moving direction
			if (hit.collider.tag == "Obstacle" || hit.collider.tag == "Wall") {		//If the object is an Obstacle or a wall
				collided = true;
				failedOnce = true;
				newPos = curPos;													//Don't move
				MoveHelper (j, collided); 
				StartCoroutine(LoadNextLevelFail());
			} else
				MoveHelper (j, false);
		} else
			MoveHelper (j, false);
	}

	void MoveHelper(int j, bool collided){
		if (collided) {
			arrows [j - 1].SetCollided ();
		}else
			StartCoroutine(setArrowAnimation (j));
	}

	IEnumerator setArrowAnimation(int j){
		arrows [j - 1].SetMove ();
		yield return new WaitForSeconds (0.75f);
		controller.transform.rotation = Quaternion.LookRotation (direction, Vector3.up);
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
		LevelCompleteSound.Play();
		yield return new WaitForSeconds(1.5f);
		nextLevel ();
	}
	
	IEnumerator LoadNextLevelFail() {
		if(ButtonManager.staticTimer) {	
			level--;
			GameOverManager.levelsPlayed--;
		}
		PlayerDeath.Play ();
		GameOverManager.levelsPlayed++;
		yield return new WaitForSeconds (1.5f);
		nextLevel ();
	}
	// Loads the next level
	void nextLevel() {
		level++;
		if (level < ButtonManager.maps.Length) {
			AutoFade.LoadLevel ("D" + ButtonManager.staticDifficulty + "L" + ButtonManager.maps [level], .9f, .9f, Color.black);
		}
	}
	
	void OnTriggerEnter(Collider hit){
		if (hit.gameObject.tag == "End" && temp == level) {
			GameOverManager.levelsPlayed++;
			levelComplete = true;
			StartCoroutine(LoadNextLevel(LevelCompleteSound));
		}
	}

	public static void setStrandedFalse() {
		stranded = false;
	}
}