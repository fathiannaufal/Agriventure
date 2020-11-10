using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public Waktu waktuScript;
    public GameObject KalahUI;
    public GameObject gameUI;

    public void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Buku")
        {
            KalahUI.SetActive(true);
            gameUI.SetActive(false);
            Debug.Log("kalah");
        }
    }
}
