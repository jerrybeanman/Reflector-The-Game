using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System;

public class GridController : MonoBehaviour {
	//---------Pieces of the game-----------//
	public Transform player;
	public Transform floor;
	public Transform obstacle;
	public Transform wall;
	public Transform end;
	public Transform wheel;
	public Transform flipLine;

	//---------Map variables-----------//
	public string levelDifficulty;

	//---------Holds the level information scanned in from a text file-----------//
	public string[][] level;

	//---------Allows the text file characters to be matched to their corresponding objects-----------//
	private const string sWall = "0";
	private const string sObstacle = "1";
	private const string sEnd = "E";	
	private const string sStart = "S";
	private const string sWheel = "W";
	private const string sObstacleAndEnd = "Z";
	private const string sBlankSpace = "b";
	private const string sFloor = "f";

	//---------Variables used to determine the height, width and angle of the map-----------//
	private static float width;
	private static float height;
	private static int angle;
	//private static Vector3 centerOfMap;
	private float object_height = 0.7f;
	private float floor_height = 0.2f;

	//----------------------------------------------//
	
	void Awake() {
		string[] currentSceneName = Regex.Split (Application.loadedLevelName, @"\D+");
		levelDifficulty = currentSceneName [1];
		// Reads our text file and stores it in the array
		level = readFile (Application.dataPath + "/StreamingAssets" + "/difficulty" + currentSceneName[1] + "-map" + currentSceneName[2] + ".txt");
		// Sets map height
		height = level.Length;
		// Sets map width
		width = level [0].Length;
		// Sets the angle of the map
		angle =	 getAngle ();
		// Instantiates objects on the game board based on our level array
		spawnLevel ();
		// Creates our 'flip line' on the board
		spawnLine ();
		// spawn the boundary
		spawnBoundary();
	}

	// Spawns the level when called
	void spawnLevel(){
		//---------Create level base on text file-----------//
		
		for (int z = 0; z < height; z++) {
			for (int x = 0; x < width; x++) {
				// evaluate condition and instantiate game object
				switch (level[z][x]){
				case sFloor:
					Instantiate(floor, new Vector3(x, floor_height , -z), Quaternion.identity);
					break;
				case sBlankSpace:
					Instantiate(wall, new Vector3(x, object_height, -z), Quaternion.identity);
					break;
				case sWall :
					Instantiate(wall, new Vector3(x, object_height, -z), Quaternion.identity);
					Instantiate(floor, new Vector3(x, floor_height , -z), Quaternion.identity);
					break;
				case sStart:
					Instantiate(player, new Vector3(x, object_height, -z), Quaternion.identity);
					Instantiate(floor, new Vector3(x, floor_height , -z), Quaternion.identity);
					break;
				case sObstacle:
					Instantiate(obstacle, new Vector3(x, object_height, -z), Quaternion.identity);
					Instantiate(floor, new Vector3(x, floor_height , -z), Quaternion.identity);
					break;
				case sEnd:
					Instantiate(end, new Vector3(x, object_height, -z), Quaternion.identity);
					Instantiate(floor, new Vector3(x, floor_height , -z), Quaternion.identity);
					break;
				case sWheel:
					Instantiate(wheel, new Vector3(x, object_height, -z), Quaternion.identity);
					Instantiate(floor, new Vector3(x, floor_height , -z), Quaternion.identity);
					break;
				case sObstacleAndEnd:
					Instantiate(obstacle, new Vector3(x, object_height, -z), Quaternion.identity);
					Instantiate(end, new Vector3(x, object_height, -z), Quaternion.identity);
					Instantiate(floor, new Vector3(x, floor_height, -z), Quaternion.identity);
					break;
				}
			}
		}        
	}
	// Reads and stores the angle that the map will 'flip' over
	int getAngle(){	
		// Holds the parsed angle, as a string, from our level file
		string angleOfFlipString = "";

		// If the width is anything but 1, read the last character of the last line for the angle of flip.
		if (width != 1 && height != 1) {
			angleOfFlipString = level[level.Length -1][level [0].Length];
		}
		// If the height = 1, reads the character in the file which is the angle of flip. (0, 45, 90, 315, or 2 if no angle)
		if (height == 1) {
			angleOfFlipString = level [level.Length -1][level [0].Length - 1];
			width = width - 1;
		}
		// If the width = 1, reads the character in the file which is the angle of flip. (0, 45, 90, 315, or 2 if no angle)
		if (width == 1) {
			angleOfFlipString = level [level.Length - 1] [level [0].Length];
		}

		// Changes our string to an int.
		int angleOfFlipInt = Int32.Parse (angleOfFlipString);
		return angleOfFlipInt;
	}

	// Spawns the 'flip line' on the map
	void spawnLine(){
		// Our only valid angles are 0, 45, 90, 315. If we dont want our map to have a flip line, we set it to '2' in the text file.
		if (angle != 2) {
			Instantiate (flipLine, new Vector3 (width / 2 - .5f, .7f, -height / 2 + .5f), Quaternion.Euler (90, angle, 0));
		}
	}

	// Reads our level text file and stores the information in a jagged array, then returns that array
	public string[][] readFile(string file){
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

	// Takes in a GameObject and vector3
	// Instantiates a new obstacle at its new position, then removes the old object
	void reInstantiate(GameObject obstacle, Vector3 newPos){
		Instantiate (obstacle, newPos, Quaternion.identity);
		Destroy (obstacle);
	}

	// Flips obstacles over the map axis
	public void flip(){
		// New vector3 to move to
		Vector3 newPos;
		// Holds all obstacles on the current map
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
		for (int i = 0; i < obstacles.Length; i++) {
			// Depending on the angle, flips objects over the correct axis
			switch (angle) {
			case 0:
				newPos = new Vector3(width - obstacles[i].transform.position.x - 1, object_height , obstacles[i].transform.position.z);
				reInstantiate(obstacles[i], newPos);
				break;
			case 90:
				newPos = new Vector3(obstacles[i].transform.position.x, object_height, -height - obstacles[i].transform.position.z + 1);
				reInstantiate(obstacles[i], newPos);
				break;
			case 45:
				//needs the newPos calcuated from algorithm based on width and length passed from the parameter
				newPos = new Vector3(height + obstacles[i].transform.position.z - 1, object_height, -width + obstacles[i].transform.position.x + 1);
				reInstantiate(obstacles[i], newPos);
				break;
			case 315:
				//needs the newPos calcuated from algorithm based on width and length passed from the parameter
				newPos = new Vector3(-obstacles[i].transform.position.z, object_height, -obstacles[i].transform.position.x);
				reInstantiate(obstacles[i], newPos);
				break;
			}
		}
	}
	void spawnBoundary(){
		var northWall = Instantiate (wall, new Vector3 (width / 2 - 0.5f, 0.5f, 1), Quaternion.identity) as GameObject;
		northWall.transform.localScale = new Vector3 (width, 2, 1);

		var eastWall = Instantiate (wall, new Vector3 (width, 0.5f, -height / 2 + 0.5f), Quaternion.identity) as GameObject;
		eastWall.transform.localScale = new Vector3 (1, 2, height);

		var southWall = Instantiate (wall, new Vector3 (width / 2 - 0.5f, 0.5f, -height), Quaternion.identity) as GameObject;
		southWall.transform.localScale = new Vector3 (width, 2, 1);

		var westWall = Instantiate (wall, new Vector3 (-1, 0.5f, -height / 2 + 0.5f), Quaternion.identity) as GameObject;
		westWall.transform.localScale = new Vector3 (1, 2, height);
	}
		
	// Returns the center of the map
	public Vector3 getCenter(){
		return new Vector3 (width / 2f - .5f, (width + height)*1.3f, -height / 2f + .5f);
	}

}
	