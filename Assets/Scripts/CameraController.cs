using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GridController grid;
	
	// Use this for initialization
	void Start () {
		transform.position =  (grid.getCenter ());
	}
	
	// Update is called once per frame
	void Update () {
	}
}
