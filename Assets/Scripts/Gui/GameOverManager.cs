﻿using UnityEngine;

public class GameOverManager : MonoBehaviour
{
	public InGameGui timer;
	Animator anim;                          // Reference to the animator component.
	public static int score = 0;

	public static int levelsPlayed = 0;
	void Awake ()
	{
		// Set up the reference.
		anim = GetComponent <Animator> ();
	}
	
	private bool addOnce = false;
	void Update ()
	{
		if(PlayerController.collided == true || InGameGui.second == 0 || PlayerController.stranded == true)
		{
			anim.SetTrigger ("Level Failed");
			score += 0;
			PlayerController.setStrandedFalse();
		}
		if (PlayerController.levelComplete == true) {
			anim.SetTrigger("Level Complete");
			if(addOnce == false){
				score += InGameGui.second * 10;
				addOnce = true;
			}
		}
		if (PlayerController.levelComplete == true && levelsPlayed == ButtonManager.maps.Length && !LevelReader.Difficulty.Equals("1")) {
			print ("game over");
			anim.SetTrigger("Tier Complete");
			levelsPlayed = 0;
		}

		if (levelsPlayed == 8 && LevelReader.Difficulty.Equals ("1")) {
			print ("Tutorial Over");
			anim.SetTrigger("Tier Complete");
			levelsPlayed = 0;
		}
	}
}