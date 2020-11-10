using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Puzzle
    {
        public int winValue;
        public int curValue;

        public int width;
        public int height;
        public PuzzlePiece[,] pieces;
    }

    public int SCORE = 0, highscore = 0;
    public float TIMER = 0f;
    private bool timerIsRunning = false;
    public Puzzle puzzle;
    public GameObject cam;
    public GameObject debugWin;
    public GameObject[] levels;
    public Text ScoreText, BestScoreText, timerUI, bestScoreText;

    void Start()
    {
        timerIsRunning = false;

        levels[SCORE].SetActive(true);
        LevelSetup();

        if(!PlayerPrefs.HasKey("MiniGame_2_HighScore"))
        {
            PlayerPrefs.SetInt("MiniGame_2_HighScore", 0);
            PlayerPrefs.Save();
            Debug.Log("MiniGame_2_Highscore : "+PlayerPrefs.GetInt("MiniGame_2_HighScore"));
        }
        bestScoreText.text = "Highscore : " + PlayerPrefs.GetInt("MiniGame_2_HighScore",0).ToString();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            TIMER += Time.deltaTime;
            DisplayTime(TIMER);
        }
    }

    public void LevelSetup()
    {
        Vector2 dimensions = CheckDimensions();

        cam = Camera.main.gameObject;

        puzzle.width = (int)dimensions.x;
        puzzle.height = (int)dimensions.y;

        puzzle.pieces = new PuzzlePiece[puzzle.width, puzzle.height];

        foreach (var piece in GameObject.FindGameObjectsWithTag("Piece"))
        {
            puzzle.pieces[(int)piece.transform.position.x, (int)piece.transform.position.y] = piece.GetComponent<PuzzlePiece>();
        }

        //Set the camera to center
        cam.transform.position = puzzle.pieces[puzzle.height / 2, puzzle.width / 2].gameObject.transform.position + (Vector3.forward * -10);

        puzzle.winValue = GetWinValue();

        ShufflePuzzle(); //Randomize the puzzle

        puzzle.curValue = Sweep(); //Check the puzzle if it's done
    }

    public int Sweep()
    {
        int value = 0;

        for (int h = 0; h < puzzle.height; h++)
        {
            for (int w = 0; w < puzzle.width; w++)
            {


                //compares top
                if (h != puzzle.height - 1)
                    if (puzzle.pieces[w, h].exitValues[0] == 1 && puzzle.pieces[w, h + 1].exitValues[2] == 1)
                        value++;


                //compare right
                if (w != puzzle.width - 1)
                    if (puzzle.pieces[w, h].exitValues[1] == 1 && puzzle.pieces[w + 1, h].exitValues[3] == 1)
                        value++;


            }
        }

        return value;
    }

    public void Win()
    {
        SCORE++;
        if (SCORE >= 5)
        {
            debugWin.SetActive(true);
            timerIsRunning = false;

            if (TIMER > 5000) TIMER = 5000;

            highscore = 5000 - (int)TIMER;

            ScoreText.text = "Score : " + highscore.ToString();

            if (PlayerPrefs.GetInt("MiniGame_2_HighScore") < highscore)
            {
                PlayerPrefs.SetInt("MiniGame_2_HighScore", highscore);
                Highscores2.AddNewHighscore(PlayerPrefs.GetString("Name"),highscore);
                PlayerPrefs.Save();
                Debug.Log("MiniGame_2_HighScore : " + PlayerPrefs.GetInt("MiniGame_2_HighScore"));
                BestScoreText.text = "Highscore : " + highscore.ToString();
            }
            HighscoresA.addAllHighscore();
        }
        else
        {
            levels[SCORE - 1].SetActive(false);
            levels[SCORE].SetActive(true);
            LevelSetup();
        }
    }

    public int QuickSweep(int w, int h)
    {
        int value = 0;

        //compares top
        if (h != puzzle.height - 1)
            if (puzzle.pieces[w, h].exitValues[0] == 1 && puzzle.pieces[w, h + 1].exitValues[2] == 1)
                value++;


        //compare right
        if (w != puzzle.width - 1)
            if (puzzle.pieces[w, h].exitValues[1] == 1 && puzzle.pieces[w + 1, h].exitValues[3] == 1)
                value++;


        //compare left
        if (w != 0)
            if (puzzle.pieces[w, h].exitValues[3] == 1 && puzzle.pieces[w - 1, h].exitValues[1] == 1)
                value++;

        //compare bottom
        if (h != 0)
            if (puzzle.pieces[w, h].exitValues[2] == 1 && puzzle.pieces[w, h - 1].exitValues[0] == 1)
                value++;


        return value;

    }

    int GetWinValue()
    {

        int winValue = 0;
        foreach (var piece in puzzle.pieces)
        { 
            foreach (var j in piece.exitValues)
            {
                winValue += j;
            }
        }

        winValue /= 2;

        return winValue;



    }

    void ShufflePuzzle()
    {
        foreach (var piece in puzzle.pieces)
        {
            int k = Random.Range(0, 4);

            for (int i = 0; i < k; i++)
            {
                piece.RotatePiece();
            }
        }
    }

    Vector2 CheckDimensions()
    {
        Vector2 aux = Vector2.zero;

        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");

        foreach (var p in pieces)
        {
            if (p.transform.position.x > aux.x)
                aux.x = p.transform.position.x;

            if (p.transform.position.y > aux.y)
                aux.y = p.transform.position.y;
        }

        aux.x++;
        aux.y++;

        return aux;
    }

    public void OnRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void quit()
    {
        SceneManager.LoadScene("AHN");
    }

    public void start()
    {
        timerIsRunning = true;
    }

    public void pause()
    {
        timerIsRunning = false;
    } 

    public void resume()
    {
        timerIsRunning = true;
    }
}
