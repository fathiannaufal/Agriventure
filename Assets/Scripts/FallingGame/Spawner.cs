using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour{
    private GameOver gameOver;

    public GameObject[] itemPrefabs;
    private Vector2 screenBound;
    private float respawnTime = 5f;
    private float time;
    private bool isMiss = false;

    void Start(){
        screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(itemWave());
        gameOver = GameObject.FindObjectOfType<GameOver>();
        time = 0;
    }

    void Update(){
        // Debug.Log("time : " + time);
        // Debug.Log("deltaTime : " + Time.deltaTime);
        // Debug.Log("RespawnTime : " + respawnTime);
        time += Time.deltaTime;
        if(time > 10 && !isMiss){
            if(respawnTime > 1f){
                respawnTime -= 1f;
            }
            time %= 10;
        }
        else if(isMiss || gameOver.isGameOver){
            respawnTime = 5f;
            time = 0;
            isMiss = false;
        }
    }

    // spawn yang jatoh2an
    private void spawnObject(){
        screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        GameObject a = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)]) as GameObject;
        a.transform.position = new Vector2(Random.Range(-screenBound.x, screenBound.x), screenBound.y + 1);
    }

    // ngatus wave dari item yang jatoh
    IEnumerator itemWave(){
        while(true){
            yield return new WaitForSeconds(respawnTime);
            spawnObject();
        }
    }

    public void objectMiss(){
        isMiss = true;
    }
}
