using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class InGameGui : MonoBehaviour {

	private Button notQuit;
	private Button quit;
	public Text score;
	public Text totalScore;
	public Text Timer;						//Text component to display onto the canvas
	public Text level;						//Current tier and level the user is on
	public float startTime;					//The starting count down time
	public static int second;				//the unit is in seconds
	public static	int lev;
	private Animator anim;
	public static bool paused = false;


	void Awake(){
		anim = GetComponent<Animator> ();
		lev = Int32.Parse (LevelReader.Difficulty);
		second = (int)startTime;
		level.text = (PlayerController.level+1).ToString();
		if (lev < 7 && lev != 1) { // Excludes tutorial levels and complexity levels
			score.text = GameOverManager.score.ToString ();
			totalScore.text = "Score: " + score.text;
		} else
			score.text = "∞";
			totalScore.text = "Score: " + "∞";
	}

	void Start(){
		if (!ButtonManager.staticDifficulty.Equals ("1")) { // There is no Andriod back button support in the tutorial, so this prevents null exceptions
			notQuit = GameObject.Find ("NoButton").GetComponent<Button> ();
			notQuit.onClick.AddListener (() => {
				anim.SetTrigger ("notQuit");
				paused = false;
			});

			quit = GameObject.Find ("QuitButton").GetComponent<Button> ();
			quit.onClick.AddListener (() => {
				print ("In quit");
				Application.Quit ();
			});
		}
	}

	void LateUpdate () {
		second = (int)startTime;
		if (lev < 7 && lev != 1) { // Excludes tutorial levels and complexity levels
			Timer.text = second.ToString();	//Display timer on canvas
			if (InputReader.isPlayed == false && second != 0 && paused == false)			//Decrement second as long as the timer hasn't reached zero
				startTime -= Time.deltaTime;
			totalScore.text = "Score: " + GameOverManager.score.ToString ();
		} else {
			Timer.text = "∞";
			totalScore.text = "Score: " + "∞";
		}

		// If B is pressed on a computer, or Andriod back button, 
		if (Input.GetKeyDown (KeyCode.B) || Input.GetKey(KeyCode.Escape)) {
				paused = true;
				anim.SetTrigger("Quit");
		}

	}

}

