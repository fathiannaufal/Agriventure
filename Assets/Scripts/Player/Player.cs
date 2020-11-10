using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public CircleCollider2D coll;
    public GameObject triggerObject;
    public Button interactButton;
    public Button interact;
    public bool trigger;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger == true && Input.GetKeyDown(KeyCode.Space))
        {
            triggerObject.GetComponent<ThisDialogue>().triggerDialog();
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            trigger = true;
            triggerObject = collision.gameObject;
            triggerObject.GetComponent<ThisDialogue>().mark.SetActive(true);
            interactButton.onClick.AddListener(triggerObject.GetComponent<ThisDialogue>().triggerDialog);
        }
        if (collision.CompareTag("Outro"))
        {
            trigger = true;
            triggerObject = collision.gameObject;
            triggerObject.GetComponent<ThisDialogue>().mark.SetActive(true);
            interactButton.onClick.AddListener(epilogue);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            trigger = false;
            interactButton.onClick.RemoveAllListeners();
            triggerObject.GetComponent<ThisDialogue>().mark.SetActive(false);
            triggerObject = null;
        }
    }

    void epilogue()
    {
        SceneMenegement.instance.crossFade("OutroScene", false);
    }
}
