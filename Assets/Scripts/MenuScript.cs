using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using System.Linq;

public class MenuScript : MonoBehaviour {

	[SerializeField] private Button MyButton = null; // assign in the editor
	public GameObject world;
	public GameObject background;
	public GameObject eventsystem;
	public GameObject directionalLight;
	public GameObject canvasUI;
	public readonly int mapDifficulty = 6;
	public readonly int levelsPerDifficulty = 7;
	public int[] randomLevel = new int[7]; // length 8


	// Use this for initialization
	void Start() {

		int count = 0;
		int[] numberContainer = new int[7];

		while (count < levelsPerDifficulty) {
			int number = Random.Range (0, levelsPerDifficulty-1);
			if(!numberContainer.Contains(number)) {
				numberContainer[count] = number;
				count++;
			}
		}
		//for (int i = 0; i < levelsPerDifficulty; i++) {
		//	print(randomLevel[i]);
		//}

		MyButton.onClick.AddListener(() => { 
			Instantiate(world);
			Instantiate(background);
			Instantiate(eventsystem);
			Instantiate(directionalLight);
			Instantiate(canvasUI);
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
