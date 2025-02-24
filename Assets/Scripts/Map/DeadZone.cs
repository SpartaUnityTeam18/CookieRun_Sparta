using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 낙사 판정
public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체에서 Cookie 컴포넌트 가져옴
        Cookie cookie = collision.GetComponent<Cookie>();

        // null이 아니면 Dead 실행
        if (cookie != null)
        {
            cookie.Hit(1000f);
            // 임시로 파괴 넣어둠
            Destroy(collision.gameObject);
        }
    }
}
