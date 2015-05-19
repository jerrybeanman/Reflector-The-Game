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
	public static bool happenOnce = false;


	void Awake(){
		int lev = Int32.Parse (LevelReader.Difficulty);
		second = (int)startTime;
		score.text = GameOverManager.score.ToString();
		totalScore.text = "Score: " + score.text;
		if (lev == 1) {
			level.text = LevelReader.Difficulty + "." + (PlayerController.level + 1); 
		} else
			level.text = LevelReader.Difficulty + "." + ButtonManager.maps[PlayerController.level];
	}
	
	// Update is called once per frame
	void Update () {
		second = (int)startTime;
		Timer.text = second + "s";	//Display timer on canvas
		if (InputReader.isPlayed == false && second != 0 )			//Decrement second as long as the timer hasn't reached zero
			startTime -= Time.deltaTime;


		if (second == 0 && happenOnce == false) {
			happenOnce = true;
			int lev = Int32.Parse(LevelReader.Difficulty);
			//For the tutorial, we want the levels to play in sequence
			if (lev == 1) {
				//print(PlayerController.level);
				//print("D" + ButtonManager.staticDifficulty + "L" + (ButtonManager.maps[PlayerController.level + 3]));
				AutoFade.LoadLevel ("D" + ButtonManager.staticDifficulty + "L" + (ButtonManager.maps[PlayerController.level + 2]), .75f, .75f, Color.black);

				//AutoFade.LoadLevel ("D" + LevelReader.Difficulty + "L" + (PlayerController.level + 2), .75f, .75f, Color.black);
			} else {
				if(PlayerController.level != ButtonManager.maps.Length)
					AutoFade.LoadLevel("D" + LevelReader.Difficulty + "L" + ButtonManager.maps[PlayerController.level], .75f, .75f, Color.black);
			}
		}
	}
}
