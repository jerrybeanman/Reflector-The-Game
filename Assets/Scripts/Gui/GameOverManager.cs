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
		// End of level / tier conditions

		if ((PlayerController.collided || PlayerController.stranded || PlayerController.levelComplete) && levelsPlayed == ButtonManager.maps.Length && ButtonManager.staticDifficulty.Equals( "1")) {
			AutoFade.LoadLevel("Rough", 1,3, Color.gray);
			PlayerController.level = 0;
			GameOverManager.score = 0;	}
		if ((PlayerController.collided || InGameGui.second == 0 || PlayerController.stranded) && levelsPlayed == ButtonManager.maps.Length) {
			setAnim ("TC");	}
		if (PlayerController.collided || InGameGui.second == 0 || PlayerController.stranded) {
			setAnim ("LF"); }
		if (PlayerController.levelComplete && levelsPlayed == ButtonManager.maps.Length) {
			setAnim ("LC");	setAnim ("TC");	}
		if (PlayerController.levelComplete) {
			setAnim ("LC"); }
		// Achievements

		if (ButtonManager.staticDifficulty.Equals ("1") && tierComplete) {
			Social.ReportProgress("CgkIj8vavqsJEAIQAQ", 100.0f, (bool success) => {});
			tierComplete = false;
		}

		// Tier 1
		// Tier 1 Apprentice
		if (ButtonManager.staticDifficulty.Equals ("2") && tierComplete && score >= 700) {
			Social.ReportProgress("CgkIj8vavqsJEAIQAg", 100.0f, (bool success) => {});
			tierComplete = false;
		}
		// Tier 1 Adept
		if (ButtonManager.staticDifficulty.Equals ("2") && tierComplete && score >= 790) {
			Social.ReportProgress("CgkIj8vavqsJEAIQAw", 100.0f, (bool success) => {});
			tierComplete = false;
		}
		// Tier 1 Master
		if (ButtonManager.staticDifficulty.Equals ("2") && tierComplete && score >= 820) {
			Social.ReportProgress("CgkIj8vavqsJEAIQBw", 100.0f, (bool success) => {});
			tierComplete = false;
		}

		// Tier 2
		// Tier 2 Apprentice
		if (ButtonManager.staticDifficulty.Equals ("3") && tierComplete && score >= 700) {
			Social.ReportProgress("CgkIj8vavqsJEAIQCA", 100.0f, (bool success) => {});
			tierComplete = false;
		}
		// Tier 2 Adept
		if (ButtonManager.staticDifficulty.Equals ("3") && tierComplete && score >= 760) {
			Social.ReportProgress("CgkIj8vavqsJEAIQDg", 100.0f, (bool success) => {});
			tierComplete = false;
		}
		// Tier 2 Master
		if (ButtonManager.staticDifficulty.Equals ("3") && tierComplete && score >= 790) {
			Social.ReportProgress("CgkIj8vavqsJEAIQDw", 100.0f, (bool success) => {});
			tierComplete = false;
		}

		// Tier 3
		// Tier 3 Apprentice
		if (ButtonManager.staticDifficulty.Equals ("4") && tierComplete && score >= 700) {
			Social.ReportProgress("CgkIj8vavqsJEAIQEA", 100.0f, (bool success) => {});
			tierComplete = false;
		}
		// Tier 3 Adept
		if (ButtonManager.staticDifficulty.Equals ("4") && tierComplete && score >= 760) {
			Social.ReportProgress("CgkIj8vavqsJEAIQEQ", 100.0f, (bool success) => {});
			tierComplete = false;
		}
		// Tier 3 Master
		if (ButtonManager.staticDifficulty.Equals ("4") && tierComplete && score >= 790) {
			Social.ReportProgress("CgkIj8vavqsJEAIQEg", 100.0f, (bool success) => {});
			tierComplete = false;
		}

		// Tier 4
		// Tier 4 Apprentice
		if (ButtonManager.staticDifficulty.Equals ("5") && tierComplete && score >= 620) {
			Social.ReportProgress("CgkIj8vavqsJEAIQEw", 100.0f, (bool success) => {});
			tierComplete = false;
		}
		// Tier 4 Adept
		if (ButtonManager.staticDifficulty.Equals ("5") && tierComplete && score >= 700) {
			Social.ReportProgress("CgkIj8vavqsJEAIQFA", 100.0f, (bool success) => {});
			tierComplete = false;
		}
		// Tier 4 Master
		if (ButtonManager.staticDifficulty.Equals ("5") && tierComplete && score >= 740) {
			Social.ReportProgress("CgkIj8vavqsJEAIQFQ", 100.0f, (bool success) => {});
			tierComplete = false;
		}

		// Tier 5
		// Tier 5 Apprentice
		if (ButtonManager.staticDifficulty.Equals ("6") && tierComplete && score >= 600) {
			Social.ReportProgress("CgkIj8vavqsJEAIQFg", 100.0f, (bool success) => {});
			tierComplete = false;
		}
		// Tier 5 Adept
		if (ButtonManager.staticDifficulty.Equals ("6") && tierComplete && score >= 650) {
			Social.ReportProgress("CgkIj8vavqsJEAIQFw", 100.0f, (bool success) => {});
			tierComplete = false;
		}
		// Tier 5 Master
		if (ButtonManager.staticDifficulty.Equals ("6") && tierComplete && score >= 700) {
			Social.ReportProgress("CgkIj8vavqsJEAIQGA", 100.0f, (bool success) => {});
			tierComplete = false;
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