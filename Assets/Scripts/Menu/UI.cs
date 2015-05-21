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
	
}
