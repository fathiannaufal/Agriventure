using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    public float timeFade, time = 0;

    public void ResetTime(){
        time = 0;
    }

    void Update(){
        time += Time.deltaTime;
        if(time > timeFade){
            time = 0;
            gameObject.SetActive(false);
        }
    }
}
