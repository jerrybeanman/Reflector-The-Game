using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
public class GameOverMainMenuButton : MonoBehaviour {

	[SerializeField] private Button MainMenuButton = null; // assign in the editor
	
	void Start() { 
		MainMenuButton.onClick.AddListener(() => { 
			LoadLevel();
		});
	}
	
	void LoadLevel(){
		AutoFade.LoadLevel("Rough", 1,3, Color.gray);
		PlayerController.level = 0;
		GameOverManager.score = 0;
		GameOverManager.levelsPlayed = 0;
		GameOverManager.tierComplete = true;
	}
}
