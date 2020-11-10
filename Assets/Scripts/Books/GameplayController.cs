using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    public BukuSpawner bukuSpawner;

    public Touch touch;
    public Vector3 touch_pos;

    public int skor;
    public int highskor;
    public Text skorT;
    public Text skor_habis;
    public Text skor_kalah;
    public Text highScoreH;
    public Text highScoreK;

    [HideInInspector]
    public BukuScripts currBuku;

    public CameraFollowMiniGame camScript;

    public int movCount;

    void Awake()
    {
        if (instance == null) instance = this;    
    }
    void Start()
    {
        bukuSpawner.SpawnBox();
        camScript.targetPos.y = 0f;
    }

    public void DetectInput()
    {
        currBuku.DropBox();
    }

    public void SpawnNewBuku()
    {
        bukuSpawner.SpawnBox();
    }

    public void MoveCamera()
    {
        movCount++;
        if(movCount == 1)
        {
            camScript.targetPos.y += 0.5f;
            
        }
        else if(movCount % 4 == 0)
        {
            camScript.targetPos.y += 1.25f;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
