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
	public static int[] maps;
	
	// Use this for initialization
	void Awake () {
		string[] currentSceneName = Regex.Split(Application.loadedLevelName, @"\D+");
		Difficulty = currentSceneName [1];
		Map = currentSceneName [2];
		string fileName =  "difficulty" + Difficulty + "-map" + Map;
		TextAsset text = (TextAsset)Resources.Load (fileName, typeof(TextAsset));				//Load the file from the Resources folder

		Level = readFile (text);		//Read the text file and assign back into two dimensional array
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
}
