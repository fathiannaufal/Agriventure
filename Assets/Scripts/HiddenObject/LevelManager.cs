using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public HealthBar healthBar;
    public Transform floatingTextTransform;
    public AudioClip audioClip;

    [SerializeField] public float timeLimit;

    [SerializeField]
    public HiddenObjectHolder hiddenObjectHolder;

    public List<HiddenObjectData> activeHiddenObjectsList;

    [SerializeField]
    public int maxHealth = 5;
    public int currentHealth;
    public int maxActiveHiddenObjectsCount = 7;
    public int totalHiddenObjectsFound = 0;

    public float currentTime = 0;
    public GameStatus gameStatus = GameStatus.NEXT;

    public void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(gameObject);
    }

    public void Start()
    {
        activeHiddenObjectsList = new List<HiddenObjectData>();
        AssignHiddenObjects();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void AssignHiddenObjects()
    {
        currentTime = timeLimit;
        UIManager.instance.TimerText.text = "" + currentTime;
        totalHiddenObjectsFound = 0;
        activeHiddenObjectsList.Clear();
        for (int i = 0; i < hiddenObjectHolder.HiddenObjectsList.Count; i++)
        {
            hiddenObjectHolder.HiddenObjectsList[i].hiddenObject.GetComponent<Collider2D>().enabled = false;
        }

        int k = 0;
        while(k < maxActiveHiddenObjectsCount)
        {
            int randomVal = UnityEngine.Random.Range(0, hiddenObjectHolder.HiddenObjectsList.Count);

            if(!hiddenObjectHolder.HiddenObjectsList[randomVal].makeHidden)
            {
                hiddenObjectHolder.HiddenObjectsList[randomVal].hiddenObject.name = "" + k;
                hiddenObjectHolder.HiddenObjectsList[randomVal].makeHidden = true;
                hiddenObjectHolder.HiddenObjectsList[randomVal].hiddenObject.GetComponent<Collider2D>().enabled = true;

                activeHiddenObjectsList.Add(hiddenObjectHolder.HiddenObjectsList[randomVal]);

                k++;
            }
        }

        UIManager.instance.PopulateHiddenObjectIcon(activeHiddenObjectsList);
        gameStatus = GameStatus.PLAYING;
    }

    public void Update()
    {
        if (gameStatus == GameStatus.PLAYING)
        {
            if (Input.GetMouseButtonDown(0))
            {
               
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.zero);

                if (hit && hit.collider != null)
                {
                    Debug.Log("Object Name:" + hit.collider.gameObject.name);

                    hit.collider.gameObject.SetActive(false);
                    UIManager.instance.CheckSelectedHiddenObject(hit.collider.gameObject.name);
                    
                    //audio
                    showFloatingText("Good", audioClip,hit.collider.gameObject.transform.position);
                    
                    for (int i = 0; i < activeHiddenObjectsList.Count; i++)
                    {
                        if (activeHiddenObjectsList[i].hiddenObject.name == hit.collider.gameObject.name)
                        {
                            activeHiddenObjectsList.RemoveAt(i);
                            break;
                        }

                    }

                    totalHiddenObjectsFound++;

                    if (totalHiddenObjectsFound >= maxActiveHiddenObjectsCount)
                    {
                        Debug.Log("Level Complete");
                        UIManager.instance.GameWin.SetActive(true);
                        gameStatus = GameStatus.NEXT;
                    }

                }
                else
                {
                    TakeDamage(1);
                    Debug.Log("Lives Left: " + currentHealth);
                    
                }

            }
            if (currentHealth <= 0)
            {
                Debug.Log("Level Lost");
                UIManager.instance.GameLost.SetActive(true);
                gameStatus = GameStatus.FAIL;
            }

            currentTime -= Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            UIManager.instance.TimerText.text = time.ToString("mm' : 'ss");

            if (totalHiddenObjectsFound < 3 && currentTime <= 0)
            {
                Debug.Log("Level Lost");
                UIManager.instance.GameLost.SetActive(true);
                gameStatus = GameStatus.FAIL;
            }
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    void showFloatingText(string status, AudioClip sound,Vector3 vector3)
    {
        Vector3 spawnPosition = vector3;
        spawnPosition.y += 0.8f;
        var a = Instantiate(floatingTextTransform, spawnPosition, Quaternion.identity);
        a.GetComponent<TextMeshPro>().SetText(status);
        a.GetComponent<AudioSource>().clip = sound;
        a.GetComponent<AudioSource>().Play();
    }

}

[System.Serializable]
public class HiddenObjectData
{
    public string name;
    public GameObject hiddenObject, heart1, heart2, heart3;
    public bool makeHidden = false;
}

public enum GameStatus
{
    PLAYING,
    FAIL,
    NEXT
}