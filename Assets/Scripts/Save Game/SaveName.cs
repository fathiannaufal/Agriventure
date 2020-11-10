using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveName : MonoBehaviour
{
    public InputField inputField;
    
    public void newName()
    {
        PlayerPrefs.DeleteAll();
        SaveGame.instance.saveName(inputField.text);
        
    }
}
