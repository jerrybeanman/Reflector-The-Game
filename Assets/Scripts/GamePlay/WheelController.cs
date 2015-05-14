using UnityEngine;
using System.Collections;

public class WheelController : MonoBehaviour {
	public GridController grid;
	
	public static bool collided = false;
	public bool isFlip = false;



	// Happens when a trigger gets activated
	// If the player activates the trigger, set collided to true (player is on the 'wheel')
	void OnTriggerEnter(Collider hit) {
		if (hit.gameObject.tag == "Player" && isFlip == false) {
			isFlip = true;
			collided = true;
		}
	}
	// Sets collided back to false when the player steps off the trigger
	void OnTriggerExit(Collider hit){
		if (hit.gameObject.tag == "Player") {
			collided = false;
		}
	}
	// Sets boolean collided to false
	public void setCollidedFalse() {
		collided = false;
	}
	
	// Takes in a GameObject and vector3
	// Instantiates a new obstacle at its new position, then removes the old object
	void reInstantiate(GameObject obstacle, Vector3 newPos){
		Instantiate (obstacle, newPos, Quaternion.identity);
		Destroy (obstacle);
	}
	public bool getCollided(){
		return collided;
	}

	// Flips obstacles over the map axis
	public void flip(){
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play ();
		// New vector3 to move to
		Vector3 newPos;
		// Holds all obstacles on the current map
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
		for (int i = 0; i < obstacles.Length; i++) {
			// Depending on the angle, flips objects over the correct axis
			switch (GridController.angle) {
			case 0:
				newPos = new Vector3(GridController.width - obstacles[i].transform.position.x - 1, GridController.object_height , obstacles[i].transform.position.z);
				reInstantiate(obstacles[i], newPos);
				break;
			case 90:
				newPos = new Vector3(obstacles[i].transform.position.x, GridController.object_height, -GridController.height - obstacles[i].transform.position.z + 1);
				reInstantiate(obstacles[i], newPos);
				break;
			case 45:
				//needs the newPos calcuated from algorithm based on width and length passed from the parameter
				newPos = new Vector3(GridController.height + obstacles[i].transform.position.z - 1, GridController.object_height, -GridController.width + obstacles[i].transform.position.x + 1);
				reInstantiate(obstacles[i], newPos);
				break;
			case 315:
				//needs the newPos calcuated from algorithm based on width and length passed from the parameter
				newPos = new Vector3(-obstacles[i].transform.position.z, GridController.object_height, -obstacles[i].transform.position.x);
				reInstantiate(obstacles[i], newPos);
				break;
			}
		}
	}
}
