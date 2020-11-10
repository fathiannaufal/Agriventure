using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] private Text timerText;
    [SerializeField] private GameObject hiddenObjectIconHolder;
    [SerializeField] private GameObject hiddenObjectIconPrefab;
    [SerializeField] private GameObject gameWin;
    [SerializeField] private GameObject gameLost;

    private List<GameObject> hiddenObjectIconList;

    public GameObject GameWin { get { return gameWin; } }
    public Text TimerText { get { return timerText; } }
    public GameObject GameLost { get { return gameLost; } }

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(gameObject);

        hiddenObjectIconList = new List<GameObject>();
    }
   
    public void PopulateHiddenObjectIcon(List<HiddenObjectData> activeHiddenObjectList)
    {
        hiddenObjectIconList.Clear();

        for (int i = 0; i < activeHiddenObjectList.Count; i++)
        {
            GameObject icon = Instantiate(hiddenObjectIconPrefab, hiddenObjectIconHolder.transform);
            icon.name = activeHiddenObjectList[i].hiddenObject.name;
            Image childImg = icon.transform.GetChild(0).GetComponent<Image>();
            Text childText = icon.transform.GetChild(1).GetComponent<Text>();

            childImg.sprite = activeHiddenObjectList[i].hiddenObject.GetComponent<SpriteRenderer>().sprite;
            childText.text = activeHiddenObjectList[i].name;

            hiddenObjectIconList.Add(icon);
        }
    }

    public void CheckSelectedHiddenObject(string objectName)
    {
        for(int i = 0; i < hiddenObjectIconList.Count; i++)
        {
            if (hiddenObjectIconList[i].name == objectName)
            {
                hiddenObjectIconList[i].SetActive(false);
                break;
            }
            
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
