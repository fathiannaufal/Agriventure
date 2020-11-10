using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCoreSets : MonoBehaviour
{
    public GameplayController GC;

    public int skor;
    public int highscore;
    public Text skorT;
    public Text skor_habis;

    void Update()
    {
        skor = GC.movCount * 10 * 31;
        skor_habis.text = skor.ToString("0");
        skorT.text = skor.ToString("0");

        if(PlayerPrefs.GetInt("MiniGame_3_HighScore") < skor)
        {
            highscore = skor;
            PlayerPrefs.SetInt("MiniGame_3_HighScore", highscore);
            Highscores3.AddNewHighscore(PlayerPrefs.GetString("Name"),highscore);
            HighscoresA.addAllHighscore();
        }
    }
}
