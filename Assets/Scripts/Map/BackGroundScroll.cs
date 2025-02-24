using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    public Transform cameraTransform;
    public float speed = 0.2f;

    private Vector3 prevCamPosition;

    void Start()
    {
        prevCamPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 movement = cameraTransform.position - prevCamPosition;
        transform.position += movement * speed;
        prevCamPosition = cameraTransform.position;
    }
}
