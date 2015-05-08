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
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play ();
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
	// Returns boolean collided
	public bool getCollided() {
		return collided;
	}
	// Sets boolean collided to false
	public void setCollidedFalse() {
		collided = false;
	}
	

}
