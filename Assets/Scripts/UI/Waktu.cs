using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waktu : MonoBehaviour
{
    float curTime = 0f;
    public float startTime = 61f;

    public GameObject mulaiUI;
    public GameObject otherUI1;
    public GameObject waktuHabisUI;
    public GameObject infoUI;
    public Text countdownText;

    public static bool countUI;

    void Start()
    {
        curTime = startTime;
    }

    void Update()
    {
        curTime -= 1 * Time.deltaTime;
        countdownText.text = curTime.ToString("0");
 
        if (curTime <= 0f)
        {
            curTime = 0f;
            startTime = 61f;
            GameSelesai();
        }
    }
    public void MulaiGame()
    {
        mulaiUI.SetActive(true);
        otherUI1.SetActive(true);
        waktuHabisUI.SetActive(false);
    }

    public void GameSelesai()
    {
        mulaiUI.SetActive(false);
        otherUI1.SetActive(false);
        waktuHabisUI.SetActive(true);
    }

}
