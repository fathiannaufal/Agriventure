using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScipt : MonoBehaviour
{
    public GameObject KalahUI;
    public GameObject gameUI;

    int a = 1;

    public void Landed()
    {
        GameplayController.instance.SpawnNewBuku();
        GameplayController.instance.MoveCamera();
    }

    public void OnCollisionEnter2D(Collision2D target)
    {
        if (a == 1)
        {
            if (target.gameObject.tag == "Buku")
            {
                gameObject.tag = "GameOver";
            }
            a = 0;
        }
        else if (a == 0)
        {
            if (target.gameObject.tag == "Buku")
            {
                KalahUI.SetActive(true);
                gameUI.SetActive(false);
                Debug.Log("kamu kalah");
            }
        }
    }
}
