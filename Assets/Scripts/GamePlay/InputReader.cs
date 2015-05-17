using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputReader : MonoBehaviour {
	public InputHistory inputs;										//Reference to the InputHistory script to instantiate direction arrows during runtimepublic 
	public List<string> inputStrings = new List<string> ();			//Array to simulate keystroke's using array of strings
	public static bool isPlayed;
	

	private float fingerStartTime  = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;
	private bool isSwipe = false;
	private float minSwipeDist  = 50.0f;
	private float maxSwipeTime = 0.5f;

	void Awake(){
		isPlayed = false;
	}

	void Update(){
		recordInputs ();												//Record any keystrokes entered by the user into the inputHistory array
		recordButtons ();
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

	void recordInputsHelper(string direction){
		inputStrings.Add (direction); inputs.makeArrows(direction);	//Reduce code usage for the recordInputs() method
		if(direction == "D"){
				GameObject[] a = GameObject.FindGameObjectsWithTag("Arrow");
				Destroy(a[a.Length -1]);
				inputStrings.RemoveAt(inputStrings.Count-1);
		}
	}


	private bool isWheelClicked = false;
	private bool isRunClicked = false;
	private bool isUndoClicked = false;
	void OnGUI() {
		float height = Screen.height;
		float width = Screen.width;
		float UndoWidth = width - Screen.width * 0.9f;
		float UndoHeight = height - Screen.height * 0.2f;
		float FlipWidth = width - Screen.width * 0.6f;
		float FlipHeight = height - Screen.height * 0.2f;
		float RunWidth = width - Screen.width * 0.3f;
		float RunHeight = height - Screen.height * 0.2f;
		float ButtonSize = width * 0.2f;
		if (GUI.Button (new Rect (UndoWidth, UndoHeight, ButtonSize, ButtonSize), "Undo")) {
			isUndoClicked = true;
		} else if (GUI.Button (new Rect (FlipWidth, FlipHeight, ButtonSize, ButtonSize), "Flip!")) {	
			isWheelClicked = true;
		} else if (GUI.Button (new Rect (RunWidth, RunHeight, ButtonSize, ButtonSize), "Run")) {
			isRunClicked = true;
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
}
