using UnityEngine;
using System.Collections;

public class LightLerp : MonoBehaviour {

	public float smooth = 2;

	private Light lt;
	private Color oriColor;

	// Use this for initialization
	void Start () {
		lt = GetComponent<Light> ();
		oriColor = lt.color;
	}
	
	// Update is called once per frame
	void Update () {
		ColorChanging ();
	}

	void ColorChanging(){
		if (Input.GetButtonDown ("Horizontal")) {
			lt.color = Color.Lerp (lt.color, Color.red, Time.deltaTime * smooth);
		} else 
		if (Input.GetButtonDown ("Vertical")) {
			lt.color = Color.Lerp(lt.color, Color.blue, Time.deltaTime * smooth);
		}
	}
}
