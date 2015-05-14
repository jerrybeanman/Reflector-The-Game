using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

public class PlayAgainButton : MonoBehaviour {
	
	[SerializeField] private Button MainMenuButton = null; // assign in the editor
	
	void Start() { 
		MainMenuButton.onClick.AddListener(() => { 
			LoadLevel();
		});
	}
	
	void LoadLevel(){
		AutoFade.LoadLevel("D" + LevelReader.Difficulty + "L1", 1,3, Color.gray);
	}
}
