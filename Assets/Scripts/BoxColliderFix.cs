using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderFix : MonoBehaviour
{
    void Start()
    {
        Vector2 size = gameObject.GetComponent<SpriteRenderer>().size;
        gameObject.GetComponent<BoxCollider2D>().size = size;
    }

}
