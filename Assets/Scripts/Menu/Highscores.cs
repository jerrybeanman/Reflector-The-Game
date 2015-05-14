using UnityEngine;
using System.Collections;

public class Highscores : MonoBehaviour {

	const string privateCode="7jevSE0ZTUKRIOW-Z_EDlw1zTR7v__jEKcTBj_HERY1A";
	const string publicCode="5553bf4c6e51b61c5428cf20";
	const string webURL="http://dreamlo.com/lb/";

	public Highscore[] highscoreList;
	static Highscores instance;
	displayHighscores highscoresDisplay;

	void Awake() {
		instance = this;
		highscoresDisplay = GetComponent<displayHighscores>();
	}

	//the upload method
	public static void AddNewHighscore(string username, int score) {
		instance.StartCoroutine(instance.UploadNewHighScore(username, score));
	}

	//uploads to the web database
	IEnumerator UploadNewHighScore(string username, int score){
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
		yield return www;

		if (string.IsNullOrEmpty (www.error)) {
			print ("Upload Successful");
			DownloadHighscores ();
		}else {
			print("Error uploading: " + www.error);
		}
	}

	//the download method
	public void DownloadHighscores(){
		StartCoroutine("DownloadingHighScoreFromDatabase");
	}

	//downloads from the web database
	IEnumerator DownloadingHighScoreFromDatabase(){
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		yield return www;

		if (string.IsNullOrEmpty (www.error)) {
			FormatHighscores (www.text);
			highscoresDisplay.OnHighscoresDownload (highscoreList);
		} else {
			print("Error downloading: " + www.error);
		}
	}

	//breaks up the string downloaded into name and score
	void FormatHighscores(string textStream) {
		string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
		
		highscoreList = new Highscore[entries.Length];

		for (int i = 0; i < entries.Length; i++) {
			string[] entryInfo = entries[i].Split(new char[] {'|'});
			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);
			highscoreList[i] = new Highscore(username, score);
			print(highscoreList[i].username + ": " + highscoreList[i].score);
		}
	}
}

public struct Highscore {
	public string username;
	public int score;

	public Highscore(string _username, int _score) {
		username = _username;
		score = _score;
	}
}
