using UnityEngine;
using System.Collections;

public class ArrowManager : MonoBehaviour {

	private Animator anim;
	private AudioSource au;
	void Start(){
		anim = GetComponent<Animator> ();
		au = GetComponent<AudioSource> ();
		au.Play ();
	}

	public void SetMove(){
		anim.SetTrigger("Move");
	}
	
	public void SetCollided(){
		anim.SetTrigger ("Collided");
	}
}
