using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    private Queue<string> qsentences;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject dialogPanel;
    public GameObject charaSprite;

    private void Start()
    {
        qsentences = new Queue<string>();
        textDisplay.text = "";
    }

    public void startDialogue(DialogueClass dialogueClass,Sprite sprite)
    {
        if(sprite != null)
        {
            charaSprite.SetActive(true);
        }
        else
        {
            charaSprite.SetActive(false);
        }
        charaSprite.GetComponent<Image>().sprite = sprite;
        qsentences.Clear();
        dialogPanel.SetActive(true);
        foreach (string sentence in dialogueClass.sentnces)
        {
            qsentences.Enqueue(sentence);
        }
        nextSentences();
    }

    IEnumerator type(string sentence)
    {
        textDisplay.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    public void nextSentences()
    {
        continueButton.SetActive(true);
        if (qsentences.Count == 0)
        {
            endDialogue();
            return;
        }
        string sentence = qsentences.Dequeue();
        Debug.Log(sentence);
        StopAllCoroutines();
        StartCoroutine(type(sentence));
    }

    private void endDialogue()
    {
        dialogPanel.SetActive(false);
        Debug.Log("End conversation");
    }
}
