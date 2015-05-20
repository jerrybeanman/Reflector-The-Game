using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class login_achieve_Button : MonoBehaviour {
	[SerializeField] private Button login = null; // assign in the editor
	[SerializeField] private Button achievement = null; // assign in the editor
			
	AudioSource sound;
			
			void Awake() {
				sound = GetComponent<AudioSource>();	
				PlayGamesPlatform.Activate();	
			}
			
			void Start() { 
					login.onClick.AddListener(() => { 
						print("anyhin");
					Social.localUser.Authenticate((bool success) => {
						// handle success or failure
						if (success) {
							Debug.Log("success");
						} else {
							Debug.Log("failed");
						}
					});
				});

				achievement.onClick.AddListener(() => { 
					Social.ShowAchievementsUI();
				});
			}

}
