using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

public class PlayAgainButton : MonoBehaviour {
	
	[SerializeField] private Button MainMenuButton = null; // assign in the editor
	public static int[] maps;
	public string difficulty;
	
	void Start() { 
		difficulty = ButtonManager.staticDifficulty;
		MainMenuButton.onClick.AddListener(() => { 
			LoadLevel();
		});
	}
	
	void LoadLevel(){
		maps = RandomLevelGenerator.randomMapPool (RandomLevelGenerator.getNumberOfMaps ("difficulty" + difficulty + "-map"));
		AutoFade.LoadLevel ("D" + difficulty + "L" + maps [0], 1, 3, Color.gray);
	}
}
