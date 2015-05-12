using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine.UI;

public class LevelReader : MonoBehaviour {
	
	public GameObject canvasUI;
	public GameObject world;
	public static string[][] Level;
	public static string Difficulty;
	public static string Map;

	// Use this for initialization
	void Awake () {
		string[] currentSceneName = Regex.Split (Application.loadedLevelName, @"\D+");
		Difficulty = currentSceneName [1];
		Map = currentSceneName [2];
		string fileName = "difficulty" + currentSceneName [1] + "-map" + currentSceneName [2];
		string filePath = Application.dataPath + "/StreamingAssets/" + fileName;
		//string filePath = "jar:file://" + Application.dataPath + "!/assets" + fileName;
		// Reads our text file and stores it in the array
		TextAsset text = (TextAsset)Resources.Load (fileName, typeof(TextAsset));
		Level = readFile (text);
	}

	// Reads our level text file and stores the information in a jagged array, then returns that array
	string[][] readFile(TextAsset t){

		string text = t.text;
		print (text);
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
