using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToGameScript : MonoBehaviour
{
    public GameObject continueButton;
    private void Start()
    {
        continueButton.SetActive(false);
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("Name"))
        {
            continueButton.SetActive(true);
        }
    }
}
