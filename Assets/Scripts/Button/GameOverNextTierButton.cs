using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using System;

public class GameOverNextTierButton : MonoBehaviour {

	[SerializeField] private Button MainMenuButton = null; // assign in the editor
	public static int[] maps;
	public string difficulty;
	
	void Start() {
		difficulty = ButtonManager.staticDifficulty;
		MainMenuButton.onClick.AddListener(() => { 
			Loadlevel();
		});
	}
	
	void Loadlevel(){
		GameOverManager.score = 0;
		PlayerController.level = 0;
		int difficultyInt = Int32.Parse (difficulty);
		difficultyInt++;
		if (difficultyInt < RandomLevelGenerator.MAXLEVELS || difficultyInt == 8) {	// the '== 8' is so there is a transition from complexity 7 to 8
			if(difficultyInt == 8) {	// this is for any complexity levels that need to play the next level sequence
				maps = RandomLevelGenerator.linearMapPool("difficulty" + difficultyInt + "-map");
			} else {
				maps = RandomLevelGenerator.randomMapPool (RandomLevelGenerator.getNumberOfMaps ("difficulty" + difficultyInt + "-map"));
			}
			ButtonManager.maps = maps;
			LevelReader.Difficulty = difficultyInt.ToString ();
			ButtonManager.staticDifficulty = difficultyInt.ToString ();

			AutoFade.LoadLevel ("D" + difficultyInt + "L" + maps [0], 1, 3, Color.gray);
		} else {
			print("No more levels to play, go to main menu");
		}
	}
}
