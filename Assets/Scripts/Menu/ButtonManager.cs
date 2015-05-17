using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {
	public string difficulty;
	public static string staticDifficulty;
	public static int[] maps;
	//public RandomLevelGenerator randomLevelGen;
	[SerializeField] private Button MyButton = null; // assign in the editor

	void Awake() {

	}

	void Start() { 
		MyButton.onClick.AddListener (() => { 
			staticDifficulty = difficulty;
			LoadLevel ();
		});
	}
	
	void LoadLevel(){
		maps = RandomLevelGenerator.mapPool (RandomLevelGenerator.getNumberOfMaps ("difficulty" + difficulty + "-map")); //fix "1"
		AutoFade.LoadLevel("D" + difficulty + "L" + maps[0], 1,3, Color.gray); //fix "1"
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

