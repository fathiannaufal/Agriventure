using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BukuSpawner : MonoBehaviour
{
    public int rand;
    public Sprite[] sprite_pfb;

    public GameObject bukuPrefab;

    void Change()
    {
        rand = Random.Range(0, sprite_pfb.Length);
        bukuPrefab.GetComponent<SpriteRenderer>().sprite = sprite_pfb[rand];
    }

    public void SpawnBox()
    {
        Change();
        GameObject buku_Obj = Instantiate(bukuPrefab);
  
        Vector3 temp = transform.position;
        Vector3 scale = transform.localScale;
        temp.z = 90f;
        scale.x = 0.5f;
        scale.y = 0.5f;
        buku_Obj.transform.position = temp;
        buku_Obj.transform.localScale = scale;
    }
}
