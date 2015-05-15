using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using System;

public class SubmitScore : MonoBehaviour {
	public InputField name;
	public Text score;
	[SerializeField] private Button DoneButton = null; // assign in the editor
	
	void Start() { 
		DoneButton.onClick.AddListener(() => { 
			Highscores.AddNewHighscore("asdf", Int32.Parse(score.text));
		});
	}

}
