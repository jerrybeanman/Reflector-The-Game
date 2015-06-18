using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class SubmitScoreButton : MonoBehaviour {
	
	[SerializeField] private Button SubmitButton = null; // assign in the editor
	
	void Start() { 
		SubmitButton.onClick.AddListener(() => { 
			Animator anim = GetComponent<Animator>();
			anim.SetTrigger("Clicked");
			postScore (Int32.Parse(LevelReader.Difficulty));
		});
	}


	void postScore(int lev){
		switch (lev) {
		case 2 :
			Social.ReportScore(GameOverManager.score, "CgkIj8vavqsJEAIQCQ", (bool success) => {});
			break;
		case 3 : 
			Social.ReportScore(GameOverManager.score, "CgkIj8vavqsJEAIQCg", (bool success) => {});
			break;
		case 4 : 
			Social.ReportScore(GameOverManager.score, "CgkIj8vavqsJEAIQCw", (bool success) => {});
			break;
		case 5 : 
			Social.ReportScore(GameOverManager.score, "CgkIj8vavqsJEAIQDA", (bool success) => {});
			break;
		case 6 : 
			Social.ReportScore(GameOverManager.score, "CgkIj8vavqsJEAIQDQ", (bool success) => {});
			break;
		}
	}
}
