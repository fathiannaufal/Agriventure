using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour
{
    public void nextScene()
    {
        Debug.Log("next scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
