using UnityEngine;
using System.Collections;

public class SpinStar : MonoBehaviour {
	
	public float RandomRotationStrenght;
	
	
	void Update () {
		transform.Rotate(RandomRotationStrenght,RandomRotationStrenght,RandomRotationStrenght);
	}
}
