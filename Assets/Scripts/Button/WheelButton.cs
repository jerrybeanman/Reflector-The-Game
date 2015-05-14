using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

public class WheelButton : MonoBehaviour {
	public PlayerController player;	
	[SerializeField] private Button wheel; // assign in the editor
	private bool flipPressed = false;
	void Start() { 
		wheel = GetComponent<Button> ();
		wheel.onClick.AddListener(() => { 
			flipPressed = true;
		});
	}	
}	