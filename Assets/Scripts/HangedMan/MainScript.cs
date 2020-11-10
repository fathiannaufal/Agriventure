using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using SimpleJSON;

public class MainScript : MonoBehaviour {

    public int SCORE = 0;
    public float maxTime = 120f;
    public float TIMER;
    private bool timerIsRunning = false;
    public int highscore = 0;
    public Text timerUI;
    public Text questionUI;
    [Space]
    public Text QuestionText, DashesText, ResultsText, BestScoreText, ScoreText;
    public Image HangmanImage, FinalImage;
    public Sprite[] HangmanSprites;
    public Sprite WinSprite, LoseSprite;

    public GameObject MainDialogue, FinalDialogue, IntroDialogue;
    public List<Button> usedButtons;

    private int currentHangmanSprite = 1;
    private const int TOTAL_HANGMAN_SPRITES = 3;
    private const char PLACEHOLDER = '*';
    private const char SPACEBAR = ' ';

    private Dictionary<string, string> gameDict;
    private string answer, userInput;

    void Start () {
        gameDict = new Dictionary<string, string>();
        LoadDictionary("MpkmbDictionary", gameDict); // file should be without ext - thanks unity !
        PickRandomQuestion();
        highscore = 0;
        
        if (PlayerPrefs.HasKey("MiniGame_5_HighScore"))
        {
            Debug.Log("WATDEFUK");
            BestScoreText.text = "Highscore : " + PlayerPrefs.GetInt("MiniGame_5_HighScore").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("MiniGame_5_HighScore", 0);
            PlayerPrefs.Save();
        }
       
        TIMER = maxTime;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (TIMER > 0)
            {
                TIMER -= Time.deltaTime;
                DisplayTime(TIMER);
            }
            else
            {
                ShowFinalDialogue(false);
                TIMER = 0;
                timerIsRunning = false;
            }
        }
    }
	
    public void OnRestartClicked() {
        MainDialogue.SetActive(true);
        FinalDialogue.SetActive(false);
        currentHangmanSprite = 0;
        HangmanImage.sprite = HangmanSprites[currentHangmanSprite];

        PickRandomQuestion();

        if (SCORE <= 1 || SCORE > 5)
        {
            TIMER = maxTime; 
            timerIsRunning = false;
            IntroDialogue.SetActive(true);
            BestScoreText.text = "Highscore : " + PlayerPrefs.GetInt("MiniGame_5_HighScore").ToString();
            highscore = 0;
        }

        for (int i = 0; i < usedButtons.Count; i++){ usedButtons[i].image.color = Color.white; usedButtons[i].interactable = true; }
    }

    public void OnPlayClicked()
    {
        timerIsRunning = true;
        IntroDialogue.SetActive(false);
        SCORE = 1;
        questionUI.text = "1 / 5";
    }

    public void OnGuessSubmitted(Button buttonIn) {
        char letter = buttonIn.GetComponentInChildren<Text>().text.ToCharArray()[0];
        usedButtons.Add(buttonIn);
        buttonIn.image.color = Color.black;
        buttonIn.interactable = false;

        if ( answer.Contains(letter) ) {
            UpdateAnswerText(letter);
            if ( CheckWinCondition() && SCORE == 5) {
                //Debug.Log("You won the game !");
                timerIsRunning = false;
                SCORE++;
                ShowFinalDialogue(true);

                highscore = (int)TIMER * 10 * 6;
                ScoreText.text = "Score : " + highscore.ToString();
                if (PlayerPrefs.GetInt("MiniGame_5_HighScore", 0) < highscore)
                {
                    PlayerPrefs.SetInt("MiniGame_5_HighScore", highscore);
                    Highscores5.AddNewHighscore(PlayerPrefs.GetString("Name"),highscore);
                    HighscoresA.addAllHighscore();
                }
                        
                PlayerPrefs.Save();

            }
            else if(CheckWinCondition())
            {
                SCORE++;
                questionUI.text = SCORE.ToString() + " / 5";
                OnRestartClicked();
            }
        }
        else
        {
            if (CheckLoseCondition()) {
                Debug.Log("You lost the game");
                SCORE = 1;
                ShowFinalDialogue(false);
            }
            else { DrawNextHangmanPart(); }
        }

    }

    private void PickRandomQuestion()
    {
        int randInt = Random.Range(0, gameDict.Count);
        QuestionText.text = gameDict.ElementAt(randInt).Key;
        answer = gameDict.ElementAt(randInt).Value.ToUpper();
        StringBuilder sb = new StringBuilder("");

        for (int i = 0; i < answer.Length; i++)
        {
            if (answer[i] == SPACEBAR)
            {
                sb.Append(SPACEBAR);
            }
            else { sb.Append(PLACEHOLDER); }
        }

        DashesText.text = sb.ToString();
        userInput = sb.ToString();
        Debug.Log("Answer: " + answer);
    }

    private void LoadDictionary(string dictFileName, Dictionary<string, string> outputDict)
    {
        TextAsset ta = Resources.Load(dictFileName) as TextAsset; //Resources.Load(dictFileName) as TextAsset;
        JSONObject jsonObj = (JSONObject)JSON.Parse(ta.text);
        foreach (var key in jsonObj.GetKeys()) { outputDict[key] = jsonObj[key]; }
    }

    private void UpdateAnswerText(char letter) {
        char[] userInputArray = userInput.ToCharArray();
        for (int i = 0; i < answer.Length; i++) {
            if (userInputArray[i] != PLACEHOLDER) { continue; } // already guessed
            if (answer[i] == letter) { userInputArray[i] = letter; }
        }
        userInput = new string(userInputArray);
        DashesText.text = userInput;
    }

    private void DrawNextHangmanPart() {
        currentHangmanSprite = ++currentHangmanSprite % TOTAL_HANGMAN_SPRITES;
        HangmanImage.sprite = HangmanSprites[currentHangmanSprite];
    }

    private bool CheckWinCondition() { return answer.Equals(userInput); }
    private bool CheckLoseCondition() { return currentHangmanSprite == TOTAL_HANGMAN_SPRITES-1; }

    private void ShowFinalDialogue(bool win) {
        MainDialogue.SetActive(false);
        FinalDialogue.SetActive(true);
        FinalImage.sprite = win ? WinSprite : LoseSprite;
        ResultsText.text = win ? "Victory !" : "Defeat !!!";
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Quit()
    {
        SceneManager.LoadScene("CCR");
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
