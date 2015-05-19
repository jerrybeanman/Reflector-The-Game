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
			LoadLevel (difficulty);
		});
	}
	
	public static void LoadLevel(string diff){
		if (staticDifficulty.Equals ("1")) {
			maps = RandomLevelGenerator.linearMapPool();
			AutoFade.LoadLevel ("D" + diff + "L" + maps [0], 1, 3, Color.gray);
		} else {
			maps = RandomLevelGenerator.randomMapPool (RandomLevelGenerator.getNumberOfMaps ("difficulty" + diff + "-map"));
			print("Level to load " + "D" + diff + "L" + maps [0]);
			AutoFade.LoadLevel ("D" + diff + "L" + maps [0], 1, 3, Color.gray);
			print("Level to load " + "D" + diff + "L" + maps [0]);
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

