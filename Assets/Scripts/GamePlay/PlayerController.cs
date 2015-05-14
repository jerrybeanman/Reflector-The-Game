using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;



public class PlayerController : MonoBehaviour {
	
	public InputHistory inputs;									//Reference to the InputHistory script to instantiate direction arrows during runtime
	public WheelController wheel; 								//Reference to the WheelController script to enable flip mechanic
	private ArrowManager[] arrows;								//Reference to the ArrowManager script to access Animator component on the arrow gameobject
	public static bool isPlayed;								//Check if return key is pressed
	private bool isFlip;										//Check if grid has been flipped
	public static bool collided;								//Check if player has collided with an obstacle or wall
	public static bool stranded;								//If the player is stranded, the level will fail
	public static bool levelComplete;
	AudioSource PlayerDeath;
	AudioSource LevelCompleteSound;

	private int temp;											
	public static int level = 0;								//Current level of tier the player object is on

	private List<string> inputHistory = new List<string> ();	//Array to simulate keystroke's using array of strings

	CharacterController controller;								//The CharacterController component that is attached to the player object
			
	private Vector3 curPos;										//Current Vector3 of the player
	private Vector3 newPos;										//The new Vector3 of the player to move to
	private Vector3 direction;									//The direction value for raycasting obstacles and walls

	
	private float fingerStartTime  = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;
	private bool isSwipe = false;
	private float minSwipeDist  = 50.0f;
	private float maxSwipeTime = 0.5f;

	void Awake(){
		collided = false;		//The player has yet to collide with anything
		isPlayed = false;		//The user has yet to finish entering their inputs
		levelComplete = false;
	}

	void Start(){
		controller = GetComponent<CharacterController>();
		temp = level;
		AudioSource[] sounds = GetComponents<AudioSource>();
		LevelCompleteSound = sounds [0];
		PlayerDeath = sounds [1];

	}
	
	
	void Update() {
		recordInputs ();												//Record any keystrokes entered by the user into the inputHistory array
		recordButtons ();
		if (Input.GetKeyDown (KeyCode.Return) && isPlayed == false) {	//If user presses the return key and only pressed once
			isPlayed = true;
			StartCoroutine ("RelayedInput");							//Move the player according to the user inputs
		}
	}


	private bool isWheelClicked = false;
	private bool isRunClicked = false;
	private bool isUndoClicked = false;
	void OnGUI() {
		float height = Screen.height;
		float width = Screen.width;
		if (GUI.Button (new Rect (10, 250, 40, 40), "Flip!")) {
			isWheelClicked = true;
		} else if (GUI.Button (new Rect (90, 250, 40, 40), "Run!")) {	
			isRunClicked = true;
		} else if (GUI.Button (new Rect (170, 250, 40, 40), "Undo")) {
			isUndoClicked = true;
		}
	}
	
	void recordButtons() {
		if (isWheelClicked) {    
			recordInputsHelper ("Space");
			isWheelClicked = false;
		} else if (isRunClicked && isPlayed == false) {
			isPlayed = true;
			StartCoroutine ("RelayedInput");							//Move the player according to the user inputs
			isRunClicked = false;
		} else if (isUndoClicked) {
			recordInputsHelper("Delete");
			isUndoClicked = false;
		}
	}
	
	
	
	void recordInputs(){
		if (isPlayed == false) {										//If user has not entered the return key
#if UNITY_EDITOR
			if (Input.GetKeyDown (KeyCode.RightArrow)) 	{recordInputsHelper("R");} else 		//Store each keystrokes
			if (Input.GetKeyDown (KeyCode.LeftArrow)) 	{recordInputsHelper("L");} else 		//as a string value into 
			if (Input.GetKeyDown (KeyCode.UpArrow)) 	{recordInputsHelper("F");} else  		//the inputHistory array
			if (Input.GetKeyDown (KeyCode.DownArrow)) 	{recordInputsHelper("B");} else  		//and instantiate the arrow
			if (Input.GetKeyDown (KeyCode.Space)) 		{recordInputsHelper("Space");} else 		//gameobject at the same time
			if (Input.GetKeyDown (KeyCode.Backspace))	{recordInputsHelper("Delete");}
#endif
#if UNITY_ANDROID
			if (Input.touchCount > 0){
				
				foreach (Touch touch in Input.touches)
				{
					switch (touch.phase)
					{
					case TouchPhase.Began :
						/* this is a new touch */
						isSwipe = true;
						fingerStartTime = Time.time;
						fingerStartPos = touch.position;
						break;
						
					case TouchPhase.Canceled :
						/* The touch is being canceled */
						isSwipe = false;
						break;
						
					case TouchPhase.Ended :
						
						float gestureTime = Time.time - fingerStartTime;
						float gestureDist = (touch.position - fingerStartPos).magnitude;
						
						if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist){
							Vector2 direction = touch.position - fingerStartPos;
							Vector2 swipeType = Vector2.zero;
							
							if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
								// the swipe is horizontal:
								swipeType = Vector2.right * Mathf.Sign(direction.x);
							}else{
								// the swipe is vertical:
								swipeType = Vector2.up * Mathf.Sign(direction.y);
							}
							
							if(swipeType.x != 0.0f){
								if(swipeType.x > 0.0f){
									recordInputsHelper("R");
								}else{
									recordInputsHelper("L");
								}
							}
							
							if(swipeType.y != 0.0f ){
								if(swipeType.y > 0.0f){
									recordInputsHelper("F"); 
								}else{
									recordInputsHelper("B");
								}
							}
							
						}
						
						break;
					}
				}
			}
#endif	
	}
}

	public void recordInputsHelper(string direction){
		if (direction == "Delete") {
			GameObject[] a = GameObject.FindGameObjectsWithTag ("Arrow");
			Destroy (a [a.Length - 1]);
			inputHistory.RemoveAt (inputHistory.Count - 1);
		} else 
			inputHistory.Add (direction); inputs.makeArrows(direction);	//Reduce code usage for the recordInputs() method
	}

	public IEnumerator RelayedInput(){
		int i, j;
		stranded = false;
		arrows = FindObjectsOfType (typeof(ArrowManager)) as ArrowManager[];					//Find all GameObjects with the ArrowManager script attached
		for (i = 0, j = inputHistory.Count; i < inputHistory.Count && j > 0; i++, j--) {	//Traverse the inputHistory back and forth at the same time

			if(wheel.getCollided() && inputHistory[i] == "Space" ){								//If player is on wheel, pressed space bar, and wheel has not been flipped
				wheel.setCollidedFalse();														//Prevets a single wheel from activating twice
				wheel.flip ();																	//Flip all obstacles
			}else{
				getDirection(inputHistory[i]);													//Evaluate the correct position to move to
				Move (j);																		//Move the player towards that position
			}
			yield return new WaitForSeconds(0.8f);												//Wait .8 seconds before moving again
		}
		//If the play gets stranded, i.e doesn't complete the level or fail
		//Moves on to the next level and player scores 0
		if(levelComplete == false && i == (inputHistory.Count) ) {
			stranded = true;
			StartCoroutine(LoadNextLevelFail());
		}
	}
	
	void Move(int j){
		RaycastHit hit;															
		Ray landingRay = new Ray (curPos, direction);
		if (Physics.Raycast (landingRay, out hit, 1)) {								//If player has detect a collidable object towards the moving direction
			if ((hit.collider.tag == "Obstacle" || hit.collider.tag == "Wall") && collided == true) {		//If the object is an Obstacle or a wall
				collided = true;					
				newPos = curPos;													//Don't move
				//PlayerDeath.Play ();
				MoveHelper (j, true); 
				//GameOverManager.levelsPlayed++;
				//level++;
				//nextLevel();
				StartCoroutine(LoadNextLevelFail());
	
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
			StartCoroutine(setArrowAnimation (j));
	}

	IEnumerator setArrowAnimation(int j){
		arrows [j - 1].SetMove ();
		yield return new WaitForSeconds (0.75f);
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
		PlayerDeath.Play ();
		GameOverManager.levelsPlayed++;
		level++;
		yield return new WaitForSeconds(1.5f);
		nextLevel();
	}
	
	void nextLevel() {
		int lev = Int32.Parse(LevelReader.Difficulty);
		//For the tutorial, we want the levels to play in sequence
		if (lev == 1) {
			AutoFade.LoadLevel ("D" + LevelReader.Difficulty + "L" + (level + 1), .75f, .75f, Color.black);
		} else {
			if(level != LevelReader.maps.Length)
				AutoFade.LoadLevel("D" + LevelReader.Difficulty + "L" + LevelReader.maps[level], .75f, .75f, Color.black);
		}
	}

	void OnTriggerEnter(Collider hit){
		if (hit.gameObject.tag == "End" && temp == level) {
			GameOverManager.levelsPlayed++;
			level++;

			levelComplete = true;
			StartCoroutine(LoadNextLevel(LevelCompleteSound));
		}
	}

	public static void setStrandedFalse() {
		stranded = false;
	}
}