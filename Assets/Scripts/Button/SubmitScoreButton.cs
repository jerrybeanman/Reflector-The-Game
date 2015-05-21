using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubmitScoreButton : MonoBehaviour {
	
	[SerializeField] private Button SubmitButton = null; // assign in the editor
	
	void Start() { 
		SubmitButton.onClick.AddListener(() => { 
			Animator anim = GetComponent<Animator>();
			anim.SetTrigger("Clicked");
			Social.ReportScore(GameOverManager.score, "CgkIj8vavqsJEAIQBg", (bool success) => {
				// handle success or failure
			});
		});
	}
}
