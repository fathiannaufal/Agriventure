using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawn : MonoBehaviour
{
    public List<Transform> transforms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        this.transform.position = transforms[0].position;
    }
}
