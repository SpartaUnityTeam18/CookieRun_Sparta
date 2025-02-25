using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 장애물 판정
public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체에서 Cookie 컴포넌트 가져옴
        Cookie cookie = collision.GetComponent<Cookie>();

        // null이 아니면 HIt 실행
        if (cookie != null)
        {
            AchievementManager.Instance.RestDodgeAchievement();
            cookie.Hit(10f);
        }
    }
}
