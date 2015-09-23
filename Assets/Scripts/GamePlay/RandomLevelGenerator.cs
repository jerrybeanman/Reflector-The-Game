using UnityEngine;
using System.Collections;
using System.Linq;

public class RandomLevelGenerator : MonoBehaviour {

	public static readonly int LEVELSPERGAME = 5;
	public static readonly int MAXLEVELS = 6; // this is the max levels for speed games

	public static int[] randomMapPool(int filesOfDifficulty) {
		int[] numberContainer = new int[LEVELSPERGAME];
		int count = 0;
		while (count < LEVELSPERGAME) {
			int number = Random.Range (1, filesOfDifficulty + 1);
			if (!numberContainer.Contains (number)) {
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
		return (counter - 1);
	}

	public static int[] linearMapPool(string fileName) {
		int numMaps = getNumberOfMaps (fileName);
		int[] numberContainer = new int[numMaps];
		for (int i = 0; i < numMaps; i++) {
			numberContainer[i] = (i + 1);
		}
		return numberContainer;
	}
}
