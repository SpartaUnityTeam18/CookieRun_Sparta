using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 배경, 전경 이동 효과
public class BackGroundScroll : MonoBehaviour
{
    // 카메라 위치
    public Transform cameraTransform;

    // 속도 ( 배경, 전경 이동 효과 )
    public float speed = 0.2f;

    // 이전 카메라 위치
    private Vector3 prevCameraPosition;

    void Start()
    {
        // 이전 카메라 위치 설정
        prevCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        // 설정한 속도에 따라 배경, 전경 이동
        Vector3 movement = cameraTransform.position - prevCameraPosition;
        transform.position += movement * speed;
        prevCameraPosition = cameraTransform.position;
    }
}
