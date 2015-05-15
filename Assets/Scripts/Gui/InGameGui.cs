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


	void Awake(){
		int lev = Int32.Parse (LevelReader.Difficulty);
		second = (int)startTime;
		score.text = GameOverManager.score.ToString();
		totalScore.text = "Score: " + score.text;
		if (lev == 1) {
			level.text = LevelReader.Difficulty + "." + (PlayerController.level+1); 
		}else
		level.text = LevelReader.Difficulty + "." + LevelReader.maps[PlayerController.level];
	}
	
	// Update is called once per frame
	void Update () {
		second = (int)startTime;
		Timer.text = second + "s";	//Display timer on canvas
		if (PlayerController.isPlayed == false && second != 0 )			//Decrement second as long as the timer hasn't reached zero
			startTime -= Time.deltaTime;
	}
}
