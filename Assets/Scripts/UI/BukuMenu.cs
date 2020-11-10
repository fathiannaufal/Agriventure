using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BukuMenu : MonoBehaviour
{
    private float minX = -1.5f, maxX = 1.5f;
    private float movSpd = 2f;

    private Rigidbody2D rb;

    private bool canMove;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Start()
    {
        canMove = true;
        if(Random.Range(0, 2) >= 0)
        {
            movSpd *= -1f;
        }
    }

    void Update()
    {
        MoveBuku();
    }

    void MoveBuku()
    {
        if(canMove)
        {
            Vector3 temp = transform.position;
            temp.x += movSpd * Time.deltaTime;
            if (temp.x >= maxX) movSpd *= -1f;
            if (temp.x <= minX) movSpd *= -1f;
            transform.position = temp;
        }
    }
}
