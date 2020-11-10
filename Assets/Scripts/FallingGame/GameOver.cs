using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private ControlledUnit controlledUnit;

    public GameObject gameOverUI;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI finalHighScore;
    public GameObject pauseButton;
    public GameObject[] openingUI;
    public bool isGameOver = true;

    void Awake(){
        controlledUnit = GameObject.FindObjectOfType<ControlledUnit>();
        Time.timeScale = 0f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isGameOver){
                Quit();
            }
        }
    }

    public void GameIsOver(){
        isGameOver = true;

        // set High Score
        int highScore = PlayerPrefs.GetInt("MiniGame_4_HighScore", 0);
        if(highScore < controlledUnit.score){
            PlayerPrefs.SetInt("MiniGame_4_HighScore", controlledUnit.score);
            Highscores4.AddNewHighscore(PlayerPrefs.GetString("Name"), PlayerPrefs.GetInt("MiniGame_4_HighScore"));
            highScore = controlledUnit.score;
            HighscoresA.addAllHighscore();
        }

        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        finalHighScore.text = "your high score : " + highScore;
        finalScore.text = controlledUnit.score.ToString() ;
    }

    // keluar aplikasi
    public void Quit(){
        Debug.Log("Quitting... ");
        //Application.Quit();
        Time.timeScale = 1f;
        SceneManager.LoadScene("GCM");
    }

    //buat kembali ke main menu
    public void MainMenu(){
        Time.timeScale = 0f;
        isGameOver = true;
        openingUI[0].SetActive(true);
        controlledUnit.ResetGame();
    }

    public void OpeningStart(){
        openingUI[0].SetActive(false);
        openingUI[1].SetActive(true);

        controlledUnit.ResetGame();
    }

    public void Dismiss(){
        Instantiate(openingUI[2]);
        isGameOver = false;
        Time.timeScale = 1f;
    }
}
