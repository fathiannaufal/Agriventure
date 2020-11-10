using UnityEngine;
using UnityEngine.UI;

public class HighscoreMiniGame1: MonoBehaviour
{

    public LevelManager lm;

    public Text highScore;
    public Text score;

    int number;

    int highscore;

    void Start()
    {
        highscore = PlayerPrefs.GetInt("MiniGame_1_HighScore",0);
            
    }

    public void Update()
    {
        if(lm != null)
        {
            //Debug.Log("number update");
            number = lm.totalHiddenObjectsFound;
            number = number * 5;
        }
        
       
        if(score != null)
            score.text = number.ToString();
        //if (PlayerPrefs.HasKey("MiniGame_1_HighScore"))
        //  PlayerPrefs.SetInt("Minigame_1_HighScore", number);
        if(highScore != null)
        {

           // Debug.Log("highscore : " + highscore);
            highScore.text = highscore.ToString();
        }
            
    }


    public void setHighscore()
    {
        highscore = number  * scoreCalculation();

        Debug.Log("Highscore = " + highscore);
        Debug.Log("set highscore....");
        
        if (!PlayerPrefs.HasKey("MiniGame_1_HighScore"))
        {
            Debug.Log("save new higscore...");
            PlayerPrefs.SetInt("MiniGame_1_HighScore", highscore);
            Highscores1.AddNewHighscore(PlayerPrefs.GetString("Name"), highscore);
        }
            
        if (PlayerPrefs.GetInt("MiniGame_1_HighScore") < highscore)
        {
            PlayerPrefs.SetInt("MiniGame_1_HighScore", highscore);
            Highscores1.AddNewHighscore(PlayerPrefs.GetString("Name"), highscore);
        }
        Debug.Log("set highscore = " + PlayerPrefs.GetInt("MiniGame_1_HighScore"));
        HighscoresA.addAllHighscore();
    }

    public void ResetButton()
    {
        PlayerPrefs.DeleteKey("MiniGame_1_HighScore");
       highScore.text = "0";
    }

    public int scoreCalculation()
    {
        float timescore;
        timescore = lm.currentTime;
        Debug.Log("timescore : " + (int)timescore);
        return (int)timescore;
    }
}
