using UnityEngine;

public class GameOverManager : MonoBehaviour
{
	public InGameGui timer;
	Animator anim;                          // Reference to the animator component.
	public static int score = 0;

	public static int levelsPlayed = 0;
	void Awake ()
	{
		// Set up the reference.
		anim = GetComponent <Animator> ();
	}
	
	
	void Update ()
	{
		if(PlayerController.collided == true || InGameGui.second == 0 || PlayerController.stranded == true)
		{
			anim.SetTrigger ("Level Failed");
			score += 0;
			
		}
		if (PlayerController.levelComplete == true) {
			anim.SetTrigger("Level Complete");
			score += InGameGui.second * 100;
		}
		if (PlayerController.levelComplete == true && levelsPlayed == LevelReader.maps.Length) {
			print ("game over");
			anim.SetTrigger("Tier Complete");
			levelsPlayed = 0;
		}
	}
}