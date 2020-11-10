using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreMiniGame4: MonoBehaviour
{
    public Text highScore;

    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("MiniGame_4_HighScore").ToString();
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("MiniGame_4_HighScore");
    }
}
