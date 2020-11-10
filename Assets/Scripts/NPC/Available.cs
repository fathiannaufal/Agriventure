using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Available : MonoBehaviour
{
    public GameObject npc;
    private void Start()
    {
        if (PlayerPrefs.HasKey("MiniGame_1_HighScore"))
        {
            Debug.Log("Minigame 1 : " + PlayerPrefs.GetInt("MiniGame_1_HighScore"));
        }
        if (PlayerPrefs.HasKey("MiniGame_2_HighScore"))
        {
            Debug.Log("Minigame 2 : " + PlayerPrefs.GetInt("MiniGame_2_HighScore"));
        }
        if (PlayerPrefs.HasKey("MiniGame_3_HighScore"))
        {
            Debug.Log("Minigame 3 : " + PlayerPrefs.GetInt("MiniGame_3_HighScore"));
        }
        if (PlayerPrefs.HasKey("MiniGame_4_HighScore"))
        {
            Debug.Log("Minigame 4 : " + PlayerPrefs.GetInt("MiniGame_4_HighScore"));
        }
        if (PlayerPrefs.HasKey("MiniGame_5_HighScore"))
        {
            Debug.Log("Minigame 5 : " + PlayerPrefs.GetInt("MiniGame_5_HighScore"));
        }
        //npc = GetComponent<GameObject>();
    }
    void Update()
    {
        if (PlayerPrefs.HasKey("MiniGame_1_HighScore") &&
            PlayerPrefs.HasKey("MiniGame_2_HighScore") &&
            PlayerPrefs.HasKey("MiniGame_3_HighScore") &&
            PlayerPrefs.HasKey("MiniGame_4_HighScore") &&
            PlayerPrefs.HasKey("MiniGame_5_HighScore"))
        {
            npc.SetActive(true);
        }
        else
            npc.SetActive(false);
    }
}
