using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RdmAct : MonoBehaviour
{
    public GameObject[] ObjectsList;
    void Start()
    {
        Debug.Log(ObjectsList.Length);
        if(this.gameObject.CompareTag("rdm"))
        ObjectsList[Random.Range(0, ObjectsList.Length)].SetActive(true);
    }
}
