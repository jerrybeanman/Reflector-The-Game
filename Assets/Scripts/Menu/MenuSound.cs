using UnityEngine;
using System.Collections;

public class MenuSound : MonoBehaviour {
	
	AudioSource sound;

	void Awake() {
		sound = GetComponent<AudioSource>();
	}
	void onMouseEnter() { 
		print ("sfdlksdf");
		sound.Play();
	}
	void OnGUI () {

		if(GUI.Button(new Rect(Screen.width / 2 - 180,200,400,80), "play")) {
			sound.Play();	
		}

		else if(GUI.Button(new Rect(Screen.width / 2 - 140,300,320,40), "tutorial")) {		
			sound.Play();
		}		
		
		else if(GUI.Button(new Rect(Screen.width / 2 - 140,350,320,40), "quit")) {			
			sound.Play();
		}	
		
	}
}
