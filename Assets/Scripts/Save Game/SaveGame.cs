using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoSingleton<SaveGame>
{
    private static bool isCreatedSave = false;
    private void Awake()
    {
        if (!isCreatedSave)
        {
            DontDestroyOnLoad(this.gameObject);
            isCreatedSave = true;

        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey("Name"))
            Debug.Log("your name is " + PlayerPrefs.GetString("Name"));

    }
    public void saveName(string name)
    {
        PlayerPrefs.SetString("Name",name);
        Debug.Log("your name is "+ PlayerPrefs.GetString("Name"));
        PlayerPrefs.Save();
    }
    
    public void saveLastPlace(string lastplace)
    {
        PlayerPrefs.SetString("LastPlace",lastplace);
        Debug.Log("Last Place = "+ PlayerPrefs.GetString("LastPlace"));
        PlayerPrefs.Save();
    }

    public void loadLastPlace()
    {
        if (PlayerPrefs.HasKey("LastPlace"))
            SceneManager.LoadScene(PlayerPrefs.GetString("LastPlace"));
    }
    public void deleteName()
    {
        PlayerPrefs.DeleteKey("Name");
    }

    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
    }

}
