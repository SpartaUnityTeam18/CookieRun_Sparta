using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라 추적 기능
public class CameraController : MonoBehaviour
{
    // 타겟 지정 (쿠키)
    public Transform target;
    
    // 오프셋
    float offsetX;

    void Start()
    {
        if (target == null) return;

        // 오프셋 설정(카마레 위치와 쿠키의 위치의 차)
        offsetX = transform.position.x - target.position.x;
    }

    void Update()
    {
        if (target == null) return;

        // 카메라 위치
        Vector3 pos = transform.position;

        // 카메라 위치 추적
        pos.x = target.position.x + offsetX;
        transform.position = pos;
    }
}
