using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class InGameGui : MonoBehaviour {
	private Button mainMenu;
	private Button notmainMenu;
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
		if(!ButtonManager.staticTimer) {
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
				Application.Quit ();
			});
			
			notmainMenu = GameObject.Find("No").GetComponent<Button>();
			notmainMenu.onClick.AddListener(() =>{
				anim.SetTrigger("notMainMenu");
				paused = false;
			});
			
			mainMenu = GameObject.Find("Yes").GetComponent<Button>();
			mainMenu.onClick.AddListener(() =>{	
				AutoFade.LoadLevel("Rough", 1,3, Color.gray);
				paused = false;
				PlayerController.level = 0;
				GameOverManager.levelsPlayed = 0;
				GameOverManager.score = 0;
			});
		}
	}

	void LateUpdate () {
		second = (int)startTime;
		if(!ButtonManager.staticTimer) {
			Timer.text = second.ToString();											//Display timer on canvas
			if (InputReader.isPlayed == false && second != 0 && paused == false)	//Decrement second as long as the timer hasn't reached zero
				startTime -= Time.deltaTime;
			totalScore.text = "Score: " + GameOverManager.score.ToString ();
		} else {
			Timer.text = "∞";
			totalScore.text = "Score: " + "∞";
		}

		// If B is pressed on a computer, or Andriod back button, 
		if ((Input.GetKeyDown (KeyCode.B) || Input.GetKey(KeyCode.Escape) && paused == false)) {
				paused = true;
				anim.SetTrigger("Quit");
		}
		
		// If C is pressed on a computer, or Andriod back button, 
		if ((Input.GetKeyDown (KeyCode.C) || Input.GetKey(KeyCode.Menu) && paused == false)) {
				paused = true;
				anim.SetTrigger("MainMenu");
		}

	}

}

