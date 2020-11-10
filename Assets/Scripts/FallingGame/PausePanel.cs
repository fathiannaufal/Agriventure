using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private GameOver gameOver;
    private ControlledUnit controlledUnit;

    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    void Awake(){
        gameOver = GameObject.FindObjectOfType<GameOver>();
        controlledUnit = GameObject.FindObjectOfType<ControlledUnit>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                Resume();
            }
            else {
                Pause();
            }
        }

        if(gameOver.isGameOver){
            gameObject.SetActive(false);
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Restart(){
        Time.timeScale = 0f;
        controlledUnit.score = 0;
        controlledUnit.time = 120f;
        gameOver.isGameOver = true;
        gameObject.SetActive(false);
        gameOver.OpeningStart();
    }
}
