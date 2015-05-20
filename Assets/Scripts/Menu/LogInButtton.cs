using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class LogInButtton : MonoBehaviour {
	[SerializeField] private Button LogInButton = null; // assign in the editor
	
			AudioSource sound;
			
			void Awake() {
				sound = GetComponent<AudioSource>();	
				//PlayGamesPlatform.Activate();	
			}
			
			void Start() { 
				LogInButton.onClick.AddListener(() => { 
					Social.localUser.Authenticate((bool success) => {
						// handle success or failure
						if (success) {
							Debug.Log("success");
						} else {
							Debug.Log("failed");
						}
					});
				});

				LogInButton.onClick.AddListener(() => { 
					Social.ShowAchievementsUI();
				});
			}

}
