﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayHighScores4 : MonoBehaviour
{

	public Text[] highscoreFields;

	void Start()
	{
		for (int i = 0; i < highscoreFields.Length; i++)
		{
			highscoreFields[i].text = i + 1 + ". Fetching...";
		}
		StartCoroutine("RefreshHighscores");
	}

	public void OnHighscoresDownloaded(Highscore[] highscoreList)
	{
		for (int i = 0; i < highscoreFields.Length; i++)
		{
			highscoreFields[i].text = i + 1 + ". ";
			if (i < highscoreList.Length)
			{
				highscoreFields[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
			}
		}
	}
	

	IEnumerator RefreshHighscores()
	{
		while (true)
		{
			Highscores4.instance.DownloadHighscores();
			yield return new WaitForSeconds(30);
		}
	}
}