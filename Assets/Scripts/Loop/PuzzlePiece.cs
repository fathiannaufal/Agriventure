using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{

    public int[] exitValues;
    /* INDEX
         0 = Top
         1 = Right
         2 = Bottom
         3 = Left
    */
    private float realRotation = 0f;
    public float speed;
    public AudioSource audioSource;

    public GameManager gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    { 
        if (transform.rotation.eulerAngles.z != realRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, realRotation), speed * Time.deltaTime);
        }
    }

    void OnMouseDown()
    {
        int difference = -gm.QuickSweep((int)transform.position.x, (int)transform.position.y);

        RotatePiece();
        audioSource.Play();

        difference += gm.QuickSweep((int)transform.position.x, (int)transform.position.y);

        gm.puzzle.curValue += difference;

        if (gm.puzzle.curValue == gm.puzzle.winValue)
            gm.Win();
    }

    public void RotatePiece()
    {   
        realRotation -= 90;

        if (realRotation == -360)
            realRotation = 0;

        RotateExitValues();
    }

    public void RotateExitValues()
    {
        int aux = exitValues[3];

        for (int i = exitValues.Length - 1; i > 0 ; i--)
        {
            exitValues[i] = exitValues[i - 1];
        }
        exitValues[0] = aux;
    }

}
