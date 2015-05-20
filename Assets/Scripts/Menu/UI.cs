using UnityEngine;
using System.Collections;
//added to initalize google play
using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class UI : MonoBehaviour {

	AudioSource sound;

	void Awake() {
		sound = GetComponent<AudioSource>();
		//initalize google play
		PlayGamesPlatform.Activate();
	}
	
	public void DisableBoolInAnimator(Animator anim) {
		anim.SetBool("isDisplayed", false);
	}
	
	public void EnableBoolInAnimator(Animator anim) {
		anim.SetBool("isDisplayed", true);
		sound.Play();
	}
	
	public void NavigateTo(int scene) {
		Application.LoadLevel (scene);

	}

	public void OnGUI() {
		if(GUILayout.Button ("LOGIN")) {
			//login
			Social.localUser.Authenticate((bool success) => {
				// handle success or failure
				if (success) {
					Debug.Log("login success");
				} else {
					Debug.Log("login failed");
				}
			});
		}

		if (GUILayout.Button ("ACHIEVEMENTS")) {
			// show achievements UI
			Social.ShowAchievementsUI();
		}

		if (GUILayout.Button ("HIGHSCORES")) {
			// show leaderboard UI
			Social.ShowLeaderboardUI();
		}

		if (GUILayout.Button ("POST SCORE")) {
			// post scores
			Social.ReportScore(12345, "CgkI4O3alMQIEAIQBA", (bool success) => {
				// handle success or failure
				if (success) {
					Debug.Log("post success");
				} else {
					Debug.Log("post failed");
				}
			});
		}

		if (GUILayout.Button ("POST ACHIEVE")) {
			// post achevement
			Social.ReportProgress("CgkI4O3alMQIEAIQAQ", 100.0f, (bool success) => {
				// handle success or failure
				if (success) {
					Debug.Log("ach success");
				} else {
					Debug.Log("ach failed");
				}
			});
		}
	}
}
