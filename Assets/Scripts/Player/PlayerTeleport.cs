using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public Collider2D col;
    public SceneMenegement sceneMenegement;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("ToGYM"))
        {
            sceneMenegement.crossFade("GYM",true);
            Debug.Log("ke gym");
        }
        if (other.CompareTag("ToCCR"))
        {
            sceneMenegement.crossFade("CCR",true);
            Debug.Log("ke ccr");
        }
        if (other.CompareTag("ToGWW"))
        {
            sceneMenegement.crossFade("GWW",true);
            Debug.Log("ke gww");
        }
        if (other.CompareTag("ToLSI"))
        {
            sceneMenegement.crossFade("LSI",true);
            Debug.Log("ke lsi");
        }
        if (other.CompareTag("ToGCM"))
        {
            sceneMenegement.crossFade("GCM",true);
            Debug.Log("ke gcm");
        }
        if (other.CompareTag("ToAHN"))
        {
            sceneMenegement.crossFade("AHN",true);
            Debug.Log("ke ahn");
        }
        if (other.CompareTag("ToKOIN"))
        {
            sceneMenegement.crossFade("KOIN",true);
            Debug.Log("ke koin");
        }
        if (other.CompareTag("MiniGame_4"))
        {
            sceneMenegement.crossFade("MiniGame_4",false);
            Debug.Log("ke game");
        }
        if (other.CompareTag("MiniGame_5"))
        {
            sceneMenegement.crossFade("MiniGame_5", false);
            Debug.Log("ke game");
        }
        if (other.CompareTag("MiniGame_1"))
        {
            sceneMenegement.crossFade("Menu_MiniGame_1", false);
            Debug.Log("ke game");
        }
        if (other.CompareTag("MiniGame_3"))
        {
            sceneMenegement.crossFade("MiniGame_3", false);
            Debug.Log("ke game");
        }
        if (other.CompareTag("MiniGame_2"))
        {
            sceneMenegement.crossFade("MiniGame_2", false);
            Debug.Log("ke game");
        }

    }
    
}
