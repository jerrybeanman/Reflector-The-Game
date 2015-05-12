using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GridController grid;
	
	// Use this for initialization
	void Start () {
		transform.position =  (grid.getCenter ());
		transform.rotation = Quaternion.Euler(90,0,0);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
