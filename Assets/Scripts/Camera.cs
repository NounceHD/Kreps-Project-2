using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;

    private void LateUpdate()
    {
        if (target.transform.position.y > -1)
        {
        targetPosition = new(target.position.x, target.position.y, transform.position.z);
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
