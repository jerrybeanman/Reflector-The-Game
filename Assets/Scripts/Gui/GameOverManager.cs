using UnityEngine;

public class GameOverManager : MonoBehaviour
{
	public InGameGui timer;
	Animator anim;                          // Reference to the animator component.
	public static int score = 0;
	private bool addOnce = false;
	public static int levelsPlayed = 0;
	public static bool tierComplete = false;

	void Awake ()
	{
		// Set up the reference.
		anim = GetComponent <Animator> ();
	}

	void Update ()
	{

		// Achievements

		if (ButtonManager.staticDifficulty.Equals ("1") && tierComplete) {
			Social.ReportProgress ("CgkIj8vavqsJEAIQAQ", 100.0f, (bool success) => {});
			tierComplete = false;
		}

		// Tier 1
		// Tier 1 Apprentice
		if (ButtonManager.staticDifficulty.Equals ("2") && tierComplete && !ButtonManager.staticTimer) {
			if (score >= 700) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQAg", 100.0f, (bool success) => {});
			}
			// Tier 1 Adept
			if (score >= 790) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQAw", 100.0f, (bool success) => {});
			}
			// Tier 1 Master
			if (score >= 820) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQBw", 100.0f, (bool success) => {});
			}
			tierComplete = false;
		}

		// Tier 2
		// Tier 2 Apprentice
		if (ButtonManager.staticDifficulty.Equals ("3") && tierComplete && !ButtonManager.staticTimer) {
			if (score >= 700) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQCA", 100.0f, (bool success) => {});
			}
			// Tier 2 Adept
			if (score >= 760) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQDg", 100.0f, (bool success) => {});
			}
			// Tier 2 Master
			if (score >= 790) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQDw", 100.0f, (bool success) => {});
			}
			tierComplete = false;
		}

		// Tier 3
		// Tier 3 Apprentice
		if (ButtonManager.staticDifficulty.Equals ("4") && tierComplete && !ButtonManager.staticTimer) {
			if (score >= 700) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQEA", 100.0f, (bool success) => {});
			}
			// Tier 3 Adept
			if (score >= 760) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQEQ", 100.0f, (bool success) => {});
			}
			// Tier 3 Master
			if (score >= 790) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQEg", 100.0f, (bool success) => {});
			}
			tierComplete = false;
		}

		// Tier 4
		// Tier 4 Apprentice
		if (ButtonManager.staticDifficulty.Equals ("5") && tierComplete && !ButtonManager.staticTimer) {
			if (score >= 620) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQEw", 100.0f, (bool success) => {});
			}
			// Tier 4 Adept
			if (score >= 700) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQFA", 100.0f, (bool success) => {});
			}
			// Tier 4 Master
			if (score >= 740) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQFQ", 100.0f, (bool success) => {});
			}
			tierComplete = false;
		}

		// Tier 5
		// Tier 5 Apprentice
		if (ButtonManager.staticDifficulty.Equals ("6") && tierComplete && !ButtonManager.staticTimer) {
			if (score >= 600) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQFg", 100.0f, (bool success) => {});
			}
			// Tier 5 Adept
			if (score >= 650) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQFw", 100.0f, (bool success) => {});
			}
			// Tier 5 Master
			if (score >= 700) {
				Social.ReportProgress ("CgkIj8vavqsJEAIQGA", 100.0f, (bool success) => {});
			}
			tierComplete = false;
		}

		// End of level / tier conditions
		
		// if tutorial levels or normal levels, send the player back to the main menu on end of tier
		if(ButtonManager.staticTimer && PlayerController.levelComplete && levelsPlayed == ButtonManager.maps.Length) {
			AutoFade.LoadLevel ("Rough", 1, 3, Color.gray);
			PlayerController.level = 0;
			score = 0;
			levelsPlayed = 0;
			tierComplete = true;
		}
		if ((PlayerController.collided || InGameGui.second == 0 || PlayerController.stranded) && levelsPlayed == ButtonManager.maps.Length && ButtonManager.staticTimer == false) {
			setAnim ("TC");
		}
		if ((PlayerController.collided || InGameGui.second == 0 || PlayerController.stranded) && ButtonManager.staticTimer == false) {
			setAnim ("LF");
		}
		if ((PlayerController.levelComplete && levelsPlayed == ButtonManager.maps.Length) && ButtonManager.staticTimer == false) {
			setAnim ("LC");
			setAnim ("TC");
		}
		if (PlayerController.levelComplete) {
			setAnim ("LC");
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
			if(addOnce == false && !ButtonManager.staticTimer && InGameGui.lev != 1){
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