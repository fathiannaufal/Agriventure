using UnityEngine;
using System.Collections;

public class Highscores3 : MonoSingleton<Highscores3>
{
	const string privateCode = "qVcLyjLKCkeQsejd_pBELQqHnmtAFEP0OQpAxlnMPZ2g";
	const string publicCode = "5f453d33eb371809c4eed124";
	const string webURL = "http://dreamlo.com/lb/";

	public DisplayHighScores3 highscoreDisplay;
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

}

