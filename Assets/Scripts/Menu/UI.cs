using UnityEngine;
using System.Collections;
//using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class UI : MonoBehaviour {

	AudioSource sound;

	void Awake() {
		sound = GetComponent<AudioSource>();	
		//PlayGamesPlatform.Activate();	
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
		if(GUILayout.Button("login")) {
			//login
			Social.localUser.Authenticate((bool success) => {
				// handle success or failure
				if (success) {
					Debug.Log("success");
				} else {
					Debug.Log("failed");
				}
			});
		}

		if(GUILayout.Button("Achievement")) {
			// show achievements UI
			Social.ShowAchievementsUI();
		}
	}
}