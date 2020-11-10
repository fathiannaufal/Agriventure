using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI highScore;

    public void ResetHighScore(){
        PlayerPrefs.SetInt("MiniGame_4_HighScore", 0);
        highScore.text = PlayerPrefs.GetInt("MiniGame_4_HighScore", 0).ToString();
    }

    public void UpdateHighScore(){
        highScore.text = PlayerPrefs.GetInt("MiniGame_4_HighScore", 0).ToString();
        if(PlayerPrefs.HasKey("MiniGame_4_HighScore"))
            Highscores.AddNewHighscore(PlayerPrefs.GetString("Name"), PlayerPrefs.GetInt("MiniGame_4_HighScore"));
    }
}
