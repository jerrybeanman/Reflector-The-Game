using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {
	public string difficulty;
	public static string staticDifficulty;
	public static int[] maps;
	[SerializeField] private Button MyButton = null; // assign in the editor
	
	void Start() { 
		MyButton.onClick.AddListener (() => { 
			staticDifficulty = difficulty;
			LoadLevel ();
		});
	}
	
	void LoadLevel(){
		if (difficulty.Equals ("1")) {
			maps = RandomLevelGenerator.linearMapPool();
			AutoFade.LoadLevel ("D" + difficulty + "L" + maps [0], 1, 3, Color.gray);
		} else {
			maps = RandomLevelGenerator.randomMapPool (RandomLevelGenerator.getNumberOfMaps ("difficulty" + difficulty + "-map"));
			AutoFade.LoadLevel ("D" + difficulty + "L" + maps [0], 1, 3, Color.gray);
		}
	}

	public int getMapsLength() {
		return maps.Length;
	}

	public string getDifficulty () {
		return difficulty;
	}

	public int[] getMaps() {
		return maps;
	}
}

