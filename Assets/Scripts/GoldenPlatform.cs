using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenPlatform : MonoBehaviour
{
    private bool moving = false;

    private Vector3 finishPos = new(0, 10, 0);
    private float speed = 0.5f;
    private Vector3 startPos;
    private float trackPercent = 0f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (moving && trackPercent < 1f) 
        {
            trackPercent +=  speed * Time.deltaTime;

            float x = finishPos.x * trackPercent + startPos.x;
            float y = finishPos.y * trackPercent + startPos.y;
            transform.position = new Vector3(x, y, startPos.z);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            moving = true;
        }
    }
}
