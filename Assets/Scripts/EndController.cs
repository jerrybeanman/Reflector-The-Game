using UnityEngine;
using System.Collections;

public class EndController : MonoBehaviour {

	public float speed;
	
	void Update () 
	{
		transform.Rotate (new Vector3 (0, 45, 0) * Time.deltaTime * speed);
	}
}
