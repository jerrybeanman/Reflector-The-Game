using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

public class GameOverNextTierButton : MonoBehaviour {

	[SerializeField] private Button MainMenuButton = null; // assign in the editor
	
	void Start() { 
		MainMenuButton.onClick.AddListener(() => { 
			LoadLevel();
		});
	}
	
	void LoadLevel(){
		int newDifficulty = int.Parse (LevelReader.Difficulty) + 1;
		AutoFade.LoadLevel("D" + newDifficulty + "L1", 1,3, Color.gray);
	}
}
