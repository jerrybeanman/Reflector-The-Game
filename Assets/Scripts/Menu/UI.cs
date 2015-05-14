using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	AudioSource sound;

	void Awake() {
		sound = GetComponent<AudioSource>();
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
