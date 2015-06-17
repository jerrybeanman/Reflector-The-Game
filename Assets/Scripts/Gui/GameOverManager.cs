using UnityEngine;

public class GameOverManager : MonoBehaviour
{
	public InGameGui timer;
	Animator anim;                          // Reference to the animator component.
	public static int score = 0;
	private bool addOnce = false;
	public static int levelsPlayed = 0;
	private bool tierComplete = false;

	void Awake ()
	{
		// Set up the reference.
		anim = GetComponent <Animator> ();
	}

	void Update ()
	{
		if ((PlayerController.collided || PlayerController.stranded || PlayerController.levelComplete) && levelsPlayed == ButtonManager.maps.Length && ButtonManager.staticDifficulty.Equals( "1")) {
			AutoFade.LoadLevel("Rough", 1,3, Color.gray);
			PlayerController.level = 0;
			GameOverManager.score = 0;	}
		if ((PlayerController.collided || InGameGui.second == 0 || PlayerController.stranded) && levelsPlayed == ButtonManager.maps.Length) {
			setAnim ("TC");	}
		if (PlayerController.collided || InGameGui.second == 0 || PlayerController.stranded) {
			setAnim ("LF"); }
		if (PlayerController.levelComplete && levelsPlayed == ButtonManager.maps.Length) {// && !ButtonManager.staticDifficulty.Equals("1")) {
			setAnim ("LC");	setAnim ("TC");	}
		if (PlayerController.levelComplete) {
			setAnim ("LC"); }
		if (ButtonManager.staticDifficulty.Equals ("1") && tierComplete) {
			Social.ReportProgress("CgkIj8vavqsJEAIQAQ", 100.0f, (bool success) => {});
			tierComplete = false;
		}
		if (ButtonManager.staticDifficulty.Equals ("4") && tierComplete && score > 100) {
			Social.ReportProgress("CgkIj8vavqsJEAIQAg", 100.0f, (bool success) => {});
			tierComplete = false;
		}
		if (ButtonManager.staticDifficulty.Equals ("9") && tierComplete) {
			Social.ReportProgress("CgkIj8vavqsJEAIQAw", 100.0f, (bool success) => {});
			tierComplete = false;
		}

		// Android button controllers
		if (Application.platform == RuntimePlatform.Android) {
			if(Input.GetKey(KeyCode.Escape)) {
				// Prompt quit
				// Application.Quit();
				return;
			}
		}

		if (Application.platform == RuntimePlatform.Android) {
			if(Input.GetKey(KeyCode.Menu)) {
				// display menu
				// toggle sound
				// quit
				return;
			}
		}
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
			if(addOnce == false && InGameGui.lev < 7 && InGameGui.lev != 1){
				score += InGameGui.second * 10;
				timer.score.text = score.ToString();
				addOnce = true;
			}
			break;
		case "TC" : 
			anim.SetTrigger("Tier Complete");
			tierComplete = true;
			levelsPlayed = 0;
			break;
		}
	}
}