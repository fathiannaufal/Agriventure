using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BukuScripts : MonoBehaviour
{
    private float minX = -1.4f, maxX = 1.4f;
    private float movSpd = 2f;

    private Rigidbody2D rb;

    private bool canMove;
    private bool ignColl;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Start()
    {
        canMove = true;
        if (Random.Range(0, 2) > 0)
        {
            movSpd *= -1f;
        }
        GameplayController.instance.currBuku = this;
    }

    private void FixedUpdate()
    {
        MoveBox();
    }

    void MoveBox()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += movSpd * Time.deltaTime;

            if (temp.x > maxX || temp.x < minX)
            {
                movSpd *= -1f;
            }
            transform.position = temp;
        }
    }

    public void DropBox()
    {
        canMove = false;
        rb.gravityScale = Random.Range(2, 4);
    }

    public void Landed()
    {
        ignColl = true;
        GameplayController.instance.MoveCamera();
        GameplayController.instance.SpawnNewBuku();
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (ignColl) return;
        if (target.gameObject.tag == "Platform" || target.gameObject.tag == "Buku")
        {
            Invoke("Landed", 0.5f);
            ignColl = true;
            Debug.Log("next book");
            GetComponent<BukuScripts>().enabled = false;
        }
    }
}
