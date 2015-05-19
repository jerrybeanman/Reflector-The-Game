using UnityEngine;
using System.Collections;
using System.Linq;

public class RandomLevelGenerator : MonoBehaviour {

	private static readonly int LEVELSPERGAME = 1;
	private static readonly int LEVELSINTUTORIAL = 8;

	public static int[] randomMapPool(int filesOfDifficulty) {
		int[] numberContainer = new int[LEVELSPERGAME];
		int count = 0;
		while (count < LEVELSPERGAME) {
			int number = Random.Range (2, filesOfDifficulty + 1);
			if (!numberContainer.Contains (number)) {
				print(number);
				numberContainer [count] = number;
				count++;
			}
		}
		return numberContainer;
	}
	// 
	public static int getNumberOfMaps (string fileName) {
		TextAsset text;
		
		int counter = 0;
		do {
			text = (TextAsset)Resources.Load (fileName + (counter + 1), typeof(TextAsset));
			counter++;
		} while(text != null);
		return (counter -1);
	}

	public static int[] linearMapPool() {
		int[] numberContainer = new int[LEVELSINTUTORIAL];
		for (int i = 0; i < LEVELSINTUTORIAL; i++) {
			numberContainer[i] = (i + 1);
		}
		return numberContainer;
	}
}
