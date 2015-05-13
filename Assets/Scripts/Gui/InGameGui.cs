using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameGui : MonoBehaviour {

	public Text Timer;				//Text component to display onto the canvas
	public Text level;				//Current tier and level the user is on
	public float startTime;	//The starting count down time
	public int second;				//the unit is in seconds

	void Awake(){
		second = (int)startTime;
		level.text = LevelReader.Difficulty + "." + LevelReader.Map;
	}
	
	// Update is called once per frame
	void Update () {
		second = (int)startTime;
		Timer.text = second + "s";	//Display timer on canvas
		if (PlayerController.isPlayed == false && second != 0 )			//Decrement second as long as the timer hasn't reached zero
			startTime -= Time.deltaTime;
	}
}
