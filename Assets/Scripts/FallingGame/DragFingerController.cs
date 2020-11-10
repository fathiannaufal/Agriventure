using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFingerController : MonoBehaviour {
    private ControlledUnit controlledUnit;

    private Vector3 touchPosition;
    private Rigidbody2D rb;
    private Vector3 direction;
    private float moveSpeed = 10f;
    private float time;
    // private bool isChanged = false;
    
    // Start is called before the first frame update
    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
        controlledUnit = GameObject.FindObjectOfType<ControlledUnit>();
    }

    // Update is called once per frame
    void Update(){
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);

            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            switch(touch.phase){
                case TouchPhase.Began:
                    time = 0;
                    break;
                case TouchPhase.Moved:
                    time += Time.deltaTime;
                    rb.velocity = new Vector2(direction.x, 0) * moveSpeed;
                    Debug.Log(time);
                    break;
                case TouchPhase.Ended:
                    if(time < 0.2) controlledUnit.changeUnit();
                    rb.velocity = Vector2.zero;
                    break;
            }
            Debug.Log(time);
        }
    }
}
