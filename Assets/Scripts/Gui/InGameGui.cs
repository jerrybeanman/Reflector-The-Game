using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class InGameGui : MonoBehaviour {


	public Text score;
	public Text totalScore;
	public Text Timer;				//Text component to display onto the canvas
	public Text level;				//Current tier and level the user is on
	public float startTime;	//The starting count down time
	public static int second;				//the unit is in seconds
	private bool happenOnce = false;
	public static	int lev;


	void Awake(){
		lev = Int32.Parse (LevelReader.Difficulty);
		second = (int)startTime;
		level.text = (PlayerController.level+1).ToString();
		if (lev < 7 && lev != 1) {
			score.text = GameOverManager.score.ToString ();
			totalScore.text = "Score: " + score.text;
		} else
			score.text = "∞";
			totalScore.text = "Score: " + "∞";
	}

	
	void LateUpdate () {
		second = (int)startTime;
		if (lev < 7 && lev != 1) {
			Timer.text = second + "s";	//Display timer on canvas
			if (InputReader.isPlayed == false && second != 0)			//Decrement second as long as the timer hasn't reached zero
				startTime -= Time.deltaTime;
			totalScore.text = "Score: " + GameOverManager.score.ToString ();
		} else {
			Timer.text = "∞";
			totalScore.text = "Score: " + "∞";
		}

	}
}

