using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {
	public string difficulty;
	public static string staticDifficulty;
	public bool timer;
	public static bool staticTimer;
	public static int[] maps;
	[SerializeField] private Button MyButton = null; // assign in the editor
	
	void Start() { 
		MyButton.onClick.AddListener (() => { 
			staticDifficulty = difficulty;
			staticTimer = timer;
			LoadLevel ();
		});
	}
	
	public static void LoadLevel(){
		if (staticDifficulty.Equals ("1")) {
			maps = RandomLevelGenerator.linearMapPool("difficulty" + staticDifficulty + "-map");
			AutoFade.LoadLevel ("D" + staticDifficulty + "L" + maps [0], 1f, 3f, Color.gray);
		} else {
			maps = RandomLevelGenerator.randomMapPool (RandomLevelGenerator.getNumberOfMaps ("difficulty" + staticDifficulty + "-map"));
			AutoFade.LoadLevel ("D" + staticDifficulty + "L" + maps [0], 1f, 3f, Color.gray);
		}
	}
}

