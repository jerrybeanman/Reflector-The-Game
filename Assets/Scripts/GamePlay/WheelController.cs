using UnityEngine;
using System.Collections;

public class WheelController : MonoBehaviour {
	public GridController grid;
	
	public static bool collided = false;
	public bool isFlip = false;

	// Happens when a trigger gets activated
	// If the player activates the trigger, set collided to true (player is on the 'wheel')
	void OnTriggerEnter(Collider hit) {
		AudioSource audio = GetComponent<AudioSource>();
		Animator anim = GetComponent<Animator> ();
		audio.Play ();
		anim.SetTrigger("Flip");
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
		var newObstacle = Instantiate (obstacle, newPos, Quaternion.identity) as GameObject;
		newObstacle.transform.localScale = new Vector3 (0.7f,0.8f,0.7f);
		Animator anim = newObstacle.GetComponent<Animator> ();
		anim.SetTrigger("Respawn");
		Destroy (obstacle);
		//Instantiate (obstacle, newPos, Quaternion.identity);
	}
	public bool getCollided(){
		return collided;
	}

	// Flips obstacles over the map axis
	public IEnumerator flip(){
		// New vector3 to move to
		Vector3 newPos;
		// Holds all obstacles on the current map
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
		Animator anim;
		for (int i = 0; i < obstacles.Length; i++) {
			// Depending on the angle, flips objects over the correct axis
			switch (GridController.angle) {
			case 0:
				newPos = new Vector3(GridController.width - obstacles[i].transform.position.x - 1, GridController.object_height , obstacles[i].transform.position.z);
				anim = obstacles[i].GetComponent<Animator>();
				anim.SetTrigger("Destroy");
				yield return new WaitForSeconds(0.6f);
				reInstantiate(obstacles[i], newPos);
				break;
			case 90:
				newPos = new Vector3(obstacles[i].transform.position.x, GridController.object_height, -GridController.height - obstacles[i].transform.position.z + 1);
				anim = obstacles[i].GetComponent<Animator>();
				anim.SetTrigger("Destroy");
				yield return new WaitForSeconds(0.6f);
				reInstantiate(obstacles[i], newPos);
				break;
			case 45:
				//needs the newPos calcuated from algorithm based on width and length passed from the parameter
				newPos = new Vector3(GridController.height + obstacles[i].transform.position.z - 1, GridController.object_height, -GridController.width + obstacles[i].transform.position.x + 1);
				anim = obstacles[i].GetComponent<Animator>();
				anim.SetTrigger("Destroy");
				yield return new WaitForSeconds(0.6f);
				reInstantiate(obstacles[i], newPos);
				break;
			case 315:
				//needs the newPos calcuated from algorithm based on width and length passed from the parameter
				newPos = new Vector3(-obstacles[i].transform.position.z, GridController.object_height, -obstacles[i].transform.position.x);
				anim = obstacles[i].GetComponent<Animator>();
				anim.SetTrigger("Destroy");
				yield return new WaitForSeconds(0.6f);
				reInstantiate(obstacles[i], newPos);
				break;
			}
		}
	}
}
