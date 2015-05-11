using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
public class LevelReader : MonoBehaviour {

	public static string[][] Level;
	public static string Difficulty;
	public static string Map;
	// Use this for initialization
	void Awake () {
		string[] currentSceneName = Regex.Split (Application.loadedLevelName, @"\D+");
		Difficulty = currentSceneName [1];
		Map = currentSceneName [2];
		// Reads our text file and stores it in the array
		Level = readFile ("jar:file://" + Application.dataPath + "!/assets/" + "/difficulty" + currentSceneName[1] + "-map" + currentSceneName[2] + ".txt");
	}
	
	// Reads our level text file and stores the information in a jagged array, then returns that array
	string[][] readFile(string file){
		string text = System.IO.File.ReadAllText(file);
		string[] lines = Regex.Split(text, "\r\n");
		int rows = lines.Length;
		
		string[][] levelBase = new string[rows][];
		for (int i = 0; i < lines.Length; i++)  {
			string[] stringsOfLine = Regex.Split(lines[i], " ");
			levelBase[i] = stringsOfLine;
		}
		return levelBase;
	}
}
