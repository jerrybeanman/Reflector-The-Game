using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class login_achieve_Button : MonoBehaviour {
	[SerializeField] private Button achievement = null; // assign in the editor
	[SerializeField] private Button highscore = null; // assign in the editor

	AudioSource sound;
			
	void Awake() {
		sound = GetComponent<AudioSource>();	
		PlayGamesPlatform.Activate();	
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
			if (success) {
				Debug.Log("success");
			} else {
				Debug.Log("failed");
			}
		});
	}
			
	void Start() { 

		achievement.onClick.AddListener(() => { 
			Social.ShowAchievementsUI();
		});
			
		highscore.onClick.AddListener(() => { 
			Social.ShowLeaderboardUI();
		});

	}

}
