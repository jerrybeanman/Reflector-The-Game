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


	void Awake(){
		int lev = Int32.Parse (LevelReader.Difficulty);
		second = (int)startTime;
		score.text = GameOverManager.score.ToString();
		totalScore.text = "Score: " + score.text;
		level.text = LevelReader.Difficulty + "." + ButtonManager.maps[PlayerController.level];
	}
	
	// Update is called once per frame
	void LateUpdate () {
		second = (int)startTime;
		Timer.text = second + "s";	//Display timer on canvas
		if (InputReader.isPlayed == false && second != 0 )			//Decrement second as long as the timer hasn't reached zero
			startTime -= Time.deltaTime;

	}
}
