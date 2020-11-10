using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingObject : MonoBehaviour
{
    private Spawner spawner;
    private ControlledUnit controlledUnit;
    private GameOver gameOver;

    public float speed = 3.0f;
    private Rigidbody2D rb;
    private Vector2 screenBound;
    public Sprite[] sprites; // buat nyimpen sprite yg bisa berubah
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start(){
        spawner = GameObject.FindObjectOfType<Spawner>();
        controlledUnit = GameObject.FindObjectOfType<ControlledUnit>();
        gameOver = GameObject.FindObjectOfType<GameOver>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -speed);
        screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update(){
        if(transform.position.y < -screenBound.y - 1){
            Destroy(this.gameObject);
            controlledUnit.addPoint = 1;
            spawner.objectMiss();
        }
        else if (controlledUnit.isHit){
            Destroy(this.gameObject);
        }

        if(gameOver.isGameOver){
            Destroy(this.gameObject);
        }
    }
}
