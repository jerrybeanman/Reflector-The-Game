using UnityEngine;

public class GameOverManager : MonoBehaviour
{
	public InGameGui timer;
	Animator anim;                          // Reference to the animator component.
	
	public static int levelsPlayed = 0;
	void Awake ()
	{
		// Set up the reference.
		anim = GetComponent <Animator> ();
	}
	
	
	void Update ()
	{
		if(PlayerController.collided == true || timer.second == 0)
		{
			anim.SetTrigger ("Level Failed");

		}
		if (PlayerController.levelComplete == true) {
			anim.SetTrigger("Level Complete");
		}
		if (PlayerController.levelComplete == true && levelsPlayed == LevelReader.maps.Length) {
			print ("game over");
			anim.SetTrigger("Tier Complete");
			levelsPlayed = 0;
		}
	}
}