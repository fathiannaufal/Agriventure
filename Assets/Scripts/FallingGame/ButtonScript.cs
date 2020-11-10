using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("http:instagram.com/mpkmbipb");
        Debug.Log("menuju situs...");
    }

    public void PlayGame ()
    {
        if (PlayerPrefs.HasKey("LastPlace"))
            SaveGame.instance.loadLastPlace();
        else
            SceneManager.LoadScene("IntroScene");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu(1)");
    }

}
