using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class StorySceneManager : MonoBehaviour
{
    public Animator BlackFade;
    public bool IS_INTRO = true;
    public bool isInteractable = true;
    public Sprite[] introStory;
    public Sprite[] outroStory;
    public string[] sentences;

    public TextMeshProUGUI tmpro;
    public Image StoryImage;
    public int STORY_INDEX = 0;
    public int StringIndex = 0;

    public Camera camera;

    void Start()
    {
        if (IS_INTRO)
        {
            StoryImage.sprite = introStory[STORY_INDEX];
        }else
        {
            StoryImage.sprite = outroStory[STORY_INDEX];
        }
        tmpro.text = sentences[StringIndex];
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //Gantigambar
                isInteractable = false;
                BlackFade.SetTrigger("nextStory");
            }
        }

        if(Input.GetMouseButtonDown(0) && isInteractable)
        {
            //Gantigambar
            isInteractable = false;
            BlackFade.SetTrigger("nextStory");
        }

    }

    public void NextStory()
    {
        if(IS_INTRO)
        {
            if(STORY_INDEX < introStory.Length-1)
            {
                StringIndex++;
                tmpro.text = sentences[StringIndex];
                STORY_INDEX++;
                StoryImage.sprite = introStory[STORY_INDEX];
            }else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
        }else
        {
            if (STORY_INDEX < outroStory.Length-1)
            {
                StringIndex++;
                tmpro.text = sentences[StringIndex];
                STORY_INDEX++;
                StoryImage.sprite = outroStory[STORY_INDEX];
                if(STORY_INDEX == outroStory.Length - 1)
                {
                    camera.backgroundColor = new Color32(255,255,255,255);
                }
            }
            else
            {
                SceneManager.LoadScene("Menu(1)");
            }
        }
    }

    public void ChangeInteractState() { isInteractable = true; }

}
