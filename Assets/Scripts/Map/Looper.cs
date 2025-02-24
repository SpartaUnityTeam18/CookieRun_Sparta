using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 배경 무한 동력기
public class Looper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체의 태그가 Grounds면 동작
        if (collision.CompareTag("Grounds"))
        {
            // 콜라이더의 가로 길이
            float width = ((BoxCollider2D)collision).size.x;

            // 충돌한 객체의 위치
            Vector3 pos = collision.transform.position;

            // 객체의 x를 width만큼 이동
            pos.x += width;
            collision.transform.position = pos;
            return;
        }
    }
}
