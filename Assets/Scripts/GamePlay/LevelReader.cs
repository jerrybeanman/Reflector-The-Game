using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class LevelReader : MonoBehaviour {
	
	public GameObject canvasUI;
	public GameObject world;
	public static string[][] Level;
	public static string Difficulty;
	public static string Map;
	private readonly int LEVELSPERGAME = 4;
	public static int[] maps;
	
	// Use this for initialization
	void Awake () {
		string[] currentSceneName = Regex.Split (Application.loadedLevelName, @"\D+");			//Array that stores the difficulty and map name
		string fileName = "difficulty" + currentSceneName [1] + "-map" + currentSceneName [2];  //The name of the file that will be loaded
		TextAsset text = (TextAsset)Resources.Load (fileName, typeof(TextAsset));				//Load the file from the Resources folder
		Difficulty = currentSceneName [1];		
		Map = currentSceneName [2];
		Level = readFile (text);		//Read the text file and assign back into two dimensional array
		if (maps == null) {				//Ensures we only get one instance of our map array
			maps = mapPool (getNumberOfMaps ("difficulty" + Difficulty + "-map"));				//Fills our map array with random values
		}
	}

	// Reads our level text file and stores the information in a jagged array, then returns that array
	string[][] readFile(TextAsset t){
		string text = t.text;
		string[] lines = Regex.Split(text, "\r\n");
		int rows = lines.Length;
		
		string[][] levelBase = new string[rows][];
		for (int i = 0; i < lines.Length; i++)  {
			string[] stringsOfLine = Regex.Split(lines[i], " ");
			levelBase[i] = stringsOfLine;
		}
		return levelBase;
	}


	void Start(){
		Instantiate (canvasUI);
		Instantiate (world);
	}

	public int[] mapPool(int filesOfDifficulty) {
		int[] numberContainer = new int[LEVELSPERGAME];
		int count = 0;
		while (count < LEVELSPERGAME) {
			int number = Random.Range (2, filesOfDifficulty + 1);
			if (!numberContainer.Contains (number)) {
				numberContainer [count] = number;
				count++;
			}
		}
		return numberContainer;
	}
	// 
	int getNumberOfMaps (string fileName) {
		TextAsset text;

		int counter = 0;
		do {
			text = (TextAsset)Resources.Load (fileName + (counter + 1), typeof(TextAsset));
			counter++;
		} while(text != null);
		return (counter -1);
	}
}
