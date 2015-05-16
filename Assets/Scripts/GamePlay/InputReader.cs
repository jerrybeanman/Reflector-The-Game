using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputReader : MonoBehaviour {
	public InputHistory inputs;									//Reference to the InputHistory script to instantiate direction arrows during runtimepublic 
	public List<string> inputStrings;			//Array to simulate keystroke's using array of strings

	void Awake(){
		inputStrings = new List<string> ();
	}

	public void recordInputs(){
			#if UNITY_EDITOR
			if (Input.GetKeyDown (KeyCode.RightArrow)) 	{recordInputsHelper("R");} else 		//Store each keystrokes
			if (Input.GetKeyDown (KeyCode.LeftArrow)) 	{recordInputsHelper("L");} else 		//as a string value into 
			if (Input.GetKeyDown (KeyCode.UpArrow)) 	{recordInputsHelper("F");} else  		//the inputHistory array
			if (Input.GetKeyDown (KeyCode.DownArrow)) 	{recordInputsHelper("B");} else  		//and instantiate the arrow
			if (Input.GetKeyDown (KeyCode.Space)) 		{recordInputsHelper("Space");} else 	//gameobject at the same time
			if (Input.GetKeyDown (KeyCode.Backspace))	{recordInputsHelper("D");}
			#endif
	}

	void recordInputsHelper(string direction){
		inputStrings.Add (direction); inputs.makeArrows(direction);	//Reduce code usage for the recordInputs() method
		if(direction == "D"){
				GameObject[] a = GameObject.FindGameObjectsWithTag("Arrow");
				Destroy(a[a.Length -1]);
				inputStrings.RemoveAt(inputStrings.Count-1);
		}
	}
}
