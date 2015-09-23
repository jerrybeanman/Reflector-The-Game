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
		if ((difficultyInt < RandomLevelGenerator.MAXLEVELS && !ButtonManager.staticTimer)) {
			maps = RandomLevelGenerator.randomMapPool (RandomLevelGenerator.getNumberOfMaps ("difficulty" + difficultyInt + "-map"));
			ButtonManager.maps = maps;
			LevelReader.Difficulty = difficultyInt.ToString ();
			ButtonManager.staticDifficulty = difficultyInt.ToString ();

			AutoFade.LoadLevel ("D" + difficultyInt + "L" + maps [0], 1, 3, Color.gray);
		} else { // If there are no more levels to play in a tier, game goes back to main menu COULD USE A WARNING TO THE PLAYER
			AutoFade.LoadLevel("Rough", 1,3, Color.gray);
			PlayerController.level = 0;
			GameOverManager.score = 0;
			GameOverManager.levelsPlayed = 0;
			GameOverManager.tierComplete = true;
		}
	}
}
