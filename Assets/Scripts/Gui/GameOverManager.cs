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
	
	private bool addOnce = false;
	void Update ()
	{
		if(PlayerController.collided || InGameGui.second == 0 || PlayerController.stranded)
			setAnim("LF");
		if (PlayerController.levelComplete) 
			setAnim ("LC");
		if (PlayerController.levelComplete && levelsPlayed == ButtonManager.maps.Length) {// && !ButtonManager.staticDifficulty.Equals("1")) {
			setAnim ("LC");	setAnim ("TC");	}
		if ((PlayerController.collided || InGameGui.second == 0 || PlayerController.stranded) && levelsPlayed == ButtonManager.maps.Length) {
			setAnim ("LF");	setAnim ("TC");	}
	}
	
	void setAnim(string trigger){
		switch (trigger) {
		case "LF" :
			anim.SetTrigger ("Level Failed");
			score += 0;
			PlayerController.setStrandedFalse();
			break;
		case "LC" : 
			anim.SetTrigger("Level Complete");
			if(addOnce == false){
				score += InGameGui.second * 10;
				addOnce = true;
			}
			break;
		case "TC" : 
			anim.SetTrigger("Tier Complete");
			levelsPlayed = 0;
			break;
		}
	}
}