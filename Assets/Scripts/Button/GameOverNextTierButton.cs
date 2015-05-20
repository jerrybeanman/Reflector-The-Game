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
		PlayerController.level = 0;
		int difficultyInt = Int32.Parse (difficulty);
		difficultyInt++;
		maps = RandomLevelGenerator.randomMapPool (RandomLevelGenerator.getNumberOfMaps ("difficulty" + difficultyInt + "-map"));
		ButtonManager.maps = maps;
		LevelReader.Difficulty = difficultyInt.ToString();
		ButtonManager.staticDifficulty = difficultyInt.ToString ();

		AutoFade.LoadLevel ("D" + difficultyInt + "L" + maps [0], 1, 3, Color.gray);
	}

	void ResetVariables () {

	}
}
