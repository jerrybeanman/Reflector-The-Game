using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public class InputHistory : MonoBehaviour {

	public GameObject rightArrow;
	public GameObject leftArrow;
	public GameObject upArrow;
	public GameObject downArrow;
	public GameObject space;
	
	private GameObject childObject;
	/**
	 *	Evaluate string passed in and instantiate the corresponding arrow GameObject  
	 */
	public void makeArrows(string direction){
		switch(direction){
	
		case "R" :	setParentAndPivot(rightArrow);	break;
		case "L" : 	setParentAndPivot(leftArrow);	break;
		case "F" :	setParentAndPivot(upArrow);		break;
		case "B" : 	setParentAndPivot(downArrow); 	break;
		case "Space" : setParentAndPivot(space);	break;
		}
	}
	

	void setParentAndPivot(GameObject arrow){
		GameObject parentObject = GameObject.FindGameObjectWithTag ("InputHistory");	//Search for gameobject with a tag InputHistory
		childObject = Instantiate (arrow) as GameObject;								//Instantitate arrow
		childObject.transform.SetParent (parentObject.transform, false);				//Make arrow a child object of InputHistory
	}
}
