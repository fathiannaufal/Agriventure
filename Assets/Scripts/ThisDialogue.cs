using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisDialogue : MonoBehaviour
{
    public GameObject mark;
    public DialogueClass dialogueClass;
    public DialogBox dialogBox;
    public Sprite sprite;

    public void triggerDialog()
    {
        dialogBox.startDialogue(dialogueClass,sprite);
    }
}
