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

/*
			//login

		}

		if(GUILayout.Button("Achievement")) {
			// show achievements UI
			Social.ShowAchievementsUI();
		}
	}*/
}