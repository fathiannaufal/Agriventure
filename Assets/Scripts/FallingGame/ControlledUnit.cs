using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControlledUnit : MonoBehaviour
{
    // import game over class
    private GameOver gameOver;
    private Spawner spawner;

    public Sprite[] sprites; // buat nyimpen sprite yg bisa berubah
    public Transform floatingTextTransform;
    public int score = 0;
    public bool isHit = false;
    public int addPoint = 1;
    private SpriteRenderer spriteRenderer;
    private int index = 0; // buat ngatur index dari sprites
    public TextMeshProUGUI scoreCounter;
    public TextMeshProUGUI timeCounter;
    public GameObject imageHint;
    public float time = 121f; // batas waktu

    public AudioClip correctSound;
    public AudioClip wrongSound;

    void Start(){
        gameOver = GameObject.FindObjectOfType<GameOver>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spawner = GameObject.FindObjectOfType<Spawner>();
        spriteRenderer.sprite = sprites[index];
    }

    void Update(){
        time -= Time.deltaTime;
        // display score
        scoreCounter.text = score.ToString();
        // display time in sec
        timeCounter.text = Mathf.FloorToInt(time).ToString();

        //check if time is 0
        if(Mathf.FloorToInt(time) == 0){
            score *= 62;
            gameOver.GameIsOver();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        string status;
        

        if((other.tag == "tipe1" && index == 0) || (other.tag == "tipe2" && index == 1) || (other.tag == "tipe3" && index == 2)){
            score += addPoint;
            status = "+" + addPoint;
            addPoint++;
            showFloatingText(status, correctSound);
        }
        else{
            Debug.Log("Salah ngambil gan!");
            status = "Kamu salah ambil";
            addPoint = 1;
            showFloatingText(status, wrongSound);
        }
        //showFloatingText(status,correctSound);
        isHit = true;
    }

    void OnTriggerExit2D(Collider2D other){
        isHit = false;
    }

    void showFloatingText(string status, AudioClip sound){
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 0.8f;
        var a = Instantiate(floatingTextTransform, spawnPosition, Quaternion.identity);
        a.GetComponent<TextMeshPro>().SetText(status);
        a.GetComponent<AudioSource>().clip = sound;
        a.GetComponent<AudioSource>().Play();
    }

    public void changeUnit(){
        if(Time.timeScale == 1f){
            Debug.Log("Unit Changed!");
            index++;
            if(index == 3) index = 0;
            spriteRenderer.sprite = sprites[index];
            imageHint.GetComponent<Image>().sprite = sprites[(index+1 == 3) ? 0 : index+1];
        }
    }

    public void ResetGame(){
        score = 0;
        time = 121f;
        scoreCounter.text = score.ToString();
        timeCounter.text = time.ToString();
        gameObject.SetActive(true);
    }
}
