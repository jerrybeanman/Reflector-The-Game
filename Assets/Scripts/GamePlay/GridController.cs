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
	public static float width;
	public static float height;
	public static int angle;
	//private static Vector3 centerOfMap;
	public static float object_height = 0.7f;
	public static float floor_height = 0.2f;

	//----------------------------------------------//
	private string[][] level;
	void Awake() {
		level = LevelReader.Level;
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
					Instantiate(end, new Vector3(x, object_height, -z), Quaternion.Euler(90,0,0));
					Instantiate(floor, new Vector3(x, floor_height , -z), Quaternion.identity);
					break;
				case sWheel:
					Instantiate(wheel, new Vector3(x, 0.2f, -z), Quaternion.identity);
					Instantiate(floor, new Vector3(x, floor_height , -z), Quaternion.identity);
					break;
				case sObstacleAndEnd:
					Instantiate(obstacle, new Vector3(x, object_height, -z), Quaternion.identity);
					Instantiate(end, new Vector3(x, object_height, -z), Quaternion.Euler(90,0,0));
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
		float cameraZoom = 1.4f;
		float mapSize = (width + height);
		if (mapSize < 5) {
			cameraZoom = 1.9f;
		}
		if (mapSize == 7) {
			cameraZoom = 1.3f;
		}
		if (mapSize > 7) {
			cameraZoom = 1.1f;
		}
		return new Vector3 (width / 2f - .5f, mapSize * cameraZoom, -height / 2f + .5f);
	}

}
	