using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowWorld: MonoBehaviour
{
    public Transform FollowTarget;
    public float speed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;


    private void Start()
    {
        //transform.position = FollowTarget.position;
        speed = 0.125f;
    }
    private void Update()
    {
        if(FollowTarget != null)
        {
            float clampedX = Mathf.Clamp(FollowTarget.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(FollowTarget.position.y, minY, maxY);

            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX,clampedY), speed);
        }
            
    }
}
