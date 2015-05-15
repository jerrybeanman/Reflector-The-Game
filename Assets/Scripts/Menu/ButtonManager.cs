using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {
	public string difficulty;
	public string level;
	[SerializeField] private Button MyButton = null; // assign in the editor
	
	void Start() { 
			MyButton.onClick.AddListener (() => { 
				LoadLevel ();
			});	
	}
	
	void LoadLevel(){
			AutoFade.LoadLevel("D" + difficulty + "L" + level, 1,3, Color.gray);
		}
}

