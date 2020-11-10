using UnityEngine;
using System.Collections;

public class HighscoresA : MonoSingleton<HighscoresA>
{
	const string privateCode = "wefNdaK66Uaz9RZfHCsHVg5k-uUR1Iukai5MUclJmUZg";
	const string publicCode = "5f52241feb371809c40c0965";
	const string webURL = "http://dreamlo.com/lb/";

	public DisplayHighScoresA highscoreDisplay;
	public Highscore[] highscoresList;

	void Awake()
	{
		//highscoreDisplay = GetComponent<DisplayHighScores>();
	}

	public static void AddNewHighscore(string username, int score)
	{
		instance.StartCoroutine(instance.UploadNewHighscore(username, score));
	}

	IEnumerator UploadNewHighscore(string username, int score)
	{
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
		yield return www;

		if (string.IsNullOrEmpty(www.error))
		{
			print("Upload Successful");
			DownloadHighscores();
		}
		else
		{
			print("Error uploading: " + www.error);
		}
	}

	public void DownloadHighscores()
	{
		StartCoroutine("DownloadHighscoresFromDatabase");
	}

	IEnumerator DownloadHighscoresFromDatabase()
	{
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		yield return www;

		if (string.IsNullOrEmpty(www.error))
		{
			FormatHighscores(www.text);
			if (highscoreDisplay != null)
				highscoreDisplay.OnHighscoresDownloaded(highscoresList);
		}
		else
		{
			print("Error Downloading: " + www.error);
		}
	}

	void FormatHighscores(string textStream)
	{
		string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new Highscore[entries.Length];

		for (int i = 0; i < entries.Length; i++)
		{
			string[] entryInfo = entries[i].Split(new char[] { '|' });
			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);
			highscoresList[i] = new Highscore(username, score);
			print(highscoresList[i].username + ": " + highscoresList[i].score);
		}
	}

	public static void addAllHighscore()
	{
		int allHighScore = PlayerPrefs.GetInt("MiniGame_1_HighScore",0) + PlayerPrefs.GetInt("MiniGame_2_HighScore",0) + PlayerPrefs.GetInt("MiniGame_3_HighScore",0) + PlayerPrefs.GetInt("MiniGame_4_HighScore",0) + PlayerPrefs.GetInt("MiniGame_5_HighScore",0);
		PlayerPrefs.SetInt("All_HighScore", allHighScore);
		PlayerPrefs.Save();
		AddNewHighscore(PlayerPrefs.GetString("Name"),allHighScore);
	}

}

